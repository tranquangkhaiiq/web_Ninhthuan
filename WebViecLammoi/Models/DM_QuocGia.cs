namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_QuocGia
    {
        [Key]
        public int QuocGia_Ma { get; set; }

        [StringLength(50)]
        public string QuocGia_Ten { get; set; }

        public DateTime? QuocGia_NgayTao { get; set; }

        [StringLength(50)]
        public string QuocGia_NguoiTao { get; set; }

        public bool? KichHoat { get; set; }
    }
}
