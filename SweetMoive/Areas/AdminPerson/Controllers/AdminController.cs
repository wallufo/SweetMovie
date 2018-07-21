using SweetMoive.Areas.AdminPerson.Models;
using SweetMoive.DAL.General;
using SweetMoive.DAL.ModelManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private AdministratorManage adminManage = new AdministratorManage();
        [HttpGet]
        // GET: AdminPerson/Admin
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //加密密码
                string _password = Security.Sha256(loginViewModel.Password);
                var _res = adminManage.Verity(loginViewModel.Accounts, _password);
                if (_res.Code == 2)
                {
                    var _admin = adminManage.Find(loginViewModel.Accounts);
                    Session.Add("AdminID", _admin.AdministratorID);
                    Session.Add("Accounts", _admin.Accounts);
                    _admin.LoginIP = Request.UserHostAddress;
                    _admin.LoginTime = DateTime.Now;
                    adminManage.Update(_admin);
                    return RedirectToAction("Index", "Home");
                }
                else if (_res.Code == 1) ModelState.AddModelError("Password", _res.Message);
                else if (_res.Code == 3) ModelState.AddModelError("Accounts", _res.Message);
                else ModelState.AddModelError("", _res.Message);
            }
            return View(loginViewModel);
        }
    }
}