using SweetMoive.Areas.AdminPerson.Models;
using SweetMoive.DAL;
using SweetMoive.DAL.General;
using SweetMoive.DAL.ModelManage;
using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Controllers
{
    [AdminAuthorize]
    public class CommentController : Controller
    {
        UserManage userManage = new UserManage();
        CommentManage commentManage = new CommentManage();
        [HttpGet]
        // GET: AdminPerson/Comment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PageListJson(int? pageIndex, int? pageSize, int? order)
        {
            Paging<MovieComment> _pageingComment = new Paging<MovieComment>();
            if (pageIndex != null && pageIndex > 0) _pageingComment.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingComment.PageSize = (int)pageSize;
            var LikeItems = new LikeManage().FindList().ToList();
            var _paging =commentManage.FindPageList(_pageingComment, 0);
            foreach(var item in _paging.Items)
            {
                int likes = 0;
                foreach (var likeitem in LikeItems)
                {
                    if(item.ID==likeitem.MovieCommentID)
                    {
                        likes++;
                    }
                }
                item.Likes = likes;
            }
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
        [HttpGet]
        public ActionResult Modify(int ID)
        {
            var _users = new UserManage().FindList();
            List<SelectListItem> _userlistItems = new List<SelectListItem>(_users.Count());
            foreach(var _user in _users)
            {
                _userlistItems.Add(new SelectListItem() { Text = _user.Username, Value = _user.ID.ToString() });
            }
            ViewBag.Users = _userlistItems;
            var _movies = new MovieManage().FindList();
            List<SelectListItem> _movielistItems = new List<SelectListItem>(_movies.Count());
            foreach (var _movie in _movies)
            {
                _movielistItems.Add(new SelectListItem() { Text = _movie.MovieName, Value = _movie.ID.ToString() });
            }
            ViewBag.Movies = _movielistItems;
            var comment = commentManage.Find(ID);
            var _likes = new LikeManage().FindList();
            int like = 0;
            foreach (var _like in _likes)
            {
                if (ID == _like.MovieCommentID)
                {
                    like++;
                }
            }
            comment.Likes = like;
            return PartialView(comment);
        }
        [HttpPost]
        public ActionResult Modify(int ID,[Bind(Include = "ID,UserID,MovieID,Content,CommentTime,Score,Likes")] MovieComment movieComment)
        {
            Response _resp = new DAL.Response();
            var _comment = commentManage.Find(ID);
            if (ModelState.IsValid)
            {
                if (_comment == null)
                {
                    _resp.Code = 0;
                    _resp.Message = "该评论不存在,可能遭到删除";
                }
                else
                {
                    _resp = commentManage.Update(movieComment);
                }
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_resp);
        }
        [HttpGet]
        public ActionResult Add()
        {
            var _users = new UserManage().FindList();
            List<SelectListItem> _userlistItems = new List<SelectListItem>(_users.Count());
            foreach (var _user in _users)
            {
                _userlistItems.Add(new SelectListItem() { Text = _user.Username, Value = _user.ID.ToString() });
            }
            ViewBag.Users = _userlistItems;
            var _movies = new MovieManage().FindList();
            List<SelectListItem> _movielistItems = new List<SelectListItem>(_movies.Count());
            foreach (var _movie in _movies)
            {
                _movielistItems.Add(new SelectListItem() { Text = _movie.MovieName, Value = _movie.ID.ToString() });
            }
            ViewBag.Movies = _movielistItems;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddCommentViewModel commentViewModel)
        {
            if (commentManage.HasUserComment(commentViewModel.UserID, commentViewModel.MovieID))
            {
                return View("Prompt", new Prompt()
                {
                    Title = "添加失败",
                    Message = "当前用户已对当前电影评论\n注意！每个用户对每个电影只能评论一次！",
                    Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Comment")+"\" class=\"btn btn-default\">评论管理</a>",
                        "<a href=\"" + Url.Action("Add", "Comment") + "\" class=\"btn btn-default\">重新添加</a>"}
                }
                );
            }
            if (ModelState.IsValid)
            {
                MovieComment _comment = new MovieComment();
                _comment.UserID = commentViewModel.UserID;
                _comment.MovieID = commentViewModel.MovieID;
                _comment.CommentTime = commentViewModel.CommentTime;
                _comment.Content = commentViewModel.Content;
                _comment.Score = commentViewModel.Score;
                var _user = userManage.Find(commentViewModel.UserID);
                if (_user.Userstatus == DAL.Models.User.UserStatus.未启用)
                {
                    return View("Prompt", new Prompt()
                    {
                        Title = "添加评论失败",
                        Message = "当前用户已被冻结，无法评论！请先启用当前用户",
                        Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Comment")+"\" class=\"btn btn-default\">评论管理</a>",
                        "<a href=\"" + Url.Action("Add", "Comment") + "\" class=\"btn btn-default\">重新添加</a>"}
                    }
                );
                }
                var _resp = commentManage.Add(_comment);
                if (_resp.Code == 1) return View("Prompt", new Prompt()
                {
                    Title = "添加评论成功",
                    Message = "您已为此用户添加了评论",
                    Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Comment")+"\" class=\"btn btn-default\">评论管理</a>",
                        "<a href=\"" + Url.Action("Add", "Comment") + "\" class=\"btn btn-default\">继续添加</a>"}
                }
                );
                else ModelState.AddModelError("", _resp.Message);
            }
            else
            {
                return View(commentViewModel);
            }
            var _users = new UserManage().FindList();
            List<SelectListItem> _userlistItems = new List<SelectListItem>(_users.Count());
            foreach (var _user in _users)
            {
                _userlistItems.Add(new SelectListItem() { Text = _user.Username, Value = _user.ID.ToString() });
            }
            ViewBag.Users = _userlistItems;
            var _movies = new MovieManage().FindList();
            List<SelectListItem> _movielistItems = new List<SelectListItem>(_movies.Count());
            foreach (var _movie in _movies)
            {
                _movielistItems.Add(new SelectListItem() { Text = _movie.MovieName, Value = _movie.ID.ToString() });
            }
            ViewBag.Movies = _movielistItems;
            return View(commentViewModel);
        }
        [HttpPost]
        public JsonResult DeleteJson(List<int> ids)
        {
            int _total = ids.Count();
            Response _resp = new DAL.Response();
            _resp = commentManage.Delete(ids);
            if (_resp.Code == 1)
            {
                _resp.Code = 2;
                _resp.Message = "共提交删除" + _total + "条评论，实际删除" + _resp.Data + "条评论。";
            }
            return Json(_resp);
        }

    }
}