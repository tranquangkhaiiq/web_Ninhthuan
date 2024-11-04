using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebViecLammoi.Models
{
    public class DM_database
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string DataDB { get; set; }
        [StringLength(50)]
        public string TenDB { get; set; }
        public bool kichhoat { get; set; }
        public DateTime Ngay { get; set; }
    }
}