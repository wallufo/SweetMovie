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
    public class MovieController : Controller
    {
        MovieManage movieManage = new MovieManage();
        [HttpGet]
        // GET: AdminPerson/Movie
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PageListJson(int? pageIndex, int? pageSize, int? order)
        {
            Paging<Movie> _pageingUser = new Paging<Movie>();
            if (pageIndex != null && pageIndex > 0) _pageingUser.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingUser.PageSize = (int)pageSize;
            var _paging = movieManage.FindPageList(_pageingUser, 0);
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
        [HttpGet]
        public ActionResult Modify(int ID)
        {
            return View(movieManage.Find(ID));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Modify(int ID, [Bind(Include = "ID,DefaultImgUrl,PostersNum,MovieName,ReleaseDate,Actors,Synopsis,Director,Type,Duration,Contry,Score,PreviewUrl,Hidden")] Movie movie, HttpPostedFileBase mPost, IEnumerable<HttpPostedFileBase> PostImg)
        {
            var MaxId = movie.ID;
            if (mPost != null)
            {
                movie.PostersNum = 0;
                movie.PostersNum++;
                movie.DefaultImgUrl = MaxId.ToString() + "-1-1";
                var postname = Path.Combine(Request.MapPath("~/Images"), movie.DefaultImgUrl + ".jpg");
                if (System.IO.File.Exists(postname))
                {
                    System.IO.File.Delete(postname);
                }
                mPost.SaveAs(postname);
            }
            if (PostImg.First() != null)
            {
                int i = 1;
                foreach(var item in PostImg)
                {
                    i++;
                    var filename = MaxId.ToString() + "-1-" + i.ToString();
                    var resultname = Path.Combine(Request.MapPath("~/Images"), filename + ".jpg");
                    if (System.IO.File.Exists(resultname))
                    {
                        System.IO.File.Delete(resultname);
                    }
                    item.SaveAs(resultname);
                }
                movie.PostersNum = i-1;
            }
            if (ModelState.IsValid)
            {
                var _resp = movieManage.Update(movie);
                if (_resp.Code == 1) return View("Prompt", new Prompt()
                {
                    Title = "修改电影数据成功",
                    Message = "您已成功的修改了电影【" + _resp.Data.MovieName + "】",
                    Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Movie")+"\" class=\"btn btn-default\">返回电影管理</a>",
                        "<a href=\"" + Url.Action("Add", "Movie") + "\" class=\"btn btn-default\">添加电影</a>"}
                }
                   );
                else ModelState.AddModelError("", _resp.Message);
            }
            return View(movie);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(AddMovieViewModel movieViewModel,HttpPostedFileBase mPost,IEnumerable<HttpPostedFileBase> PostImg)
        {
            if (movieManage.HasMovieName(movieViewModel.MovieName)) ModelState.AddModelError("MovieName", "当前输入的电影已存在");
            Movie _movie = new Movie();
            var MaxId = movieManage.MovieId(p => p.ID);
            MaxId += 1;
            _movie.PostersNum = 0;
            if (mPost != null)
            {
                _movie.DefaultImgUrl = MaxId.ToString() + "-1-1";
                var postname = Path.Combine(Request.MapPath("~/Images"), _movie.DefaultImgUrl + ".jpg");
                _movie.PostersNum++;
                mPost.SaveAs(postname);
            }
            if (PostImg.First() != null)
            {
                int i = _movie.PostersNum;
                foreach (var item in PostImg)
                {
                    i++;
                    var filename = MaxId.ToString() + "-1-" + i.ToString();
                    var resultname = Path.Combine(Request.MapPath("~/Images"), filename + ".jpg");
                    //if (System.IO.File.Exists(resultname))
                    //{
                    //    System.IO.File.Delete(resultname);
                    //}
                    item.SaveAs(resultname);
                }
                _movie.PostersNum = i - 1;
            }
            if (ModelState.IsValid)
            {
                _movie.MovieName = movieViewModel.MovieName;
                _movie.ReleaseDate = movieViewModel.ReleaseDate;
                _movie.Actors = movieViewModel.Actors;
                _movie.Synopsis = movieViewModel.Synopsis;
                _movie.Director = movieViewModel.Director;
                _movie.Type = (Movie.Types)movieViewModel.Type;
                _movie.Duration = movieViewModel.Duration;
                _movie.Contry = movieViewModel.Contry;
                _movie.Score = movieViewModel.Score;
                _movie.PreviewUrl = movieViewModel.PreviewUrl;
                _movie.Hidden = (Movie.Hiddens)movieViewModel.Hidden;
                var _resp = movieManage.Add(_movie);
                if (_resp.Code == 1) return View("Prompt", new Prompt()
                {
                    Title = "添加电影数据成功",
                    Message = "您已成功的添加了电影【" + _resp.Data.MovieName + "】",
                    Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Movie")+"\" class=\"btn btn-default\">返回电影管理</a>",
                        "<a href=\"" + Url.Action("Add", "Movie") + "\" class=\"btn btn-default\">继续添加电影</a>"}
                });
                else ModelState.AddModelError("", _resp.Message);
            }
            return View(movieViewModel);
        }
        [HttpPost]
        public JsonResult HasMovieName(string MovieName)
        {
            return Json(!movieManage.HasMovieName(MovieName));
        }
    }
}