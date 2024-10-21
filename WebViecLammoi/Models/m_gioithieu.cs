using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebViecLammoi.Models
{
    public partial class m_gioithieu
    {
        public int id { get; set; }

        public string noidung { get; set; }

        [StringLength(50)]
        public string ghichu { get; set; }

        public DateTime? ngaytao { get; set; }

        public Guid msrepl_tran_version { get; set; }
    }
}