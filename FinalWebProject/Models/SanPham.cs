namespace FinalWebProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        public int? MaLoaiSP { get; set; }

        public int? MaNSX { get; set; }

        [StringLength(255)]
        public string TenSP { get; set; }

        public string MoTa { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public int? GiaTien { get; set; }

        public int? SoLuongDaBan { get; set; }

        public int? LuotXem { get; set; }

        public int? TinhTrang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }
    }
}
