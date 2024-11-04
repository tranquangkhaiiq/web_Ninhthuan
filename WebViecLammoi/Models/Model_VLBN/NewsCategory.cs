namespace WebViecLammoi.Models.Model_VLBN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(200)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? PortalId { get; set; }

        public int? ParentId { get; set; }

        public int? OrderNumber { get; set; }

        public Guid msrepl_tran_version { get; set; }
    }
}
