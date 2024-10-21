namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhanVien_LoaiTaiKhoan
    {
        [Key]
        public int NhanVien_LoaiTaiKhoan_ID { get; set; }

        [StringLength(50)]
        public string TenLoaiTaiKhoan { get; set; }

        public bool? TrangChu { get; set; }

        public bool? AdminWebService { get; set; }

        public bool? QuanLyKhachHang { get; set; }

        public bool? QuanLyDoanhNghiep { get; set; }

        public bool? HoSoKhachHang { get; set; }

        public bool? HoSoDoanhNghiep { get; set; }

        public bool? QuanLyNhanVien { get; set; }

        public bool? QuanLyMaVach { get; set; }

        public bool? mau28_QuanLyLaoDong { get; set; }

        public bool? ThongKeBaoCao { get; set; }

        public bool? QuanLyDanhMuc { get; set; }

        public bool? CungLaoDong { get; set; }

        public bool? CauLaoDong { get; set; }

        public bool? BHTN { get; set; }

        public bool? DaoTaoNghe { get; set; }

        public bool? QuanLyKhaiBaoCoViecLam { get; set; }

        public bool? QuanLyXacNhanSoThangHuong { get; set; }

        public bool? QuanLyDaoTao { get; set; }

        public bool? QuanLyQRCodeHocNghe { get; set; }

        public bool? QuanLyTuVanHocNgheBHTN { get; set; }

        public bool? QuanLyViecLamBHTN { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }
    }
}
