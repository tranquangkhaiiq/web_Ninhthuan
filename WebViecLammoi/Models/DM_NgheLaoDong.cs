namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_NgheLaoDong
    {
        public DM_NgheLaoDong()
        {
            this.KhachHang_TrinhDos = new HashSet<KhachHang_TrinhDo>();
            this.DoanhNghiep_TuyenDungs = new HashSet<DoanhNghiep_TuyenDung>();
        }
        [Key]
        public int NgheLaoDong_ID { get; set; }

        [StringLength(500)]
        public string TenNgheLaoDong { get; set; }

        public int? NhomNganhLaoDong { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public bool? KichHoat { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }
        public ICollection<KhachHang_TrinhDo> KhachHang_TrinhDos { get; set; }
        public ICollection<DoanhNghiep_TuyenDung> DoanhNghiep_TuyenDungs { get; set; }
    }
}
