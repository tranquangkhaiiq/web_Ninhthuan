namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMS_admintoDN
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(200)]
        public string Tieude {  get; set; }
        [StringLength(2000)]
        public string SMS_AdmintoDN { get; set; }
        public int DNID { get; set; }
        public bool DaXem { get; set; }
        public DateTime Ngay { get; set; }
    }
}
