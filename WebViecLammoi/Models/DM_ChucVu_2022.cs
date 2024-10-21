namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_ChucVu_2022
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string TenChucVu { get; set; }

        public bool? Kichhoat { get; set; }
    }
}
