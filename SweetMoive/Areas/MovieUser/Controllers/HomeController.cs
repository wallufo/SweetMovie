using SweetMoive.Areas.MovieUser.Models;
using SweetMoive.DAL;
using SweetMoive.DAL.General;
using SweetMoive.DAL.ModelManage;
using SweetMoive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SweetMoive.Areas.MovieUser.Controllers
{
    public class HomeController : Controller
    {
        UserManage userManage = new UserManage();
        MovieManage movieManage = new MovieManage();
        FavoriteManage favoriteManage = new FavoriteManage();
        HistoryManage historyManage = new HistoryManage();
        [HttpGet]
        public ActionResult Index()
        {
            return View(movieManage.FindList().ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username,string Password,string Code)
        {
            //success【0:用户名或密码不为空;1:验证码不为空;2:用户名或密码错误;3:验证码错误;4:登陆成功;5:当前用户被冻结】
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)) return Json(new { success = 0 });
            if(string.IsNullOrWhiteSpace(Code)) return Json(new { success = 1 });
            if(Session["Validatecode"].ToString()!=Code) return Json(new { success = 3 });
            else
            {
                string _password = Security.Sha256(Password);
                var _resp = userManage.Verity(Username, _password);
                if (_resp.Code == 2)
                {
                    var _user = userManage.Find(Username);
                    if (_user.Userstatus == DAL.Models.User.UserStatus.未启用)
                    {
                        return Json(new { success=5 });
                    }
                    else
                    {
                        Session.Add("UserID", _user.ID);
                        Session.Add("AuthUser", _user.Role);
                        Session.Add("Username", _user.Username);
                        return Json(new { success = 4 });
                    }
                }
                else
                {
                    return Json(new { success = 0 });
                }
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MovieHistory()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MovieLeaderboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MovieList()
        {
            return View(movieManage.FindList().ToList());
        }
        [HttpPost]
        public ActionResult MovieList(string keyword)
        {
            Search<Movie> _searchMovie = new Search<Movie>();
            if (!String.IsNullOrEmpty(keyword))
            {
                var _movie = movieManage.Search(_searchMovie, keyword, 0);
                return View(_movie.Items);
            }
            else
            {
                var _Allmovie = movieManage.FindList().ToList();
                return View(_Allmovie);
            }
            
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (userManage.HasUserName(registerViewModel.Username)) ModelState.AddModelError("Username", "用户名已存在");
            if (userManage.HasUserEmail(registerViewModel.EmailAdress)) ModelState.AddModelError("EmailAdress", "邮箱已存在");
            if (ModelState.IsValid)
            {
                if (Session["EmailCode"] == null)
                {
                    ModelState.AddModelError("Code", "请获取验证码");
                }
                else if (registerViewModel.Code == Session["EmailCode"].ToString())
                {
                    if (Security.Sha256(registerViewModel.Password) == Security.Sha256(registerViewModel.ConfirmPassword))
                    {
                        User _user = new User();
                        _user.Username = registerViewModel.Username;
                        _user.EmailAdress = registerViewModel.EmailAdress;
                        _user.Role = DAL.Models.User.Roles.普通用户;  //默认用户身份
                        _user.Password = Security.Sha256(registerViewModel.Password);
                        _user.DefaultImgUrl = "/DefaultImg/Default.jpg";
                        _user.SweetScore = 40;
                        _user.Userstatus = DAL.Models.User.UserStatus.启用;
                        var _resp = userManage.Add(_user);
                        if (_resp.Code == 1) return RedirectToAction("Login");
                        else ModelState.AddModelError("", _resp.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("Code", "验证码不正确");
                }
            }
            else
            {
                return View(registerViewModel);
            }
            return View(registerViewModel);
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
        [HttpPost]
        public ActionResult SendEmail(string EmailAdress)
        {
            try
            {
                Session["EmailCode"] = randomnumber(4);
                MailMessage _msg = new MailMessage("sweetmoviesite@163.com", EmailAdress, "SweetMovie网站注册验证码", "欢迎来到SweetMovie网站，您的验证码是:" + Session["EmailCode"]);//由第一个邮箱向第二个邮箱发送邮件
                SmtpClient _client = new SmtpClient("smtp.163.com", 25);//163邮箱的服务器和端口 
                _client.DeliveryMethod = SmtpDeliveryMethod.Network;//通过网络发送
                _client.Credentials = new System.Net.NetworkCredential("sweetmoviesite@163.com", "sweetmovie1");//发件人的邮箱和密码
                _client.Send(_msg);
                return Content("发送成功");
            }
            catch
            {
                return Content("发送失败");
            }
        }
        public ActionResult GetValidateCode()
        {
            ValidateCode _vcode = new ValidateCode();
            string code = _vcode.CreateValidateCode(4);
            Session["Validatecode"] = code;
            byte[] bytes = _vcode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        /// <summary>
        /// 邮箱验证码
        /// </summary>
        /// <param name="length">四位</param>
        /// <returns></returns>
        public static string randomnumber(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
        [HttpGet]
        public ActionResult MovieDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _movie = movieManage.Find(id);
            if (_movie == null && _movie.Hidden == Movie.Hiddens.隐藏)
            {
                return View("404NotFound");
            }
            if (Session["UserID"] != null)
            {
                var _userid = Convert.ToInt32(Session["UserID"]);
                var favCount = favoriteManage.Count(p => p.UserID == _userid && p.MovieID == id);
                ViewBag.AlreadyFavorite = (favCount == 0) ? false : true;
                var _history = historyManage.Find(id,_userid);
                if (_history == null)
                {
                    History his = new History();
                    his.ViewTime = DateTime.Now;
                    his.MovieID = id;
                    his.UserID = _userid;
                    var _resp = historyManage.Add(his);
                }
                else
                {
                    _history.ViewTime = DateTime.Now;
                    var _resp = historyManage.Update(_history);
                }
            }
            return View(_movie);
        }
    }
}