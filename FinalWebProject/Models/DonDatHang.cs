namespace FinalWebProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [Key]
        public int MaDonHang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayGiao { get; set; }

        public int? TinhTrangGiaoHang { get; set; }

        public int? DaThanhToan { get; set; }

        [StringLength(128)]
        public string IdUser { get; set; }

        [StringLength(255)]
        public string TenKH { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(11)]
        public string SDT { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
