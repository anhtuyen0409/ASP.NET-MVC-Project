namespace FinalWebProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        public int MaCTDH { get; set; }

        public int? MaDonHang { get; set; }

        public int? MaSP { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGia { get; set; }
    }
}
