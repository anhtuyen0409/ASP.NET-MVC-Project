using FinalWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalWebProject.Controllers
{
    public class GioHangController : Controller
    {
        HangTheThaoContext db = new HangTheThaoContext();
        public List<GioHang> layGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        // GET: GioHang
        public ActionResult ThemGioHang(int maSP)
        {
            //lấy ra session giỏ hàng
            List<GioHang> lstGioHang = layGioHang();
            //kiểm tra sản phẩm này có tồn tại trong Session["GioHang"] chưa?
            GioHang sp = lstGioHang.Find(n=>n.iMaSP == maSP);
            if(sp == null)
            {
                sp = new GioHang(maSP);
                lstGioHang.Add(sp);
                return RedirectToAction("Index", "GioHang");
            }
            return View();
        }
        //xoá giỏ hàng
        public ActionResult XoaGioHang(int maSP)
        {
            //lấy giỏ hàng từ session
            List<GioHang>lstGioHang = layGioHang();
            //kiểm tra sản phẩm đã có trong session["giohang"] chưa?
            GioHang sp = lstGioHang.SingleOrDefault(n=>n.iMaSP == maSP);
            //nếu tồn tại thì cho sửa số lượng
            if(sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == maSP);
                return RedirectToAction("Index", "GioHang");
            }
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "GioHang");
        }
        //xoá tất cả giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = layGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        //cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int maSP, FormCollection f)
        {
            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = layGioHang();
            //kiểm tra sp đã có trong session["GioHang"] chưa?
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaSP == maSP);
            //nếu tồn tại sp thì cho sửa số lượng
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("Index", "GioHang");
        }
        //tổng số lượng
        private int TongSoLuong()
        {
            int tongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return tongSoLuong;
        }
        //tổng tiền
        private int TongTien()
        {
            int tongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                tongTien = lstGioHang.Sum(n => n.iThanhTien);
            }
            return tongTien;
        }
        //xây dựng trang giỏ hàng
        public ActionResult Index()
        {
            List<GioHang> lstGioHang = layGioHang();
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.tongsoluong = TongSoLuong();
            ViewBag.tongtien = TongTien();
            return View(lstGioHang);

        }
        //tạo partial view để hiển thị thông tin giỏ hàng 
        public ActionResult GioHangPartial()
        {
            ViewBag.tongsoluong = TongSoLuong();
            ViewBag.tongtien = TongTien();
            return PartialView();
        }
        
        //hiển thị view đặt hàng để cập nhật các thông tin cho đơn hàng
        [HttpGet]
        [Authorize]
        public ActionResult DatHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //lấy giỏ hàng từ Session
            List<GioHang> lstGioHang = layGioHang();
            ViewBag.tongsoluong = TongSoLuong();
            ViewBag.tongtien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(DonDatHang donDatHang, AspNetUser user)
        {
            //thêm đơn hàng
            try
            {
                //lấy giỏ hàng từ Session
                List<GioHang> lstGioHang = layGioHang();
                donDatHang.IdUser = user.Id;
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                foreach(var item in lstGioHang)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaDonHang = donDatHang.MaDonHang;
                    ctdh.MaSP = item.iMaSP;
                    ctdh.SoLuong = item.iSoLuong;
                    ctdh.DonGia = item.iGiatien;
                    db.ChiTietDonHangs.Add(ctdh);
                }
                db.SaveChanges();
                Session["GioHang"] = null;
                return RedirectToAction("XacNhanDonHang", "GioHang");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}