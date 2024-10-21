namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNgiep_TuyenDung_CheDoPhucLoi_2022
    {
        [Key]
        public int TuyenDung_CheDoPhucLoi_ID { get; set; }

        public int? TuyenDung_ID { get; set; }

        public int? CheDoPhucLoi_ID { get; set; }

        [StringLength(500)]
        public string MoTaCheDoPhucLoi { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
    }
}
