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
        [UserAuthorize]
        public ActionResult Index()
        {
            var _user = userManage.Find(Convert.ToInt32(Session["UserID"]));         
            return View(new UserViewModel {
                ID=_user.ID,
                UserName=_user.Username,
                Sex=_user.Sex.ToString(),
                Mymottoy=_user.MyMotto,
                Role=Session["AuthUser"].ToString()
            });
        }
        [UserAuthorize]
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
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo([Bind(Include = "ID,DefaultImgUrl,Name,Mottoy,Sex")]EditUserViewModel edituserViewModel,HttpPostedFileBase userImg)
        {
            Response _resp = new DAL.Response();
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
        [UserAuthorize]
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
        public async Task<JsonResult> UploadImg(int id)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
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