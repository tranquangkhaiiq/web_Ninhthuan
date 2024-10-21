namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_MucLuong2022
    {
        [Key]
        public int MucLuong_ID { get; set; }

        [StringLength(200)]
        public string TenMucLuong { get; set; }

        public bool? KichHoat { get; set; }
    }
}
