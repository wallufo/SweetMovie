using SweetMoive.Areas.MovieUser.Models;
using SweetMoive.DAL.ModelManage;
using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.MovieUser.Controllers
{
    public class ArticleController : Controller
    {
        ArticleManage articleManage = new ArticleManage();
        [HttpGet]
        public ActionResult Index()
        {
            var _atr = articleManage.FindList();
            return View(_atr.ToList());
        }
        [HttpGet]
        public ActionResult ArticleDetail(int ID)
        {
            var _article = articleManage.Find(ID);
            return View(_article);
        }
        [HttpGet]
        public ActionResult ArticleSubmit()
        {
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
        [UserAuthorize]
        [ValidateInput(false)]
        public ActionResult ArticleSubmit(AddArticleViewModel addarticleViewModel,HttpPostedFileBase banner,int UserID)
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
                Article article = new Article();
                article.MovieID = addarticleViewModel.MovieID;
                article.Title = addarticleViewModel.Title;
                article.UserID = UserID;
                article.Content = addarticleViewModel.Content;
                article.Releasetime = DateTime.Now;
                article.Auditstatus = Article.Status.待审核;
                var _resp = articleManage.Add(article);
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(addarticleViewModel);
            }
        }
    }
}