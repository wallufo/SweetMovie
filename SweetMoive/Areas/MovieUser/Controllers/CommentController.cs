using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using SweetMoive.DAL.ModelManage;
using SweetMoive.DAL.Models;

namespace SweetMoive.Areas.MovieUser.Controllers
{
    public class CommentController : Controller
    {
        UserManage userManage = new UserManage();
        CommentManage commentMange = new CommentManage();
        [HttpGet]
        public ActionResult CommentList(int? id,int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _comment = commentMange.FindList(p => p.MovieID == id);
            _comment=_comment.OrderBy(p => p.CommentTime);
            const int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(_comment.ToPagedList(pageNumber,pageSize));
        }
        [UserAuthorize]
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CommentSubmit([Bind(Include ="ID,Content,score,UserID,MovieID")] MovieComment comment)
        {
            comment.CommentTime = DateTime.Now;

            //解决未拿到最新评论用户名BUG
            comment.User = userManage.Find(comment.UserID);
            int isUser=commentMange.Count(p => p.UserID == comment.UserID);
            if (isUser > 0)
            {
                return Content("<h1 class='text-center' style='color:#fff;'>您已评论过此电影!</h1>");
            }
            if (ModelState.IsValid)
            {
                commentMange.Add(comment);
                comment.User.SweetScore = comment.User.SweetScore++;
                userManage.Update(comment.User);
                var _comment = commentMange.FindList(p => p.MovieID == comment.UserID);
                _comment = _comment.OrderBy(p => p.CommentTime);
                const int pageSize = 5;
                return PartialView("CommentList", _comment.ToPagedList(1, pageSize));
            }
            return Content("<h1 class='text-center' style='color:#fff;'>评论失败</h1>");
        }
    }
}