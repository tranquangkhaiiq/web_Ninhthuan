namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_HocVan
    {
        [Key]
        public int HocVan_ID { get; set; }

        [StringLength(50)]
        public string HocVan_Ten { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(20)]
        public string NguoiTao { get; set; }

        public bool? KichHoat { get; set; }
    }
}
