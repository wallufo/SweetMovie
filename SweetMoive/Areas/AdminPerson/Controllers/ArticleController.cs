using SweetMoive.Areas.AdminPerson.Models;
using SweetMoive.DAL;
using SweetMoive.DAL.General;
using SweetMoive.DAL.ModelManage;
using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Controllers
{
    [AdminAuthorize]
    public class ArticleController : Controller
    {
        ArticleManage articleManage = new ArticleManage();
        [HttpGet]
        // GET: AdminPerson/Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PageListJson(int? pageIndex,int? pageSize,int? order)
        {
            Paging<Article> _pageingArticle = new Paging<Article>();
            if (pageIndex != null && pageIndex > 0) _pageingArticle.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingArticle.PageSize = (int)pageSize;
            var _paging = articleManage.FindPageList(_pageingArticle, 0);
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
        [HttpGet]
        public ActionResult Audit()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AuditPageListJson(int? pageIndex,int? pageSize,int? order)
        {
            Paging<Article> _pageingArticle = new Paging<Article>();
            if (pageIndex != null && pageIndex > 0) _pageingArticle.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingArticle.PageSize = (int)pageSize;
            var _paging = articleManage.FindAuditPageList(_pageingArticle, 0);
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
        [HttpPost]
        public JsonResult PassArticle(int ID,int status)
        {
            Response _resp = articleManage.ChangeStatus(ID,status);
            if (_resp.Code == 4) _resp.Message = "对当前文章审核通过";
            if (_resp.Code == 3) _resp.Message = "对当前文章拒绝审核通过";
            return Json(_resp);
        }
        [HttpGet]
        public ActionResult Modify(int ID)
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
            var _Article = articleManage.Find(ID);
            return View(_Article);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modify(int ID,[Bind(Include ="ID,UserID,MovieID,Releasetime,Title,Content,Auditstatus")] Article article, HttpPostedFileBase banner)
        {
            if (banner != null)
            {
                var bannerName = Path.Combine(Request.MapPath("/ArticleImg"), article.ID + ".jpg");
                if (System.IO.File.Exists(bannerName))
                {
                    System.IO.File.Delete(bannerName);
                }
                banner.SaveAs(bannerName);
            }
            if (ModelState.IsValid)
            {
                var _resp = articleManage.Update(article);
                if(_resp.Code==1) return View("Prompt", new Prompt()
                {
                    Title = "修改文章数据成功",
                    Message = "您已成功的修改了文章【" + _resp.Data.Title + "】",
                    Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Article")+"\" class=\"btn btn-default\">返回电影管理</a>",
                        "<a href=\"" + Url.Action("Add", "Article") + "\" class=\"btn btn-default\">添加电影</a>"}
                }
                );
               else ModelState.AddModelError("", _resp.Message);
            }
            return View(article);
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
        [ValidateInput(false)]
        public ActionResult Add(AddArticelViewModel articleViewModel, HttpPostedFileBase banner)
        {
            var MaxId = articleManage.ArticleId(p => p.ID);
            var articleID = MaxId + 1;
            if (banner != null)
            {
                var bannerName = Path.Combine(Request.MapPath("/ArticleImg"), articleID + ".jpg");
                if (System.IO.File.Exists(bannerName))
                {
                    System.IO.File.Delete(bannerName);
                }
                banner.SaveAs(bannerName);
            }
            if (ModelState.IsValid)
            {
                Article _article = new Article();
                _article.UserID = articleViewModel.UserID;
                _article.MovieID = articleViewModel.MovieID;
                _article.Releasetime = articleViewModel.Releasetime;
                _article.Title = articleViewModel.Title;
                _article.Content = articleViewModel.Content;
                _article.Auditstatus = Article.Status.待审核;
                var _resp = articleManage.Add(_article);
                if (_resp.Code == 1) return View("Prompt", new Prompt()
                {
                    Title = "添加文章成功",
                    Message = "您成功的添加了当前文章",
                    Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Article")+"\" class=\"btn btn-default\">文章管理</a>",
                        "<a href=\"" + Url.Action("Add", "Article") + "\" class=\"btn btn-default\">继续添加</a>"}
                }
               );
                else ModelState.AddModelError("", _resp.Message);
            }
            else
            {
                return View(articleViewModel);
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
            return View(articleViewModel);
        }
        [HttpPost]
        public JsonResult DeleteJson(List<int> ids)
        {
            int _total = ids.Count();
            Response _resp = new DAL.Response();
            _resp = articleManage.Delete(ids);
            if (_resp.Code == 1)
            {
                _resp.Code = 2;
                _resp.Message = "共提交删除" + _total + "篇文章，实际删除" + _resp.Data + "篇文章。";
            }
            return Json(_resp);
        }
    }
}