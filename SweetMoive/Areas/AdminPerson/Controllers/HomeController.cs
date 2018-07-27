using SweetMoive.DAL.ModelManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        [HttpGet]
        // GET: AdminPerson/Home
        public ActionResult Index()
        {
            ArticleManage _article = new ArticleManage();
            ViewBag.CommentNum = new CommentManage().Count();
            ViewBag.UserNum = new UserManage().Count();
            ViewBag.MovieNum = new MovieManage().Count();
            ViewBag.ArticleNum = _article.Count();
            List<DateTime> SevenDay = new List<DateTime>();
            List<int> Counts = new List<int>();
            for(var i = -6; i < 1; i++)
            {
                SevenDay.Add(DateTime.Now.Date.AddDays(i));
            }
            foreach(var item in SevenDay)
            {
                Counts.Add(_article.Count(p => p.Releasetime == item.Date));
            }
            ViewBag.labels = SevenDay.ToArray();
            ViewBag.data = Counts.ToArray();
            return View();
        }
    }
}