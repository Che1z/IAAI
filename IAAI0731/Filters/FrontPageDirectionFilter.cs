using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Filters
{
    public class FrontPageDirectionFilter : ActionFilterAttribute
    {
        private Dictionary<string, Dictionary<string, string>> dictionaries = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, string> ControllerDictionary = new Dictionary<string, string>();
        public FrontPageDirectionFilter()
        {
            dictionaries.Add("AboutUsIntro", new Dictionary<string, string>());
            dictionaries["AboutUsIntro"].Add("AssociationIntro", "協會介紹");
            dictionaries["AboutUsIntro"].Add("Detail", "專家介紹");
            dictionaries["AboutUsIntro"].Add("Master", "專家介紹");

            dictionaries.Add("Calendar", new Dictionary<string, string>());
            dictionaries["Calendar"].Add("showCalendar", "協會行事曆");

            dictionaries.Add("Library", new Dictionary<string, string>());
            dictionaries["Library"].Add("List", "知識庫下載");

            dictionaries.Add("Press", new Dictionary<string, string>());
            dictionaries["Press"].Add("Detail", "訊息發布");
            dictionaries["Press"].Add("news", "訊息發布");

            dictionaries.Add("UserAuth", new Dictionary<string, string>());
            dictionaries["UserAuth"].Add("Detail", "討論區");
            dictionaries["UserAuth"].Add("Forum", "討論區");
            dictionaries["UserAuth"].Add("LogIn", "登入");
            dictionaries["UserAuth"].Add("SignUp", "註冊");
            dictionaries["UserAuth"].Add("Reply", "討論區");
            dictionaries["UserAuth"].Add("Post", "討論區");

            dictionaries.Add("Contact", new Dictionary<string, string>());
            dictionaries["Contact"].Add("ContactForm", "聯絡我們");


            ControllerDictionary.Add("AboutUsIntro", "關於我們");
            ControllerDictionary.Add("Calendar", "日歷");
            ControllerDictionary.Add("Library", "知識庫");
            ControllerDictionary.Add("Press", "最新消息");
            ControllerDictionary.Add("UserAuth", "會員專區");
            ControllerDictionary.Add("Contact", "聯絡我們");
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 使用 dictionaries，並根據需要設置 ViewBag 或其他處理邏輯
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            // 確認 controllerName 和 actionName 是否在字典中，並進行處理
            string PageDirection = $"<li>首頁</li>\r\n" +
                $"<li>{ControllerDictionary[controllerName]}</li>\r\n" +
                $"<li><a href=\"#\"> {dictionaries[controllerName][actionName]}</a></li>";

            filterContext.Controller.ViewBag.pageDirection = PageDirection;
        }
    }
}