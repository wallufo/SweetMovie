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
        [HttpPost]
        public ActionResult Modify(int ID, [Bind(Include = "ID,MovieName,ReleaseDate,Actors,Synopsis,Director,Type,Duration,Contry,Score,Hidden")] Movie movie, HttpPostedFileBase mPost, IEnumerable<HttpPostedFileBase> PostImg)
        {
            var MaxId = movie.ID;
            movie.PostersNum = 0;
            if (mPost != null)
            {
                movie.PostersNum++;
                movie.DefaultImgUrl = MaxId.ToString() + "1-1";
                var postname = Path.Combine(Request.MapPath("~Images"), movie.DefaultImgUrl + ".jpg");
                if (System.IO.File.Exists(postname))
                {
                    System.IO.File.Delete(postname);
                }
                mPost.SaveAs(postname);
            }
            if (PostImg.First() != null)
            {
                int i = movie.PostersNum;
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
                movie.PostersNum = i;
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
    }
}