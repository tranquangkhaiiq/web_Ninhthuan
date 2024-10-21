namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhatKy_Admin
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int LoaiUser { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(500)]
        public string CongViec { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }
}
