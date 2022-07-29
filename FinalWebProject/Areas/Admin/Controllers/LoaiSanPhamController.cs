using ClosedXML.Excel;
using FinalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace FinalWebProject.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private HangTheThaoContext db = new HangTheThaoContext();
        // GET: Admin/LoaiSanPham
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            return View(db.LoaiSanPhams.OrderByDescending(x=>x.MaLoaiSP).ToPagedList(page,pageSize));
        }

        //import, export file excel
        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Mã loại sản phẩm"),
                                                     new DataColumn("Tên loại sản phẩm")
                                                     });

            var loaiSanPhams = from LoaiSanPham in db.LoaiSanPhams select LoaiSanPham;

            foreach (var lsp in loaiSanPhams)
            {
                dt.Rows.Add(lsp.MaLoaiSP, lsp.TenLoaiSP);
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

        // GET: Admin/LoaiSanPham/Details/5
        public ActionResult Details(int? id)
        {
            var thongTin = db.LoaiSanPhams.Find(id);
            if (thongTin == null)
            {
                return HttpNotFound();
            }
            return View(thongTin);
        }

        // GET: Admin/LoaiSanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSanPham/Create
        [HttpPost]
        public ActionResult Create(LoaiSanPham loaiSP)
        {
            try
            {
                // TODO: Add insert logic here
                db.LoaiSanPhams.Add(loaiSP);
                db.SaveChanges();
                return RedirectToAction("Index","LoaiSanPham");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/LoaiSanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var loaiSanPhams = db.LoaiSanPhams.ToList();
            LoaiSanPham loaiSP = new LoaiSanPham();
            foreach (LoaiSanPham lsp in loaiSanPhams)
            {
                if (lsp.MaLoaiSP == id)
                {
                    loaiSP = lsp;
                    break;
                }
            }
            return View();
        }

        // POST: Admin/LoaiSanPham/Edit/5
        [HttpPost, ActionName("Edit")]
       
        public ActionResult Edit(int id, string tenLoaiSP)
        {
            var loaiSanPhams = db.LoaiSanPhams.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (LoaiSanPham lsp in loaiSanPhams)
            {
                if (lsp.MaLoaiSP == id)
                {
                    lsp.TenLoaiSP = tenLoaiSP;
                    db.SaveChanges();
                    break;
                }
            }
            return View("Index", loaiSanPhams);
        }

        // GET: Admin/LoaiSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            var thongTin = db.LoaiSanPhams.Find(id);
            if (thongTin == null)
            {
                return HttpNotFound();
            }
            return View(thongTin);
        }

        // POST: Admin/LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var loaiSanPhams = db.LoaiSanPhams.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (LoaiSanPham lsp in loaiSanPhams)
            {
                if (lsp.MaLoaiSP == id)
                {
                    
                    db.LoaiSanPhams.Remove(lsp);
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Index","LoaiSanPham");
        }
    }
}
