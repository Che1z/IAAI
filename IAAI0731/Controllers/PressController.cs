using IAAI0731.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Data.Entity;
using MvcPaging;
using IAAI0731.Filters;

namespace IAAI0731.Controllers
{
    [SidebarFilter]
    [FrontPageDirectionFilter]

    public class PressController : Controller
    {
        // GET: Press
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult news(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            using (var db = new Model1())
            {
                return View(db.NewsEntities
                    .Include(n => n.Photos)  // 使用正確的導航屬性名稱
                    .OrderByDescending(p => p.CreateAt)
                    .ToPagedList(currentPageIndex, 3));
            }
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            else
            {
                using (var db = new Model1())
                {
                    var newsData = db.NewsEntities.Where(n => n.Id == id).FirstOrDefault();
                    if (newsData == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return View(newsData);
                    }
                }
            }

        }
    }
}