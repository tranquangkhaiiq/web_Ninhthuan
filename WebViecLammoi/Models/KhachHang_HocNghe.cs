namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_HocNghe
    {
        [Key]
        public int HocNghe_ID { get; set; }

        public int? KH_ID { get; set; }

        public int? HocNghe_SoQuyetDinh { get; set; }

        public int? LoaiHocNghe_ID { get; set; }

        public int? BHTN_ID { get; set; }

        public int? KhoaHoc_ID { get; set; }

        public int? NganhNgheDaoTao_ID { get; set; }

        public int? ChiNhanh_ID { get; set; }

        public DateTime? NgayDangKy { get; set; }

        [StringLength(500)]
        public string GiayToKemTheo { get; set; }

        public DateTime? NgayDuyet { get; set; }

        public DateTime? NgayNhanKetQua { get; set; }

        public bool? DaNhanKetQua { get; set; }

        public bool? DaNhapHoc { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? NguoiCapNhat { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }
}
