using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IAAI0731.Filters
{
    public class UserInfoActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                string ticketUserData = ((FormsIdentity)(filterContext.HttpContext.User.Identity)).Ticket.UserData;
                JObject userData = JObject.Parse(ticketUserData);
                controller.ViewBag.UserName = (string)userData["Name"];
                controller.ViewBag.Email = (string)userData["Email"];
                controller.ViewBag.Id = (string)userData["Id"];
            }
            base.OnActionExecuting(filterContext);
        }
    }
}