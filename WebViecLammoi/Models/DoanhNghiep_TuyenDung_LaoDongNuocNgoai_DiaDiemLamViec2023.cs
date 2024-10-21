namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNghiep_TuyenDung_LaoDongNuocNgoai_DiaDiemLamViec2023
    {
        public int ID { get; set; }

        public int? TuyenDung_ID { get; set; }

        public string DiaDiemLamViec { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        [StringLength(2000)]
        public string GhiChu { get; set; }
    }
}
