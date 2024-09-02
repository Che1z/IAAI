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
    public class KnowledgeController : Controller
    {
        // GET: Backend/Kowledge
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Knowledge knowledge)
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
        [UserInfoActionFilter]
        [Authorize]
        public ActionResult Create(HttpPostedFileBase file, string Title)
        {
            if (file == null)
            {
                ModelState.AddModelError("Invalid Input", "請選擇檔案");
                return View();
            }
            else
            {
                using (var db = new Model1())
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadFiles"), fileName);
                    file.SaveAs(path);
                    Knowledge newKnowledge = new Knowledge()
                    {
                        Title = Title,
                        FilePath = path,
                        MemberId = int.Parse(ViewBag.Id),
                        UpdatedBy = ViewBag.UserName,
                        CreateAt = DateTime.Now,
                    };
                    db.KnowledgeEntities.Add(newKnowledge);
                    db.SaveChanges();
                    return RedirectToAction("Edit", "Knowledge");

                }
            }
        }

        //刪除
        [HttpPost]
        public ActionResult Delete(IEnumerable<int> deleteList)
        {

            using (var db = new Model1())
            {
                var list = db.KnowledgeEntities.Where(x => deleteList.Contains(x.Id)).ToList();
                db.KnowledgeEntities.RemoveRange(list);
                db.SaveChanges();
                return RedirectToAction("Edit", "Knowledge");
            }
        }

        //預覽pdf檔案
        public ActionResult PreviewFile(string filePath, string filName)
        {
            if (System.IO.File.Exists(filePath))
            {
                Response.AddHeader("Content-Disposition", "inline; filename=\"" + filName + "\"");
                Response.AddHeader("Content-Type", "application/pdf");
                return File(filePath, "application/pdf", Path.GetFileName(filePath));
            }
            return HttpNotFound();
        }
    }
}