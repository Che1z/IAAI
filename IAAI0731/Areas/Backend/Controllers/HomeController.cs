using IAAI0731.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Areas.Backend.Controllers
{
    [PermissionFilter]

    public class HomeController : Controller
    {
        // GET: Backend/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}