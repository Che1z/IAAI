using IAAI0731.Filters;
using IAAI0731.Models;
using IAAI0731.Models.BackModel;
using IAAI0731.Models.BackModel.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Areas.Backend.Controllers
{
    [PermissionFilter]
    public class NewsController : Controller
    {
        // GET: Backend/News
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [UserInfoActionFilter]
        [Authorize]
        public ActionResult Create(HttpPostedFileBase file, NewsVM newsVM)
        {
            var relativePath = $"/UploadImages/userfiles/{ViewBag.Id}/newsCover/";
            var physicalPath = Server.MapPath("~" + relativePath);
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            var fileName = Path.GetFileName(file.FileName);
            var fullPath = Path.Combine(physicalPath, fileName);
            file.SaveAs(fullPath);
            using (var db = new Model1())
            {
                News news = new News()
                {
                    Title = newsVM.news.Title,
                    Description = newsVM.news.Description,
                    Content = newsVM.news.Content,
                    MemberId = int.Parse(ViewBag.Id),
                    UpdatedBy = ViewBag.UserName,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                db.NewsEntities.Add(news);
                db.SaveChanges();
                NewsPhoto newsPhoto = new NewsPhoto()
                {
                    FileName = fileName,
                    FilePath = relativePath + fileName,
                    NewsId = news.Id,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    UpdatedBy = ViewBag.UserName
                };

                db.NewsPhotoEntities.Add(newsPhoto);
                db.SaveChanges();
                return RedirectToAction("Create", "News");
            }
        }

        [HttpGet]
        [Authorize]

        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        [Authorize]

        public ActionResult List(  NewsVM newsVM)
        {
            return View();
        }

        [HttpGet]
        [Authorize]

        public ActionResult Detail(string newsId)
        {
            //只有透過List才能正常傳入newsId值，藉此判斷是否透過正確流程導向至Detail頁
            if (newsId == null)
            {
                return HttpNotFound();
            }
            int newsNumber = int.Parse(newsId);
            ViewBag.newsId = newsNumber;
            using (var db = new Model1())
            {
                // 根據 newsId 從資料庫中抓取新聞數據
                var newsData = db.NewsEntities.Where(x => x.Id == newsNumber).FirstOrDefault();
                var newsPhotoData = db.NewsPhotoEntities.Where(x => x.NewsId == newsData.Id).FirstOrDefault();
                if (newsData == null)
                {
                    return HttpNotFound(); // 如果找不到新聞項目，返回 404 錯誤
                }

                // 將新聞數據轉換為View模型
                var newsVM = new NewsVM
                {
                    news = newsData,
                    newsPhoto = newsPhotoData
                };

                return View(newsVM); // 傳遞視圖模型給視圖
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]

        public ActionResult Detail(HttpPostedFileBase file,NewsVM newsVM, string Id, string newsId, string UserName)
        {
            int newsNumber = int.Parse(newsId);
           
            using (var db = new Model1())
            {
                var newsData = db.NewsEntities.Where(x => x.Id == newsNumber).FirstOrDefault();
                newsData.Title = newsVM.news.Title;
                newsData.Content = newsVM.news.Content;
                newsData.Description = newsVM.news.Description;
                newsData.UpdateAt = DateTime.Now;
                newsData.UpdatedBy = UserName;
                db.SaveChanges();
                if (file != null)
                {
                    var relativePath = $"/UploadImages/userfiles/{Id}/newsCover/";
                    var physicalPath = Server.MapPath("~" + relativePath);
                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(physicalPath);
                    }

                    var fileName = Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(physicalPath, fileName);
                    file.SaveAs(fullPath);
                    var newsPhotoData = db.NewsPhotoEntities.Where(x => x.NewsId == newsData.Id).FirstOrDefault();
                    newsPhotoData.FileName = fileName;
                    newsPhotoData.FilePath = relativePath + fileName;
                    newsPhotoData.UpdateAt = DateTime.Now;
                    newsPhotoData.UpdatedBy = UserName;
                    db.SaveChanges();
                }               
            }
            return RedirectToAction("List", "News");
        }

        public ActionResult Delete(int newsId)
        {
            using (var db = new Model1())
            {
                var newsPhotoData = db.NewsPhotoEntities.Where(x => x.NewsId == newsId).FirstOrDefault();
                db.NewsPhotoEntities.Remove(newsPhotoData);
                db.SaveChanges();

                var newsData = db.NewsEntities.Where(x => x.Id == newsId).FirstOrDefault();
                db.NewsEntities.Remove(newsData);
                db.SaveChanges();
                return RedirectToAction("List", "News");
            }
        }

    }
}