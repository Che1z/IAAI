using IAAI0731.Filters;
using IAAI0731.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Controllers
{
    [FrontPageDirectionFilter]

    public class IntroductionController : Controller
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AssociationIntro() {
            using (var db = new Model1()) {
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
    }
}