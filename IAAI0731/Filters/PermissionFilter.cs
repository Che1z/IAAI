using IAAI0731.Models;
using IAAI0731.Models.BackModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IAAI0731.Filters
{
    public class PermissionFilter : ActionFilterAttribute
    {
        private Model1 db = new Model1();

        //全域變數組字串
        StringBuilder _sbBuilder = new StringBuilder();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //取得使用者識別
            int _ticketUserID = Convert.ToInt32(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name);

            //判斷使用者是否有該權限
            //取得目前URL的controllerName及actionName
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            //判斷使用者是否存在
            Boolean userIsExist = db.MembershipEntities.Any(m => m.Id == _ticketUserID);
            if (userIsExist == false)
            {
                //登出驗證表單
                FormsAuthentication.SignOut();
            }

            //取得使用者擁有的權限
            var userPermissionsCode = db.MembershipEntities.Where(m => m.Id == _ticketUserID).Select(m => m.NewPermission).FirstOrDefault()?.Split(',');//例如： ['A01','A02']

            Boolean userHasRoutePermission = false;
            if (userPermissionsCode != null)
            {
                //取出使用者擁有的權限的控制器名稱
                var userPermissionsNames = db.PermissionEntities
                    .Where(p => userPermissionsCode.Contains(p.Code))
                    .Select(p => new { Controller = p.ControllerName, ActionName = p.ActionName })
                    .ToList();

                userPermissionsNames.Add(new { Controller = "MainPanel", ActionName = "Index" });
                userPermissionsNames.Add(new { Controller = "MemberShip", ActionName = "PermissionDetail" });

                //遍歷使用者擁有的權限的控制器名稱
                foreach (var userPermissionsControllerName in userPermissionsNames)
                {
                    if (userPermissionsControllerName.Controller == controllerName && userPermissionsControllerName.ActionName == actionName)
                    {
                        userHasRoutePermission = true;
                        break;
                    }
                }
            }

            ////沒有權限
            if (userHasRoutePermission == false)
            {
                //登出驗證表單
                FormsAuthentication.SignOut();

                //跳回Home頁
                filterContext.Result = new RedirectResult("~/Backend/MainPanel/Index");
            }

            //有權限

            Dictionary<string, Dictionary<string, string>> dictionaries = new Dictionary<string, Dictionary<string, string>>();
            dictionaries.Add("AboutUs", new Dictionary<string, string>());
            dictionaries["AboutUs"].Add("ListAssociationExpert", "編輯名單");
            dictionaries["AboutUs"].Add("CreateAssociationExpert", "新增名單");
            dictionaries["AboutUs"].Add("AssociationIntro", "協會介紹");

            dictionaries.Add("Knowledge", new Dictionary<string, string>());
            dictionaries["Knowledge"].Add("Create", "新增");
            dictionaries["Knowledge"].Add("Edit", "編輯");

            dictionaries.Add("News", new Dictionary<string, string>());
            dictionaries["News"].Add("Create", "新增");
            dictionaries["News"].Add("Edit", "編輯");
            dictionaries["News"].Add("List", "列表");

            dictionaries.Add("MemberShip", new Dictionary<string, string>());
            dictionaries["MemberShip"].Add("PermissionManagement", "會員管理");
            dictionaries["MemberShip"].Add("PermissionDetail", "會員管理");

            dictionaries.Add("MainPanel", new Dictionary<string, string>());
            dictionaries["MainPanel"].Add("Index", "首頁");

            Dictionary<string, string> Controllerdictionaries = new Dictionary<string, string>();
            Controllerdictionaries.Add("AboutUs", "關於我們");
            Controllerdictionaries.Add("Knowledge", "知識庫");
            Controllerdictionaries.Add("News", "新聞管理");
            Controllerdictionaries.Add("MemberShip", "會員管理");
            Controllerdictionaries.Add("MainPanel", "首頁");

            string title = "<h5 class=\"mb-0\">預設標題</h5>\r\n";
            string link =
                $"<li class=\"breadcrumb-item\"><a href=\"/Backend/MainPanel/Index\">首頁</a></li>\r\n" +
                $"<li class=\"breadcrumb-item\"><a href=\"javascript:void(0);\">未知控制器</a></li>\r\n" +
                $"<li class=\"breadcrumb-item\" aria-current=\"page\">未知操作</li>";

            // 取得 Title 值
            if (dictionaries.ContainsKey(controllerName) && dictionaries[controllerName].ContainsKey(actionName))
            {
                title = $"<h5 class=\"mb-0\">{dictionaries[controllerName][actionName]}</h5>\r\n";
            }


            // 取得 Link 中的控制器部分
            if (Controllerdictionaries.ContainsKey(controllerName))
            {
                link =
                    $"<li class=\"breadcrumb-item\"><a href=\"/Backend/MainPanel/Index\">首頁</a></li>\r\n" +
                    $"<li class=\"breadcrumb-item\"><a href=\"javascript:void(0);\">{Controllerdictionaries[controllerName]}</a></li>\r\n" +
                    $"<li class=\"breadcrumb-item\" aria-current=\"page\">未知操作</li>";
            }

            // 取得Link 中的ActionName
            if (dictionaries.ContainsKey(controllerName) && dictionaries[controllerName].ContainsKey(actionName))
            {
                link =
                    $"<li class=\"breadcrumb-item\"><a href=\"/Backend/MainPanel/Index\">首頁</a></li>\r\n" +
                    $"<li class=\"breadcrumb-item\"><a href=\"javascript:void(0);\">{Controllerdictionaries[controllerName]}</a></li>\r\n" +
                    $"<li class=\"breadcrumb-item\" aria-current=\"page\">{dictionaries[controllerName][actionName]}</li>";
            }

            filterContext.Controller.ViewBag.Title = title;
            filterContext.Controller.ViewBag.Link = link;
            //以下組出Menu字串

            _sbBuilder.Clear();
            //全抓
            List<Permission> permissions = db.PermissionEntities.ToList();

            //第一層
            List<Permission> roots = permissions.Where(x => x.ParentId == 1).ToList();//['A', 'B', 'C']

            //遍歷第一層
            foreach (var root in roots)
            {
                if (userPermissionsCode != null && userPermissionsCode.Any(p => p.StartsWith(root.Code)))
                {
                    if (root.Permissions.Count > 0)//如果有孩子
                    {
                        //_sbBuilder.Append($"<li class='submenu'><a href = '#'><i class='glyphicon glyphicon-list'></i>{root.Description}<span class='caret pull-right'></span></a>");
                        _sbBuilder.Append($"<li class='pc-item pc-hasmenu'>" +
                            $"<a href='#'class='pc-link'>" +
                            $"<span class='pc-micon'>{root.Icon}</span>"
                            + $"<span class=pc-mtext>{root.Description}</span>"
                            + $" <span class=\"pc-arrow\"><i class=\"ti ti-chevron-right\"></i></span\r\n></a>");

                        // 處理孩子
                        GetPermissionString(root);


                        _sbBuilder.Append($"</li>");
                    }
                    else//如果沒孩子
                    {
                        _sbBuilder.Append($" <li class='pc-item'><a href='{root.Url}'class='pc-link'>" +
                            $"<span class='pc-micon'>{root.Icon}</span>"
                            + $"<span class='pc-mtext'>{root.ControllerName}</span>");
                    }
                }
            }
           
            filterContext.Controller.ViewBag.Menu = _sbBuilder.ToString();
            
        }


      

        private void GetPermissionString(Permission node)
        {
            //取得使用者識別
            int _ticketUserID = Convert.ToInt32(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name);

            //取得使用者擁有的權限
            var userPermissions = db.MembershipEntities.Where(m => m.Id == _ticketUserID).Select(m => m.NewPermission).FirstOrDefault()?.Split(',');//例如： ['A01','A02']

            _sbBuilder.Append("<ul class= pc-submenu style= display: none;>");

            foreach (var nodeKid in node.Permissions)
            {
                if (userPermissions.Any(p => p.StartsWith(nodeKid.Code)))
                {
                    if (nodeKid.Permissions.Count > 0)//如果有孩子
                    {
                        //_sbBuilder.Append($"<li class='submenu'><a href = '#'>{nodeKid.Description}<span class='caret pull-right'></span></a>");
                        _sbBuilder.Append($"<li class='pc-item'><a class='pc-link' href='{nodeKid.Url}'>{nodeKid.Description}</a>");
                        GetPermissionString(nodeKid);
                        _sbBuilder.Append("</li>");
                    }
                    else//如果沒孩子
                    {
                        _sbBuilder.Append($"<li class='pc-item'><a class='pc-link' href='{nodeKid.Url}'>{nodeKid.Description}</a></li>");
                    }
                }
            }

            _sbBuilder.Append("</ul>");
        }
    }
}