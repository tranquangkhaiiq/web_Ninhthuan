namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pay_NhapDiem
    {
        [Key]
        public Guid Id { get; set; }
        public int DN_ID { get; set; }
        public double SoTien { get; set; }
        public double SoDiemNhap { get; set; }
        [StringLength(200)]
        public string FileKiemtra { get; set; }
        [StringLength(500)]
        public string Ghichu { get; set; }
        public DateTime Ngay { get; set; }
    }
}
