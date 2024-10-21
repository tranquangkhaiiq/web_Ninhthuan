namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_DoiTuong
    {
        public int id { get; set; }

        [StringLength(200)]
        public string ten_doituong { get; set; }

        [StringLength(50)]
        public string ghichu { get; set; }
    }
}
