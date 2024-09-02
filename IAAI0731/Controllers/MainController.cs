using IAAI0731.Filters;
using IAAI0731.Models;
using IAAI0731.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Controllers
{
    [SidebarFilter]
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            using (var db = new Model1())
            {
                var newsItems = db.NewsEntities.OrderByDescending(x => x.CreateAt).ToList().Take(4);
                var newsPhotos = newsItems.SelectMany(n => n.Photos).ToList();
                var viewModel = new IndexNews
                {
                    NewsItems = newsItems,
                    NewsPhotos = newsPhotos
                };
                return View(viewModel);
            }
        }
    }
}