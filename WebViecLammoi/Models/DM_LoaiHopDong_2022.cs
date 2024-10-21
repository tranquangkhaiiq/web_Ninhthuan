namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_LoaiHopDong_2022
    {
        [Key]
        public int LoaiHopDong_ID { get; set; }

        [StringLength(2000)]
        public string TenLoaiHopDong { get; set; }

        public bool? KichHoat { get; set; }
    }
}
