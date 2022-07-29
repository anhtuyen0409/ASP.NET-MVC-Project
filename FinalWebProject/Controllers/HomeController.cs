using FinalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace FinalWebProject.Controllers
{
    public class HomeController : Controller
    {
        private HangTheThaoContext db = new HangTheThaoContext();
        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());

        }
        public ActionResult HienThiToanBoSanPham(int page = 1, int pageSize = 8)
        {
            return View(db.SanPhams.OrderBy(x => x.MaSP).ToPagedList(page, pageSize));
        }

        public ActionResult HienThiSanPhamTheoLoai(int? id)
        {
            var sp = (from s in db.SanPhams where s.MaLoaiSP == id select s).ToList();
            return View(sp);
        }
        public ActionResult HienThiSanPhamTheoNhaSanXuat(int? id)
        {
            var sp = (from s in db.SanPhams where s.MaNSX == id select s).ToList();
            return View(sp);
        }
        public ActionResult Details(int? id)
        {
            var thongTinSanPham = db.SanPhams.Find(id);
            if (thongTinSanPham == null)
            {
                return HttpNotFound();
            }
            return View(thongTinSanPham);
        }
        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
    }
}