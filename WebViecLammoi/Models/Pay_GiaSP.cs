namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pay_GiaSP
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TenBangchuaSP { get; set; }
        
        [StringLength(500)]
        public string TenSP { get; set; }
        public double Gia {  get; set; }
        [StringLength(500)]
        public string Ghichu { get; set; }

    }
}
