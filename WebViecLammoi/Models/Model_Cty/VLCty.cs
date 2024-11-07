using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebViecLammoi.Models.Model_Cty
{
    public partial class VLCty : DbContext
    {
        public VLCty()
            : base("name=VLCty")
        {
        }
        public virtual DbSet<DM_DiaChi> DM_DiaChis { get; set; }
        public virtual DbSet<Pay_Sys> Pay_Syss { get; set; }
        public virtual DbSet<DoanhNghiep> DoanhNghieps { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung> DoanhNghiep_TuyenDung { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_NghiepVu> DoanhNghiep_TuyenDung_NghiepVu { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_NgoaiNgu> DoanhNghiep_TuyenDung_NgoaiNgu { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_TinHoc> DoanhNghiep_TuyenDung_TinHoc { get; set; }
        public virtual DbSet<DoanhNghiep_UngTuyen> DoanhNghiep_UngTuyen { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhachHang_NghiepVu> KhachHang_NghiepVu { get; set; }
        public virtual DbSet<KhachHang_TimViec_KinhNghiem_2022> KhachHang_TimViec_KinhNghiem_2022 { get; set; }
        public virtual DbSet<KhachHang_TimViec_NgoaiNgu_2022> KhachHang_TimViec_NgoaiNgu_2022 { get; set; }
        public virtual DbSet<KhachHang_TimViec_TinHoc> KhachHang_TimViec_TinHoc { get; set; }
        public virtual DbSet<KhachHang_TimViecLam> KhachHang_TimViecLam { get; set; }
        public virtual DbSet<KhachHang_TimViecLam_FileDinhKem> KhachHang_TimViecLam_FileDinhKem { get; set; }
        public virtual DbSet<KhachHang_TrinhDo> KhachHang_TrinhDo { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.MaVach)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.Skype)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.NguoiDaiDien_DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.MaSoThue)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.LoaiChuTheTuyenDung)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.MaChuTheTuyenDung)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung>()
                .Property(e => e.XoaHoSo)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung>()
                .Property(e => e.YeuCauNganhNghe_34_2022)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung>()
                .Property(e => e.MongMuonDN_2022)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung>()
                .Property(e => e.YeuCauThem_2022)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaVach)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoGiayPhepLaiXe)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.HangGPLX)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoBHXH)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Sotaikhoan)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Skype)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViec_KinhNghiem_2022>()
                .Property(e => e.TuThang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViec_KinhNghiem_2022>()
                .Property(e => e.DenThang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViecLam>()
                .Property(e => e.DiemHoSo)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViecLam>()
                .Property(e => e.NganhNghe34_2022)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViecLam>()
                .Property(e => e.MucLuong_2022)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViecLam>()
                .Property(e => e.KhaNangDapUng_2022)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViecLam>()
                .Property(e => e.SanSangLamViec_2022)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TimViecLam_FileDinhKem>()
                .Property(e => e.NguoiUpload)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang_TrinhDo>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.msrepl_tran_version1)
                .IsUnicode(false);
        }
    }
}
