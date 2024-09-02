using IAAI0731.Filters;
using IAAI0731.Models;
using IAAI0731.Models.BackModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI0731.Areas.Backend.Controllers
{
    [PermissionFilter]

    public class AboutUsController : Controller
    {
        // GET: Backend/AboutUs
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

        [HttpPost]
        [UserInfoActionFilter]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult AssociationIntro(AboutUs aboutUs)
        {
            using (var db = new Model1())
            {
                AboutUs newData = new AboutUs()
                {
                    Title = aboutUs.Title,
                    Content = aboutUs.Content,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    UpdatedBy = ViewBag.UserName
                };
                db.AboutUsEntities.Add(newData);
                db.SaveChanges();
            }
            return RedirectToAction("AssociationIntro", "AboutUs");
        }

        [HttpGet]
        public ActionResult CreateAssociationExpert()
        {
            return View();
        }

        [UserInfoActionFilter]
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateAssociationExpert(HttpPostedFileBase file, Expert expert)
        {
            if (file == null)
            {
                ModelState.AddModelError("Invalid Input", "請選擇檔案");
                return View();
            }
            else
            {
                var relativePath = $"/UploadImages/expert/";
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
                    Expert expertInput = new Expert()
                    {
                        Name = expert.Name,
                        CurrentJobDescription = expert.CurrentJobDescription,
                        ImagePath = relativePath + fileName,
                        ImageName = fileName,
                        AdditionalInfo = expert.AdditionalInfo,
                        CreateAt = DateTime.Now,
                        UpdatedBy = ViewBag.UserName,
                        UpdateAt = DateTime.Now
                    };
                    db.ExpertEntities.Add(expertInput);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("ListAssociationExpert", "AboutUs");
        }

        [HttpGet]
        public ActionResult ListAssociationExpert()
        {
            using (var db = new Model1())
            {
                IEnumerable<Expert> expertList = db.ExpertEntities.ToList();
                if (expertList != null)
                {
                    return View(expertList);
                }
                else
                {
                    return View();
                }
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult Detail(string expertId)
        {
            if (expertId == null)
            {
                return HttpNotFound();
            }

            int expertNumber = int.Parse(expertId);

            using (var db = new Model1())
            {
                var expertData = db.ExpertEntities.Where(x => x.Id == expertNumber).FirstOrDefault();
                if (expertData != null)
                {
                    return View(expertData);
                }
                else
                {
                    return View();
                }

            }

        }

        [UserInfoActionFilter]
        [HttpPost]
        [Authorize]
        public ActionResult Detail(HttpPostedFileBase file, Expert expert)
        {
            int idNumber = expert.Id;
           
            using (var db = new Model1())
            {
                var expertData = db.ExpertEntities.Where(x => x.Id == idNumber).FirstOrDefault();
                expertData.UpdateAt = DateTime.Now;
                expertData.Name = expert.Name;
                expertData.CurrentJobDescription = expert.CurrentJobDescription;
                if (file != null)
                {
                    var relativePath = $"/UploadImages/expert/";
                    var physicalPath = Server.MapPath("~" + relativePath);
                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(physicalPath);
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(physicalPath, fileName);
                    file.SaveAs(fullPath);
                    expertData.ImagePath = relativePath + fileName;
                    expertData.ImageName = fileName;
                }
                expertData.UpdatedBy = ViewBag.UserName;
                db.SaveChanges();

            }
            return RedirectToAction("ListAssociationExpert","AboutUs");
        }

        public ActionResult Delete(int expertId)
        {
            using (var db = new Model1())
            {
                var expertData = db.ExpertEntities.Where(x => x.Id == expertId).FirstOrDefault();
                db.ExpertEntities.Remove(expertData);
                db.SaveChanges();


                return RedirectToAction("ListAssociationExpert", "AboutUs");
            }
        }
    }
}