using SweetMoive.DAL;
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
    }
}