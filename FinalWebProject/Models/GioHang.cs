using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.Models
{
    public class GioHang
    {
        HangTheThaoContext db = new HangTheThaoContext();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public int iGiatien { get; set; }
        public int iSoLuong { get; set; }
        public int iThanhTien { get { return iSoLuong * iGiatien; } }

        //khởi tạo giỏ hàng theo mã sp được truyền vào với số lượng mặc định là 1
        public GioHang(int maSP)
        {
            iMaSP = maSP;
            SanPham sp = db.SanPhams.Single(n=>n.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            sHinhAnh = sp.HinhAnh;
            iGiatien = int.Parse(sp.GiaTien.ToString());
            iSoLuong = 1;
        }

    }
}