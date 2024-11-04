namespace WebViecLammoi.Models.Model_Cty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_TimViec_TinHoc
    {
        [Key]
        public int KhachHang_TimViec_TinHoc_ID { get; set; }

        public int? KH_ID { get; set; }

        public int? NghiepVu_TinHoc_ID { get; set; }

        [StringLength(500)]
        public string KhaNangSuDung { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
    }
}
