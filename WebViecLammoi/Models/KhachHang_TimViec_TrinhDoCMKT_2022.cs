namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_TimViec_TrinhDoCMKT_2022
    {
        [Key]
        public int TimViec_TrinhDo_ID { get; set; }

        public int? KH_ID { get; set; }

        public int? TrinhDoCMKT_ID { get; set; }

        [StringLength(500)]
        public string MoTaChuyenNganh { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
    }
}
