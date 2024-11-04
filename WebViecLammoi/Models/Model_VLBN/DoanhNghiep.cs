namespace WebViecLammoi.Models.Model_VLBN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoanhNghiep")]
    public partial class DoanhNghiep
    {
        [Key]
        public int DN_ID { get; set; }

        [StringLength(50)]
        public string MaVach { get; set; }

        [StringLength(2000)]
        public string TenDoanhNghiep { get; set; }

        [StringLength(500)]
        public string TenNgan { get; set; }

        public int? LoaiHinhDoanhNghiep_ID { get; set; }

        public int? Tinh_ID { get; set; }

        public int? Huyen_ID { get; set; }

        public int? Xa_ID { get; set; }

        [StringLength(300)]
        public string DiaChi { get; set; }

        [StringLength(2000)]
        public string DienThoai { get; set; }

        [StringLength(2000)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(200)]
        public string Skype { get; set; }

        [StringLength(200)]
        public string Facebook { get; set; }

        [StringLength(50)]
        public string NguoiDaiDien { get; set; }

        public int? ChucVu { get; set; }

        [StringLength(2000)]
        public string NguoiDaiDien_DienThoai { get; set; }

        [StringLength(3000)]
        public string SoDangKyKinhDoanh { get; set; }

        [StringLength(50)]
        public string MaSoThue { get; set; }

        public int? SoLaoDong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThanhLap { get; set; }

        public string Logo { get; set; }

        public string GioiThieu { get; set; }

        public int? NganhKD_ID { get; set; }

        public int? NgheKD_ID { get; set; }

        public int? KhuCongNghiep_ID { get; set; }

        public bool? KichHoat { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? NguoiCapNhat { get; set; }

        [StringLength(50)]
        public string LoaiChuTheTuyenDung { get; set; }

        [StringLength(50)]
        public string MaChuTheTuyenDung { get; set; }

        [StringLength(500)]
        public string DichVuDangKy { get; set; }

        [StringLength(500)]
        public string NganhNgheKinhTe2022 { get; set; }

        public int? SoLaoDongTuyenTrong6ThangToi { get; set; }

        [StringLength(200)]
        public string QuocGia { get; set; }
    }
}
