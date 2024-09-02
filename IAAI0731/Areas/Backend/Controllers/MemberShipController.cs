using IAAI0731.Filters;
using IAAI0731.Models;
using IAAI0731.Models.BackModel;
using IAAI0731.Models.ViewModel;
using MvcPaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IAAI0731.Areas.Backend.Controllers
{
    public class MemberShipController : Controller
    {
        // GET: Backend/MemberShip
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Models.BackModel.Membership membership)
        {
            Models.BackModel.Membership membershipInput = new Models.BackModel.Membership();
            if (membershipInput == null || !ModelState.IsValid)
            {
                ViewBag.Require = "請填寫所有必填項目";
                return View();
            }
            else
            {
                using (var db = new Model1())
                {
                    var findAccount = db.MembershipEntities.Where(a => a.Account == membership.Account).FirstOrDefault();
                    var findEmail = db.MembershipEntities.Where(a => a.Email == membership.Email).FirstOrDefault();
                    if (findAccount != null || findEmail != null)
                    {
                        ViewBag.Require = "帳號或信箱已註冊過";
                        return View();
                    }
                    else
                    {
                        string salt = CreateSalt();
                        var userList = new Models.BackModel.Membership
                        {
                            Account = membership.Account,
                            Password = CreatePasswordHash(membership.Password, salt),
                            Salt = salt,
                            Name = membership.Name,
                            Gender = membership.Gender,
                            Email = membership.Email,
                            CreateAt = DateTime.Now,
                            UpdatedBy = membership.Name,
                        };
                        db.MembershipEntities.Add(userList);
                        db.SaveChanges();

                        return View();
                    };
                }
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LogInVM logInVM)
        {
            LogInVM logInVM1 = new LogInVM();
            string hashPassword;
            if (logInVM1 == null || !ModelState.IsValid)
            {
                ViewBag.Require = "請填寫所有必填項目";
                return View();
            }
            else
            {
                using (var db = new Model1())
                {
                    var user = db.MembershipEntities.FirstOrDefault(x => x.Account == logInVM.Account);
                    if (user == null)
                    {
                        ViewBag.Require = "帳號密碼錯誤";
                        return View();
                    }

                    hashPassword = CreatePasswordHash(logInVM.Password, user.Salt);
                    if (user.Password == hashPassword)
                    {
                        ViewBag.Require = "登入成功";

                        string userData = JsonConvert.SerializeObject(new
                        {
                            user.Id,
                            user.Account,
                            user.Name,
                            user.Gender,
                            user.Email,
                            user.NewPermission,
                        });




                        //設定驗證票(夾帶資料，cookie 命名) // 需額外引入using System.Web.Configuration;
                        var tempt = SetAuthenTicket(userData, Convert.ToString(user.Id));

                        //將 Cookie 寫入回應
                        Response.Cookies.Add(tempt);
                        return RedirectToAction("Index", "MainPanel");
                    }
                    else
                    {
                        ViewBag.Require = "帳號密碼錯誤";
                        return View();
                    }
                }
            }
        }

        [Authorize]
        [PermissionFilter]
        public ActionResult PermissionManagement(int? page)
        {

            string ticketUserData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;

            string ticketUserID = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.Name;

            if (ticketUserID != "8")
            {
                return RedirectToAction("LogIn", "MemberShip");

            }
            else
            {
                int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
                using (var db = new Model1())
                {
                    return View(db.MembershipEntities
                        .Where(x => x.Id != 8)
                        .OrderByDescending(p => p.CreateAt)
                        .ToPagedList(currentPageIndex, 3));
                }
            }
        }

        [HttpGet]
        [PermissionFilter]
        public ActionResult PermissionDetail(int? id)
        {
            string ticketUserData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;

            string ticketUserID = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.Name;

            if (ticketUserID != "8")
            {
                return RedirectToAction("LogIn", "MemberShip");

            }
            else if (!id.HasValue)
            {
                return RedirectToAction("PermissionManagement", "MemberShip");
            }
            else
            {
                using (var db = new Model1())
                {
                    var userData = db.MembershipEntities.Where(x => x.Id == id).FirstOrDefault();
                    if (userData == null)
                    {
                        return RedirectToAction("PermissionManagement", "MemberShip");
                    }
                    else
                    {
                        ViewBag.DefaultPermission = userData.NewPermission;
                        ViewBag.PermissionData = GetPermissionData();
                        return View(userData);
                    }
                }
            }
        }

        [HttpPost]
        [PermissionFilter]
        public ActionResult PermissionDetail(int? newsId , IAAI0731.Models.BackModel.Membership membership)
        {
            using (var db = new Model1())
            {
                var userData = db.MembershipEntities.Where(x => x.Id == newsId).FirstOrDefault();
                if (userData == null)
                {
                    return RedirectToAction("PermissionManagement", "MemberShip");
                }
                else
                {

                    string newPermissions = membership.NewPermission;
                    userData.NewPermission = newPermissions;
                    db.SaveChanges();
                    return RedirectToAction("PermissionManagement", "MemberShip");
                }
            }

        }



        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "MemberShip");

        }
        StringBuilder _sbBuilder = new StringBuilder();

        // 遞迴方法
        public string GetPermissionData()
        {

            using (var db = new Model1())
            {
                //全抓
                List<Permission> permissions = db.PermissionEntities.ToList();

                //第一層
                List<Permission> roots = permissions.Where(x => x.ParentId == null).ToList();

                _sbBuilder.Append("[");

                foreach (Permission root in roots)//遍歷兄弟姊妹
                {
                    GetPermissionString(root);

                    if (!root.Equals(roots.Last()))//如果root跟roots的最後一個不一樣，結尾加上逗號
                    {
                        _sbBuilder.Append(",");
                    }
                }
                _sbBuilder.Append("]");

                return _sbBuilder.ToString();
            }
        }

        public void GetPermissionString(Permission node)
        {
            //塞兄弟姊妹的資料
            _sbBuilder.Append($"{{'id': '{node.Code}', 'text': '{node.Description}'");

            if (node.Permissions.Count > 0)//如果有孩子
            {
                _sbBuilder.Append(", 'children':[");

                foreach (var nodePermission in node.Permissions)//遍歷孩子
                {
                    GetPermissionString(nodePermission);//抓孩子

                    if (!nodePermission.Equals(node.Permissions.Last()))//如果nodePermission跟node.Permissions的最後一個不一樣，結尾加上逗號
                    {
                        _sbBuilder.Append(",");
                    }
                }

                _sbBuilder.Append("]");
            }
            _sbBuilder.Append("}");
        }






        // 建立一個指定長度的隨機salt值
        public static string CreateSalt(int saltLenght = 5)
        {
            //生成一個加密的隨機數
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);

            //返回一個Base64隨機數的字串
            return Convert.ToBase64String(buff);
        }

        // 返回密碼加鹽且雜湊後的字串(密碼 , salt)
        public static string CreatePasswordHash(string pwd, string strSalt)
        {
            //把密碼和Salt連起來
            string saltAndPwd = String.Concat(pwd, strSalt);
            //對密碼進行雜湊
            string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha256");
            //轉為小寫字元並擷取前16個字串
            hashenPwd = hashenPwd.ToLower().Substring(0, 16);
            //返回雜湊後的值
            return hashenPwd;
        }

        /// <summary>
        /// 設定認證票證並創建用戶的認證 Cookie。
        /// </summary>
        /// <param name="userData">要存儲在認證票證中的用戶特定數據。</param>
        /// <param name="userId">用戶的識別符。</param>
        /// <returns>包含加密認證票證的認證 Cookie。</returns>
        public static HttpCookie SetAuthenTicket(string userData, string userId)
        {
            // 聲明一個認證票證。
            // 注意：需要額外引入 using System.Web.Security;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(1), false, userData);

            // 加密認證票證。
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // 創建一個 Cookie。
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // 寫入 Cookie 到回應。
            return authenticationCookie;
        }

    }
}





