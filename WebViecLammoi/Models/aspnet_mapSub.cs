

namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    public partial class aspnet_mapSub
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Substr { get; set; }
        public int mapautoId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }
    }
}