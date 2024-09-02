using IAAI0731.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcPaging;
using System.IO;
using IAAI0731.Filters;


namespace IAAI0731.Controllers
{
    [SidebarFilter]
    [FrontPageDirectionFilter]

    public class LibraryController : Controller
    {
        // GET: Library
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            using (var db = new Model1())
            {
                return View(db.KnowledgeEntities 
                    .Include(n=>n.MembershipsFK)// 使用正確的導航屬性名稱
                    .OrderByDescending(p => p.CreateAt)
                    .ToPagedList(currentPageIndex, 5));
            }
        }

        public FileResult Download(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            using (var db = new Model1())
            {              
                var knowledgeData = db.KnowledgeEntities.Where(x => x.Id == id).FirstOrDefault();
                var filePath = knowledgeData.FilePath;
                var fileName = knowledgeData.Title + Path.GetExtension(filePath); // 用標題作為文件名，並附上原文件擴展名
                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                // 返回文件
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
        }
    }
}