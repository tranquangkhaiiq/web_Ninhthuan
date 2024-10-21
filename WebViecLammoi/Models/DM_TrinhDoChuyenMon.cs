namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_TrinhDoChuyenMon
    {
        public DM_TrinhDoChuyenMon()
        {
            this.DoanhNghiep_TuyenDungs = new HashSet<DoanhNghiep_TuyenDung>();
            this.KhachHang_TrinhDos = new HashSet<KhachHang_TrinhDo>();
        }
        [Key]
        public int TrinhDoChuyenMon_ID { get; set; }

        [Required]
        [StringLength(2000)]
        public string TenChuyenMon { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public bool? KichHoat { get; set; }

        public int? OrderTopTrinhDo { get; set; }

        public ICollection<DoanhNghiep_TuyenDung> DoanhNghiep_TuyenDungs { get; set; }
        public ICollection<KhachHang_TrinhDo> KhachHang_TrinhDos { get; set; }
    }
}
