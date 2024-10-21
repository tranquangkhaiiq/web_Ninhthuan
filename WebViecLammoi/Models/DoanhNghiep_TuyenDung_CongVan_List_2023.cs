namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNghiep_TuyenDung_CongVan_List_2023
    {
        public int ID { get; set; }

        public int? DN_ID { get; set; }

        [StringLength(500)]
        public string GuiId { get; set; }

        public int? TuyenDung_ID { get; set; }

        public bool? TinhTrangNhanKetQua { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        [StringLength(2000)]
        public string GhiChu { get; set; }

        [StringLength(50)]
        public string SoCongVan { get; set; }

        [StringLength(10)]
        public string NgayCongVan { get; set; }
    }
}
