namespace WebViecLammoi.Models.Model_VLBN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_TimViec_KinhNghiem_2022
    {
        [Key]
        public int KhachHang_TimViec_KinhNghiem_ID { get; set; }

        public int? KH_ID { get; set; }

        [StringLength(500)]
        public string TenCongTy { get; set; }

        [StringLength(500)]
        public string ChucVu { get; set; }

        [StringLength(10)]
        public string TuThang { get; set; }

        [StringLength(10)]
        public string DenThang { get; set; }

        public string CongViecChinh { get; set; }

        [StringLength(500)]
        public string DaLamViecNuocNgoai { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
    }
}
