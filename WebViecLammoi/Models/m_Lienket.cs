

namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    public partial class m_Lienket
    {
        [Key]
        public int Id { get; set; }
        [StringLength(500)]
        public string Tenweb { get; set; }

        [StringLength(500)]
        public string link { get; set; }
        public int loaiId { get; set; }
        [StringLength(500)]
        public string Gioithieu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }
    }
}