namespace WebViecLammoi.Models.Model_Cty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [Key]
        public int KH_ID { get; set; }

        [StringLength(50)]
        public string MaVach { get; set; }

        [Required]
        [StringLength(30)]
        public string CMND { get; set; }

        [StringLength(2000)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        public int? GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCap { get; set; }

        public int? NoiCap_ID { get; set; }

        [StringLength(50)]
        public string SoGiayPhepLaiXe { get; set; }

        [StringLength(50)]
        public string HangGPLX { get; set; }

        public int? NoiCapGPLX { get; set; }

        public DateTime? NgayCapGPLX { get; set; }

        [StringLength(2000)]
        public string DienThoai { get; set; }

        public int? TheBHYT { get; set; }

        [StringLength(50)]
        public string SoBHXH { get; set; }

        [StringLength(50)]
        public string bhxh_noikhambenh { get; set; }

        public DateTime? bhxh_ngaycap { get; set; }

        public int? bhxh_noicap { get; set; }

        [StringLength(3000)]
        public string MaSoThue { get; set; }

        public int? Nganhang { get; set; }

        [StringLength(50)]
        public string Sotaikhoan { get; set; }

        [StringLength(2000)]
        public string Email { get; set; }

        [StringLength(80)]
        public string Skype { get; set; }

        [StringLength(80)]
        public string Facebook { get; set; }

        public int? ThuongTru_Tinh_ID { get; set; }

        public int? ThuongTru_Huyen_ID { get; set; }

        public int? ThuongTru_Xa_ID { get; set; }

        [StringLength(300)]
        public string ThuongTru_DiaChi { get; set; }

        public int? TamTru_Tinh_ID { get; set; }

        public int? TamTru_Huyen_ID { get; set; }

        public int? TamTru_Xa_ID { get; set; }

        [StringLength(300)]
        public string TamTru_DiaChi { get; set; }

        public string Hinh { get; set; }

        public string ChuKy { get; set; }

        public int? TinhTrangHonNhan { get; set; }

        public int? HocVan_ID { get; set; }

        [StringLength(50)]
        public string SucKhoe { get; set; }

        public int? ChieuCao { get; set; }

        public int? CanNang { get; set; }

        public int? QuocTich { get; set; }

        public int? DanToc_ID { get; set; }

        public int? DoiTuongChinhSach_ID { get; set; }

        public int? TonGiao { get; set; }

        public int? NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiCapNhat { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? SoLanCapNhat { get; set; }

        public bool KichHoat { get; set; }

        public string DoiTuongUuTien_2022 { get; set; }

        public int? YeuCauHocVan_2022 { get; set; }

        public string DichVuDangKy_2022 { get; set; }
    }
}
