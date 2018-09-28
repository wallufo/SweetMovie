using PagedList;
using SweetMoive.Areas.MovieUser.Models;
using SweetMoive.DAL;
using SweetMoive.DAL.ModelManage;
using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.MovieUser.Controllers
{
    public class UserController : Controller
    {
        MovieManage movieManage = new MovieManage();      
        UserManage userManage = new UserManage();
        FavoriteManage favoriteManage = new FavoriteManage();
        HistoryManage historyManage = new HistoryManage();
        LikeManage likeMange = new LikeManage();
        FollowerManage followerMange = new FollowerManage();
        CommentManage commentMange = new CommentManage();
        ArticleManage articleMange = new ArticleManage();
        [UserAuthorize]
        public ActionResult Index()
        {
            var _user = userManage.Find(Convert.ToInt32(Session["UserID"]));
            return View(new UserViewModel {
                ID=_user.ID,
                UserName=_user.Username,
                Sex=_user.Sex.ToString(),
                Mymottoy=_user.MyMotto,
                Role=_user.Role.ToString(),
                SweetScore=_user.SweetScore
            });
        }
        [HttpGet]
        public ActionResult MyFavorite(int? id,int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _fav = favoriteManage.FindList(p => p.UserID == id);
            _fav = _fav.OrderBy(p => p.ID);
            const int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(_fav.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult EditUserInfo(int id)
        {
            var _user = userManage.Find(id);
            return PartialView(new EditUserViewModel {
                ID=_user.ID,
                Name=_user.Name,
                Sex=_user.Sex,
                Mottoy=_user.MyMotto,
                DefalutImgUrl=_user.DefaultImgUrl
            });
        }
        [HttpPost]
        public ActionResult isLike(int? id,int? userID)
        {
            // status[0:未点赞;1:已点赞;2:用户未登录]
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (userID == 0)
            {
                return Json(new { status = 2 });
            }
            var likecount = likeMange.Count(p => p.MovieCommentID == id && p.UserID == userID);
            if (likecount != 0)
            {
                return Json(new { status = 1 });
            }
            else
            {
                return Json(new { status = 0 });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo([Bind(Include = "ID,DefaultImgUrl,Name,Mottoy,Sex")]EditUserViewModel edituserViewModel,HttpPostedFileBase userImg)
        {
            Response _resp = new Response();
            var _user = userManage.Find(edituserViewModel.ID);
            var DefaultImg = "/DefaultImg/" + edituserViewModel.ID + ".jpg";
            _user.DefaultImgUrl= DefaultImg;
            if (ModelState.IsValid)
            {
                _user.MyMotto = edituserViewModel.Mottoy;
                _user.Name = edituserViewModel.Name;
                _user.Sex = edituserViewModel.Sex;
                _resp = userManage.Update(_user);
                if (_resp.Code == 1)
                {
                    _resp.Message = "用户信息填写成功！";
                }
                else
                {
                    _resp.Message = "信息上传失败，请重新尝试！";
                }
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_resp);
        }
        [UserAuthorize]
        [HttpPost]
        public ActionResult Favorite(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _movie = movieManage.Find(id);
            if (_movie.Hidden == Movie.Hiddens.隐藏 || _movie == null)
            {
                return HttpNotFound();
            }
            var _user = userManage.Find(Convert.ToInt32(Session["UserID"]));
            var MovieID = _movie.ID;
            //Status【0:收藏成功1:取消收藏】
            var _fav = favoriteManage.Find(id, _user.ID);
            if (_fav==null)
            {
                Favorite _favor = new Favorite();
                _favor.MovieID = id;
                _favor.UserID = _user.ID;
                var _resp = favoriteManage.Add(_favor);
                return Json(new { StatusCode = 0 });
            }
            else
            {
                var _resp = favoriteManage.Delete(_fav.ID);
                return Json(new { StatusCode = 1 });
            }
        }
        [HttpPost]
        [UserAuthorize]
        public ActionResult Follower(int userID,int FollowerID)
        {
            var _follo = followerMange.Find(userID, FollowerID);
            if (_follo == null)
            {
                Follower _fow = new Follower();
                _fow.UserID = userID;
                _fow.Followed_user = FollowerID;
                var _resp = followerMange.Add(_fow);
                return Json(new { StatusCode = 1 });
            }
            else
            {
                var _resp = followerMange.Delete(_follo.ID);
                return Json(new { StatusCode = 0 });
            }
        }
        [HttpPost]
        [UserAuthorize]
        public ActionResult Likes(int userID,int commentID)
        {
            var _like = likeMange.Find(userID, commentID);
            var _comment = commentMange.Find(commentID);
            var _user = userManage.Find(_comment.UserID);
            if (_like == null)
            {
                Like like = new Like();
                like.UserID = userID;
                like.MovieCommentID = commentID;
                _comment.Likes = _comment.Likes++;
                _user.SweetScore = _user.SweetScore++;
                var _resp = likeMange.Add(like);
                var _res = commentMange.Update(_comment);
                var _re = userManage.Update(_user);
                return Json(new { StatusCode = 1 });
            }
            else
            {
                _comment.Likes = _comment.Likes--;
                _user.SweetScore = _user.SweetScore--;
                var _res = commentMange.Update(_comment);
                var _re = userManage.Update(_user);
                var _resp = likeMange.Delete(_like.ID);
                return Json(new { StatusCode = 0 });
            }
        }
        [HttpGet]
        public ActionResult MyHistory(int? id,int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _his = historyManage.FindList(p => p.UserID == id);
            _his = _his.OrderByDescending(p => p.ViewTime);
            const int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(_his.ToPagedList(pageNumber,pageSize));
        }
        [HttpPost]
        [UserAuthorize]
        public ActionResult DeleteHistory(int id)
        {
            var _his = historyManage.Find(id);
            historyManage.Delete(id);
            return Json(new { success = 1 });
            
        }
        [HttpGet]
        [UserAuthorize]
        public ActionResult Myfork(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _fork = followerMange.FindList(p => p.UserID == id).ToList();
            List<User> _user = new List<User>();
            foreach(var item in _fork)
            {
                var _follower = userManage.Find(p => p.ID == item.Followed_user);
                _user.Add(_follower);
            }
            return View(_user);
        }
        [HttpGet]
        public ActionResult OtherUser(int id)
        {
            var _user = userManage.Find(id);
            if (Session["UserID"] != null)
            {
                var userID = Convert.ToInt32(Session["UserID"]);
                int count = followerMange.Count(p => p.UserID == userID && p.Followed_user == id);
                if (count == 0)
                {
                    ViewBag.isFollower = false;
                }
                else
                {
                    ViewBag.isFollower = true;
                }
            }
            else
            {
                ViewBag.isFollower = false;
            }
            return View(new UserViewModel {
                ID=_user.ID,
                UserName=_user.Username,
                Mymottoy=_user.MyMotto,
                Name=_user.Name,
                Sex=_user.Sex.ToString(),
                Role=_user.Role.ToString()
            });
        }
        [HttpPost]
        public ActionResult Role(int id)
        {
            var _user = userManage.Find(id);
            if (_user.SweetScore >= 60)
            {
                if (_user.Role.ToString() == "普通用户")
                {
                    _user.Role = DAL.Models.User.Roles.评论专家;
                    var _resp = userManage.Update(_user);
                    return Json(new { success = 1 });
                }
                else
                {
                    return Json(new { success = 2 });
                }
            }
            else
            {
                return Json(new { success = 0 });
            }
        }
        [HttpGet]
        public ActionResult MyArticle(int id, int? page)
        {
            var _art = articleMange.FindList(p => p.UserID == id);
            _art = _art.OrderByDescending(p => p.Releasetime);
            const int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(_art.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public async Task<JsonResult> UploadImg(int id)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        var fileName = Path.GetFileName(file);
                        var fileUrl = "/DefaultImg/" + id + ".jpg";
                        var path = Path.Combine(Server.MapPath("~/DefaultImg"), fileName + ".jpg");
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }
            return Json("File uploaded successfully");
        }
    }
}