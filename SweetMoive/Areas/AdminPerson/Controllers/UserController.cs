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
    public class UserController : Controller
    {
        UserManage userManage = new UserManage();
        [HttpGet]
        // GET: AdminPerson/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PageListJson(int? pageIndex, int? pageSize, int? order)
        {
            Paging<User> _pageingUser = new Paging<User>();
            if (pageIndex != null && pageIndex > 0) _pageingUser.PageIndex = (int)pageIndex;
            if (pageSize != null && pageSize > 0) _pageingUser.PageSize = (int)pageSize;
            var _paging = userManage.FindPageList(_pageingUser, 0);
            return Json(new { total = _paging.TotalNumber, rows = _paging.Items });
        }
        [HttpGet]
        public ActionResult Modify(int ID)
        {
            return PartialView(userManage.Find(ID));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(int ID,[Bind(Include = "ID,DefaultImgUrl,Role,Sex,Username,Password,Name,MyMotto,SweetScore,EmailAdress,Userstatus")] User user)
        {
            Response _resp = new DAL.Response();
            var _user = userManage.Find(ID);
            if (ModelState.IsValid)
            {
                if (_user == null)
                {
                    _resp.Code = 0;
                    _resp.Message = "用户不存在，可能遭到删除";
                }
                else
                {
                    _resp = userManage.Update(user);
                }
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = General.GetModelErrorString(ModelState);
            }
            return Json(_resp);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddUserViewModel addUderViewModel)
        {
            if (userManage.HasUserName(addUderViewModel.Username)) ModelState.AddModelError("Username", "用户名已存在");
            if (userManage.HasUserEmail(addUderViewModel.EmailAdress)) ModelState.AddModelError("EmailAdress", "邮箱已存在");
            if (ModelState.IsValid)
            {
                if (Security.Sha256(addUderViewModel.Password) == Security.Sha256(addUderViewModel.ConfirmPassword))
                {
                    User _user = new User();
                    _user.Role = (User.Roles)addUderViewModel.Role;
                    _user.Username = addUderViewModel.Username;
                    _user.Password = Security.Sha256(addUderViewModel.Password);
                    _user.Name = addUderViewModel.Name;
                    _user.EmailAdress = addUderViewModel.EmailAdress;
                    _user.Userstatus = (User.UserStatus)addUderViewModel.Userstatus;
                    _user.DefaultImgUrl = "/DefaultImg/Default.jpg";
                    _user.SweetScore = addUderViewModel.SweetScore;
                    var _resp = userManage.Add(_user);
                    if (_resp.Code == 1) return View("Prompt", new Prompt()
                    {
                        Title = "添加用户成功",
                        Message = "您已添加了用户【" + _resp.Data.Username + "】",
                        Buttons = new List<string>(){"<a href=\""+Url.Action("Index","User")+"\" class=\"btn btn-default\">用户管理</a>",
                        "<a href=\"" + Url.Action("Add", "User") + "\" class=\"btn btn-default\">继续添加</a>"}
                    }
                    );
                    else ModelState.AddModelError("",_resp.Message); 
                }
               
            }
            else
            {
                return View(addUderViewModel);
            }
            return View(addUderViewModel);
        }
        [HttpPost]
        public JsonResult HasUsername(string Username)
        {
            return Json(!userManage.HasUserName(Username));
        }
        [HttpPost]
        public JsonResult HasEmailAdress(string EmailAdress)
        {
            return Json(!userManage.HasUserEmail(EmailAdress));
        }
    }
}