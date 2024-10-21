namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_CheDoPhucLoi_2022
    {
        [Key]
        public int CheDoPhucLoi_ID { get; set; }

        [StringLength(2000)]
        public string TenCheDoPhucLoi { get; set; }

        public bool? KichHoat { get; set; }

        [StringLength(200)]
        public string IconShow { get; set; }
    }
}
