namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_NganhKinhDoanh
    {
        [Key]
        public int Nganh_ID { get; set; }

        [Required]
        [StringLength(2000)]
        public string TenNganhKD { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public bool? KichHoat { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }
}
