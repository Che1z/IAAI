using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Filters
{
    public class SidebarFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var action = filterContext.RouteData.Values["action"].ToString().ToLower();
            var sidebarItems = new List<SidebarItem>();

            switch (controller)
            {
                case "aboutusintro":
                    sidebarItems.Add(new SidebarItem
                    {
                        Title = "關於我們",
                        Text = "協會介紹",
                        Url = "/AboutUsIntro/AssociationIntro",
                        IsActive = action == "associationintro"
                    });
                    sidebarItems.Add(new SidebarItem
                    {
                        Title = "關於我們",
                        Text = "專家介紹",
                        Url = "/AboutUsIntro/Master",
                        IsActive = action == "master" || action == "detail"
                    });
                    break;
                case "press":
                    sidebarItems.Add(new SidebarItem
                    {
                        Title = "訊息發佈",
                        Text = "最新消息",
                        Url = "/Press/news",
                        IsActive = true
                    });
                    break;
                case "library":
                    sidebarItems.Add(new SidebarItem
                    {
                        Title = "知識庫",
                        Text = "知識庫下載",
                        Url = "/Library/List",
                        IsActive = true
                    });
                    break;
                case "calendar":
                    sidebarItems.Add(new SidebarItem 
                    { 
                        Title = "行事曆",
                        Text = "協會行事曆",
                        Url = "/Calendar/showCalendar",
                        IsActive = true
                    });
                    break;
                case "contact":
                    sidebarItems.Add(new SidebarItem
                    {
                        Title = "聯絡我們",
                        Text = "聯絡我們",
                        Url = "/Contact/ContactForm",
                        IsActive = true
                    });
                    break;
                case "userauth":
                    if (filterContext.HttpContext.Session["LoggedIn"] == null)
                    {
                        // 用戶未登入
                        sidebarItems.Add(new SidebarItem
                        {
                            Title = "會員專區",
                            Text = "登入",
                            Url = "/UserAuth/LogIn",
                            IsActive = action == "login"
                        });
                        sidebarItems.Add(new SidebarItem
                        {
                            Title = "會員專區",
                            Text = "註冊",
                            Url = "/UserAuth/SignUp",
                            IsActive = action == "signup"
                        });
                    }
                    else
                    {
                        // 用戶已登入
                        sidebarItems.Add(new SidebarItem
                        {
                            Title = "會員專區",
                            Text = "討論區",
                            Url = "/UserAuth/Forum",
                            IsActive = action == "forum"
                        });
                    }
                    break;
            }

            filterContext.Controller.ViewBag.SidebarItems = sidebarItems;
        }


    }
}
public class SidebarItem
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
}