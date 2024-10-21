namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNghiep_TuyenDung_TinHoc
    {

        [Key]
        public int TuyenDung_TinHoc_ID { get; set; }

        public int? TuyenDung_ID { get; set; }

        public int? NghiepVu_TinHoc_ID { get; set; }

        [StringLength(500)]
        public string KhaNangSuDung { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
        [ForeignKey("NghiepVu_TinHoc_ID")]
        public virtual DM_NghiepVu DM_NghiepVus { get; set; }
    }
}
