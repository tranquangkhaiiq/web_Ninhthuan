namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_ThoiGianLamViec
    {
        [Key]
        public int ThoiGianLamViec_ID { get; set; }

        [Required]
        [StringLength(2000)]
        public string TenThoiGianLamViec { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public bool? KichHoat { get; set; }
    }
}
