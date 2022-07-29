using ClosedXML.Excel;
using FinalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace FinalWebProject.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private HangTheThaoContext db = new HangTheThaoContext();
        // GET: Admin/SanPham
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int page=1, int pageSize=10)
        {
            return View(db.SanPhams.OrderBy(x=>x.MaSP).ToPagedList(page,pageSize));
        }


        //import, export file excel
        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Mã sản phẩm"),
                                                     new DataColumn("Loại sản phẩm"),
                                                     new DataColumn("Nhà sản xuất"),
                                                     new DataColumn("Tên sản phẩm"),
                                                     new DataColumn("Mô tả"),
                                                     new DataColumn("Hình ảnh"),
                                                     new DataColumn("Giá tiền")
                                                     });

            var sanPhams = from SanPham in db.SanPhams select SanPham;

            foreach (var sp in sanPhams)
            {
                dt.Rows.Add(sp.MaSP, sp.LoaiSanPham.TenLoaiSP, sp.NhaSanXuat.TenNSX, sp.TenSP, sp.MoTa, sp.HinhAnh, sp.GiaTien);
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

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            var thongTin = db.SanPhams.Find(id);
            if (thongTin == null)
            {
                return HttpNotFound();
            }
            return View(thongTin);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        public ActionResult Create(SanPham sanPham)
        {
            try
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index", "SanPham");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            var sanPhams = db.SanPhams.ToList();
            SanPham sanPham = new SanPham();
            foreach (SanPham sp in sanPhams)
            {
                if (sp.MaSP == id)
                {
                    sanPham = sp;
                    break;
                }
            }
            return View();
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(int id, int maLoaiSP, int maNSX, string ten, string moTa, string hinhAnh, int giaTien, DateTime ngayCapNhat)
        {
            var sanPhams = db.SanPhams.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (SanPham sp in sanPhams)
            {
                if (sp.MaSP == id)
                {
                    sp.MaLoaiSP = maLoaiSP;
                    sp.MaNSX = maNSX;
                    sp.TenSP = ten;
                    sp.MoTa = moTa;
                    sp.HinhAnh = hinhAnh;
                    sp.GiaTien = giaTien;
                    sp.NgayCapNhat = ngayCapNhat;
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Index", "SanPham");
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            var thongTin = db.SanPhams.Find(id);
            if (thongTin == null)
            {
                return HttpNotFound();
            }
            return View(thongTin);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var sanPhams = db.SanPhams.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (SanPham sp in sanPhams)
            {
                if (sp.MaSP == id)
                {

                    db.SanPhams.Remove(sp);
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Index", "SanPham");
        }
    }
}
