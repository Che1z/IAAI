using IAAI0731.Filters;
using IAAI0731.Models;
using IAAI0731.Models.BackModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Controllers
{
    [SidebarFilter]
    [FrontPageDirectionFilter]
    public class AboutUsIntroController : Controller
    {
        // GET: AboutUsIntro
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AssociationIntro()
        {
            using (var db = new Model1())
            {
                var aboutUsData = db.AboutUsEntities.OrderByDescending(x => x.UpdateAt).FirstOrDefault();
                if (aboutUsData == null)
                {
                    return View();
                }
                else
                {
                    return View(aboutUsData);

                }
            }
        }

        [HttpGet]
        public ActionResult Master() {
            using(var db = new Model1())
            {
                IEnumerable<Expert> expertList = db.ExpertEntities.ToList();
                if(expertList != null)
                {
                    return View(expertList); 
                }
                else
                {
                    return View();
                }

            }
        }
        public ActionResult Detail(string Id)
        {
            if(Id == null)
            {
                return HttpNotFound();
            }
            using(var db = new Model1())
            {
                int IdNumber = int.Parse(Id);
                var expertData = db.ExpertEntities.Where(x => x.Id == IdNumber).FirstOrDefault();
                if(expertData != null) { 
                return View(expertData);
                }
                else
                {
                    return View();
                }
            }
        }
    }
}