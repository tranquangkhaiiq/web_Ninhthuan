namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_KinhNghiem_2022
    {
        [Key]
        public int KinhNghiem_ID { get; set; }

        [StringLength(2000)]
        public string TenKinhNghiem { get; set; }

        [StringLength(2000)]
        public string MoTa { get; set; }

        public bool? KichHoat { get; set; }
    }
}
