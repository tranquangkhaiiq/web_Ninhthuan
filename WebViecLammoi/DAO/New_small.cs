using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebViecLammoi.DAO
{
    public partial class New_small
    {
        public int NewId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(4000)]
        public string Title { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }

        [Column(TypeName = "ntext")]
        public string Summary { get; set; }
        //public string Content { get; set; }
        //public bool isActive { get; set; }
        public bool Hotcat { get; set; }

        public bool Hotsite { get; set; }

        public DateTime Createdate { get; set; }

        public int PortalId { get; set; }
        //public int UserId { get; set; }

        //public int Status { get; set; }

        //public int? ExSummary { get; set; }

        //public string ImagePath2 { get; set; }
        public int? View { get; set; }
    }
}