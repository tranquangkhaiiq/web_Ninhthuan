namespace WebViecLammoi.Models.Model_VLBN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNghiep_TuyenDung_NghiepVu
    {
        [Key]
        public int TuyenDung_NghiepVu_ID { get; set; }

        public int? TuyenDung_ID { get; set; }

        public int? NghiepVu_ID { get; set; }

        [StringLength(500)]
        public string MoTaTrinhDoNghiepVu { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
    }
}
