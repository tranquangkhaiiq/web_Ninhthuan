namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_DichVu2022
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string TenDichVu { get; set; }

        public string GhiChu { get; set; }
    }
}
