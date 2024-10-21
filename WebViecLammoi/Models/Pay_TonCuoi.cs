namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pay_TonCuoi
    {
        [Key]
        public int DNID { get; set; }
        public double TongNhap { get; set; }
        public double TongXuat { get; set; }
        public double SoDiemTon { get; set; }
        [StringLength(500)]
        public string Ghichuxuat { get; set; }
        [StringLength(500)]
        public string Ghichunhap { get; set; }
        public DateTime? ngay { get; set; }
    }
}
