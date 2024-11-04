namespace WebViecLammoi.Models.Model_VLBN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int NewId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(4000)]
        public string Title { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }

        [Column(TypeName = "ntext")]
        public string Summary { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool isActive { get; set; }

        public bool Hotcat { get; set; }

        public bool Hotsite { get; set; }

        public DateTime Createdate { get; set; }

        public int PortalId { get; set; }

        public int UserId { get; set; }

        public int Status { get; set; }

        public int? ExSummary { get; set; }

        public string ImagePath2 { get; set; }

        public int? View { get; set; }

        public Guid msrepl_tran_version { get; set; }

        public DateTime? Createdate2 { get; set; }

        public string msrepl_tran_version1 { get; set; }
    }
}
