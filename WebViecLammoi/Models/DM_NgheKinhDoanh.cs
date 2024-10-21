namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_NgheKinhDoanh
    {
        public DM_NgheKinhDoanh()
        {
        }
        [Key]
        public int NgheKD_ID { get; set; }

        [StringLength(2000)]
        public string TenNgheKD { get; set; }

        public int? Nhom_NganhKD_ID { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public bool? KichHoat { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
        //public ICollection<DoanhNghiep_TuyenDung> DoanhNghiep_TuyenDungs { get; set; }
    }
}
