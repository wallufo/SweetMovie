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
    public class CommentController : Controller
    {
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
            Paging<MovieComment> _pageingUser = new Paging<MovieComment>();
            if (pageIndex != null && pageIndex > 0) _pageingUser.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingUser.PageSize = (int)pageSize;
            var _paging = commentManage.FindPageList(_pageingUser, 0);
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
    }
}