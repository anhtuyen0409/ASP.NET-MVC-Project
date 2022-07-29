using ClosedXML.Excel;
using FinalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalWebProject.Areas.Admin.Controllers
{
    public class NhaSanXuatController : Controller
    {
        private HangTheThaoContext db = new HangTheThaoContext();
        // GET: Admin/NhaSanXuat
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.NhaSanXuats.ToList());
        }


        //import, export file excel
        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Mã nhà sản xuất"),
                                                     new DataColumn("Tên nhà sản xuất")
                                                     });

            var nhaSanXuats = from NhaSanXuat in db.NhaSanXuats select NhaSanXuat;

            foreach (var nsx in nhaSanXuats)
            {
                dt.Rows.Add(nsx.MaNSX, nsx.TenNSX);
            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }

        // GET: Admin/NhaSanXuat/Details/5
        public ActionResult Details(int? id)
        {
            var thongTin = db.NhaSanXuats.Find(id);
            if (thongTin == null)
            {
                return HttpNotFound();
            }
            return View(thongTin);
        }

        // GET: Admin/NhaSanXuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaSanXuat/Create
        [HttpPost]
        public ActionResult Create(NhaSanXuat nhaSanXuat)
        {
            try
            {
                db.NhaSanXuats.Add(nhaSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index", "NhaSanXuat");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/NhaSanXuat/Edit/5
        public ActionResult Edit(int id)
        {
            var nhasanxuats = db.NhaSanXuats.ToList();
            NhaSanXuat nhaSX = new NhaSanXuat();
            foreach (NhaSanXuat nsx in nhasanxuats)
            {
                if (nsx.MaNSX == id)
                {
                    nhaSX = nsx;
                    break;
                }
            }
            return View();
        }

        // POST: Admin/NhaSanXuat/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int id, string tenNSX)
        {
            var nhasanxuats = db.NhaSanXuats.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (NhaSanXuat nsx in nhasanxuats)
            {
                if (nsx.MaNSX == id)
                {
                    nsx.TenNSX = tenNSX;
                    db.SaveChanges();
                    break;
                }
            }
            return View("Index", nhasanxuats);
        }

        // GET: Admin/NhaSanXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            var thongTin = db.NhaSanXuats.Find(id);
            if (thongTin == null)
            {
                return HttpNotFound();
            }
            return View(thongTin);
        }

        // POST: Admin/NhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var nhasanxuats = db.NhaSanXuats.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (NhaSanXuat nsx in nhasanxuats)
            {
                if (nsx.MaNSX == id)
                {
                    db.NhaSanXuats.Remove(nsx);
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Index", "NhaSanXuat");
        }
    }
}
