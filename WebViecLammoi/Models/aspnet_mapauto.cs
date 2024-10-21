

namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    public partial class aspnet_mapauto
    {
        [Key]
        public int Id { get; set; }
        [StringLength(500)]
        public string Keystr { get; set; }

        [StringLength(500)]
        public string Giaithich { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }
    }
}