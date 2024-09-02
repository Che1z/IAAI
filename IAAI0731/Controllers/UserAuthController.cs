using IAAI0731.Filters;
using IAAI0731.Models;
using IAAI0731.Models.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using MvcPaging;
using System.Drawing.Printing;


namespace IAAI0731.Controllers
{
    [SidebarFilter]
    [FrontPageDirectionFilter]

    public class UserAuthController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserRegistrationVM userRegistrationVM)
        {
            ServiceExperience serviceExperience = new ServiceExperience();
            if (userRegistrationVM == null || !ModelState.IsValid)
            {
                ViewBag.Require = "請填寫所有必填項目";
                return View();
            }
            else if (userRegistrationVM.Captcha != "FRJN2" && !string.IsNullOrEmpty(userRegistrationVM.Captcha))
            {
                ViewBag.Error = "驗證碼錯誤";
                return View();
            }
            else
            {
                using (var db = new Model1())
                {
                    string salt = CreateSalt();

                    var userList = new UserList
                    {
                        Account = userRegistrationVM.User.Account,
                        Password = CreatePasswordHash(userRegistrationVM.User.Password, salt),
                        Salt = salt,
                        Name = userRegistrationVM.User.Name,
                        Gender = userRegistrationVM.User.Gender,
                        Birthday = userRegistrationVM.User.Birthday,
                        MembershipType = userRegistrationVM.User.MembershipType,
                        Address = userRegistrationVM.User.Address,
                        Email = userRegistrationVM.User.Email,
                        IsInternationalMember = userRegistrationVM.User.IsInternationalMember, // 如果為 null，則設置為 false
                        CurrentPosition = userRegistrationVM.User.CurrentPosition,
                        JobTitle = userRegistrationVM.User.JobTitle,
                        HighestEducation = userRegistrationVM.User.HighestEducation
                        // 添加其他必要的屬性
                    };
                    var ServiceExperience = new ServiceExperience
                    {
                        ServiceCompany = userRegistrationVM.ServiceExperience.ServiceCompany,
                        JobTitle = userRegistrationVM.ServiceExperience.JobTitle,
                        StartYear = userRegistrationVM.ServiceExperience.StartYear,
                        StartMonth = userRegistrationVM.ServiceExperience.StartMonth,
                        EndYear = userRegistrationVM.ServiceExperience.EndYear ?? null,
                        EndMonth = userRegistrationVM.ServiceExperience.EndMonth ?? null,
                    };

                    db.UserListEntities.Add(userList);
                    db.ServiceExperienceEntities.Add(ServiceExperience);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("LogIn", "UserAuth");
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInVM logInVM)
        {
            string hashPassword;
            if (logInVM == null || !ModelState.IsValid)
            {
                ViewBag.Require = "請填寫所有必填項目";
                return View();
            }
            else
            {
                using (var db = new Model1())
                {
                    var user = db.UserListEntities.FirstOrDefault(x => x.Account == logInVM.Account);
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
                            user.Birthday,
                            user.MembershipType,
                            user.Address,
                            user.Email,
                            user.IsInternationalMember,
                            user.CurrentPosition,
                            user.JobTitle,
                            user.HighestEducation
                        });

                        // 使用 Session 變數記錄登入狀態
                        Session["LoggedIn"] = true;
                        Session["UserId"] = user.Id;
                        Session["UserData"] = userData;


                        return RedirectToAction("Forum", "UserAuth");
                    }
                    else
                    {
                        ViewBag.Require = "帳號密碼錯誤";
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Forum(int? page)
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "UserAuth");
            }
            else
            {
                int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

                using (var db = new Model1())
                {
                    return View(db.ForumEntities
                        .Include(n => n.ForumReplies)  // 使用正確的導航屬性名稱
                        .OrderByDescending(p => p.CreateAt)
                        .ToPagedList(currentPageIndex, 3));
                }
            }
        }
        [HttpGet]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Post(Forum forum)
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "UserAuth");
            }
            else
            {

                string userData = Session["UserData"].ToString();
                JObject userObject = JObject.Parse(userData);
                string userName = userObject["Name"].ToString();

                using (var db = new Model1())
                {
                    Forum newPost = new Forum()
                    {
                        Title = forum.Title,
                        Content = forum.Content,
                        CreateAt = DateTime.Now,
                        UpdatedBy = userName,
                    };
                    db.ForumEntities.Add(newPost);
                    db.SaveChanges();

                    return RedirectToAction("Forum", "UserAuth");
                }
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Detail(int? page,int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            int pageSize = 3; // 每頁顯示3個項目
            int pageNumber = page.HasValue ? page.Value - 1 : 0;

            using (var db = new Model1())
            {
                var postData = db.ForumEntities
                                 .Where(x => x.Id == id)
                                 .FirstOrDefault();

                if (postData == null)
                {
                    return HttpNotFound();
                }

                // 根據回覆數量對回覆進行分頁
                var replyData = db.ForumReplyEntities
                                  .Where(x => x.PostNumber == id)
                                  .OrderByDescending(p => p.CreateAt)
                                  .ToPagedList(pageNumber, pageSize);

                // 包裝成 ViewModel 並傳遞到視圖
                var viewModel = new ForumDetailVM
                {
                    Forum = postData,
                    ForumReplies = replyData
                };

                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Reply(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Forum", "UserAuth");
            }
            else
            {
                using (var db = new Model1())
                {
                    var postData = db.ForumEntities.Where(x => x.Id == id).FirstOrDefault();
                    ForumReplyVM forumReplyVM = new ForumReplyVM()
                    {
                        forum = postData,
                    };
                    return View(forumReplyVM);
                }
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Reply(ForumReplyVM forumReplyvm, int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Forum", "UserAuth");
            }
            else
            {

                using (var db = new Model1())
                {
                    string userData = Session["UserData"].ToString();
                    JObject userObject = JObject.Parse(userData);
                    string userName = userObject["Name"].ToString();
                    ForumReply forumReply = new ForumReply()
                    {
                        PostNumber = id,
                        ReplyContent = forumReplyvm.forumReply.ReplyContent,
                        CreateAt = DateTime.Now,
                        UpdatedBy = userName,
                    };
                    db.ForumReplyEntities.Add(forumReply);
                    db.SaveChanges();
                    return RedirectToAction("Forum", "UserAuth");
                }
            }
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