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
        // GET: AdminPerson/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}