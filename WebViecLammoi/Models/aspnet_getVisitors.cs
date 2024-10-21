

namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    public partial class aspnet_getVisitors
    {
        [Key]
        public int Id { get; set; }

        public int? TongLuotTruyCap { get; set; }

        public int? Online { get; set; }

        [StringLength(500)]
        public string Ghichu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }
    }
}