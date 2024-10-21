namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsStatu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewsStatusId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string StatusName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
