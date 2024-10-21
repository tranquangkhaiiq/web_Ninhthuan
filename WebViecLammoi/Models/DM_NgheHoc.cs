namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_NgheHoc
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Ten_NgheHoc { get; set; }

        public double? SoThangHoc { get; set; }

        public int? TrangThai { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(1000)]
        public string GhiChu { get; set; }
    }
}
