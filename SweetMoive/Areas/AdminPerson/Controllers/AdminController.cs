using SweetMoive.Areas.AdminPerson.Models;
using SweetMoive.DAL;
using SweetMoive.DAL.General;
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
        public ActionResult LoginOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public JsonResult PageListJson(int? pageIndex,int? pageSize,int? order)
        {
            Paging<Administrator> _pageingAdmin = new Paging<Administrator>();
            if (pageIndex != null && pageIndex > 0) _pageingAdmin.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingAdmin.PageSize = (int)pageSize;
            var _paging = adminManage.FindPageList(_pageingAdmin, 0);
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(AddAdminViewModel addAdminVieModel)
        {
            if (adminManage.HasAccounts(addAdminVieModel.Accounts)) ModelState.AddModelError("Accounts", "账号已存在");
            if (ModelState.IsValid)
            {
                if(Security.Sha256(addAdminVieModel.Password)== Security.Sha256(addAdminVieModel.ConfirmPassword))
                {
                    Administrator _admin = new Administrator();
                    _admin.Accounts = addAdminVieModel.Accounts;
                    _admin.Password = Security.Sha256(addAdminVieModel.Password);
                    _admin.CreateTime = DateTime.Now;
                    var _resp = adminManage.Add(_admin);
                    if (_resp.Code == 1) return View("Prompt", new Prompt()
                    {
                        Title = "添加管理员成功",
                        Message = "您已添加了管理员【" + _resp.Data.Accounts + "】",
                        Buttons = new List<string>(){"<a href=\""+Url.Action("Index","Admin")+"\" class=\"btn btn-default\">管理员管理</a>",
                        "<a href=\"" + Url.Action("Add", "Admin") + "\" class=\"btn btn-default\">继续添加</a>"}
                    });
                    else ModelState.AddModelError("", _resp.Message);
                }
                else
                {
                    return View(addAdminVieModel);
                }
                
            }
            return View(addAdminVieModel);
        }
        [HttpPost]
        public JsonResult CanAccounts(string Accounts)
        {
            return Json(!adminManage.HasAccounts(Accounts));
        }
        [HttpPost]
        public JsonResult ResetPassword(int ID)
        {
            string _password = "SweetMovie";
            Response _res = adminManage.ChangePassword(ID, DAL.General.Security.Sha256(_password));
            if (_res.Code == 1) _res.Message = "密码重置为:" + _password;
            return Json(_res);
        }
        [HttpPost]
        public JsonResult DeleteJson(List<int> ids)
        {
            int _total = ids.Count();
            Response _resp = new DAL.Response();
            int _currentAdminID = int.Parse(Session["AdminID"].ToString());
            if (ids.Contains(_currentAdminID))
            {
                ids.Remove(_currentAdminID);
            }
            _resp = adminManage.Delete(ids);
            if (_resp.Code == 1 && _resp.Data < _total)
            {
                _resp.Code = 2;
                _resp.Message = "共提交删除" + _total + "名管理员，实际删除" + _resp.Data + "名管理员。/n原因不能删除当前管理员!";
            }
            else if (_resp.Code == 2)
            {
                _resp.Message = "共提交删除" + _total + "名管理员，实际删除" + _resp.Data + "名管理员。";
            }
            return Json(_resp);
        }
        [HttpGet]
        public ActionResult MyInfo()
        {
            return View(adminManage.Find(Session["Accounts"].ToString()));
        }
    }
}