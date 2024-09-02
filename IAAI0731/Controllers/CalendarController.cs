using IAAI0731.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Controllers
{
    [SidebarFilter]
    [FrontPageDirectionFilter]

    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult showCalendar()
        {
            return View();
        }
    }
}