using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebViecLammoi.Models
{
    public partial class VLDB : DbContext
    {
        public VLDB()
            : base("name=VLDB")
        {
        }
        public virtual DbSet<DM_database> DM_databases { get; set; }
        public virtual DbSet<m_Lienket> m_Lienkets { get; set; }
        public virtual DbSet<aspnet_mapSub> aspnet_mapSubs { get; set; }
        public virtual DbSet<aspnet_mapauto> aspnet_mapautos { get; set; }
        public virtual DbSet<SMS_admintoDN> SMS_AdmintoDNs { get; set; }
        public virtual DbSet<Pay_Sys> Pay_Sys { get; set; }
        public virtual DbSet<Pay_GiaSP> Pay_GiaSPs { get; set; }
        public virtual DbSet<Pay_CTXuatDiem> Pay_CTXuatDiems { get; set; }
        public virtual DbSet<Pay_NhapDiem> Pay_NhapDiems { get; set; }
        public virtual DbSet<Pay_TonCuoi> Pay_TonCuois { get; set; }
        public virtual DbSet<KhachHang_KinhNghiem_LamViec_2022> KhachHang_KinhNghiem_LamViec_2022s { get; set; }
        public virtual DbSet<KhachHang_TimViec_TinHoc> KhachHang_TimViec_TinHocs { get; set; }
        public virtual DbSet<KhachHang_TimViec_NgoaiNgu_2022> KhachHang_TimViec_NgoaiNgu_2022s {  get; set; }
        public virtual DbSet<DoanhNghiep_UngTuyen> DoanhNghiep_UngTuyens {  get; set; }
        public virtual DbSet<aspnet_getVisitors> aspnet_getVisitors { get; set; }
        public virtual DbSet<m_gioithieu> m_gioithieus { get; set; }
        public virtual DbSet<VBPQ_DonVi> VBPQ_DonVis { get; set; }
        public virtual DbSet<VBPQ_LoaiTaiLieu> VBPQ_LoaiTaiLieus { get; set; }
        public virtual DbSet<VBPQ_TaiLieu> VBPQ_TaiLieus { get; set; }
        public virtual DbSet<DM_CheDoPhucLoi_2022> DM_CheDoPhucLoi_2022 { get; set; }
        public virtual DbSet<DM_ChucDanh> DM_ChucDanh { get; set; }
        public virtual DbSet<DM_ChucVu_2022> DM_ChucVu_2022 { get; set; }
        public virtual DbSet<DM_DanToc> DM_DanToc { get; set; }
        public virtual DbSet<DM_DichVu2022> DM_DichVu2022 { get; set; }
        public virtual DbSet<DM_DoiTuong> DM_DoiTuong { get; set; }
        public virtual DbSet<DM_GioiTinh> DM_GioiTinh { get; set; }
        public virtual DbSet<DM_HocVan> DM_HocVan { get; set; }
        public virtual DbSet<DM_KhuCongNghiep> DM_KhuCongNghiep { get; set; }
        public virtual DbSet<DM_KinhNghiem_2022> DM_KinhNghiem_2022 { get; set; }
        public virtual DbSet<DM_KyNangMem_2022> DM_KyNangMem_2022 { get; set; }
        public virtual DbSet<DM_LoaiHinhDoanhNghiep> DM_LoaiHinhDoanhNghiep { get; set; }
        public virtual DbSet<DM_LoaiHopDong_2022> DM_LoaiHopDong_2022 { get; set; }
        public virtual DbSet<DM_MucLuong2022> DM_MucLuong2022 { get; set; }
        public virtual DbSet<DM_NganhKinhDoanh> DM_NganhKinhDoanh { get; set; }
        public virtual DbSet<DM_NganhLaoDong> DM_NganhLaoDong { get; set; }
        public virtual DbSet<DM_NgheHoc> DM_NgheHoc { get; set; }
        public virtual DbSet<DM_NgheKinhDoanh> DM_NgheKinhDoanh { get; set; }
        public virtual DbSet<DM_NgheLaoDong> DM_NgheLaoDong { get; set; }
        public virtual DbSet<DM_NghiepVu> DM_NghiepVu { get; set; }
        public virtual DbSet<DM_QuocGia> DM_QuocGia { get; set; }
        public virtual DbSet<DM_ThoiGianLamViec> DM_ThoiGianLamViec { get; set; }
        public virtual DbSet<DM_TrinhDoChuyenMon> DM_TrinhDoChuyenMon { get; set; }
        public virtual DbSet<DoanhNghiep> DoanhNghieps { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung> DoanhNghiep_TuyenDung { get; set; }
        
        public virtual DbSet<DoanhNghiep_TuyenDung_LaoDongNuocNgoai_DiaDiemLamViec2023> DoanhNghiep_TuyenDung_LaoDongNuocNgoai_DiaDiemLamViec2023 { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_LaoDongNuocNgoai_MoTaKinhNghiem2023> DoanhNghiep_TuyenDung_LaoDongNuocNgoai_MoTaKinhNghiem2023 { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_LaoDongNuocNgoai_MoTaLuong2023> DoanhNghiep_TuyenDung_LaoDongNuocNgoai_MoTaLuong2023 { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_LaoDongNuocNgoai_ThoiGianLamViec2023> DoanhNghiep_TuyenDung_LaoDongNuocNgoai_ThoiGianLamViec2023 { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_NghiepVu> DoanhNghiep_TuyenDung_NghiepVu { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_NgoaiNgu> DoanhNghiep_TuyenDung_NgoaiNgu { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_TinHoc> DoanhNghiep_TuyenDung_TinHoc { get; set; }
        public virtual DbSet<DoanhNgiep_TuyenDung_CheDoPhucLoi_2022> DoanhNgiep_TuyenDung_CheDoPhucLoi_2022 { get; set; }
        public virtual DbSet<DoanhNgiep_TuyenDung_KyNangMem_2022> DoanhNgiep_TuyenDung_KyNangMem_2022 { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhachHang_HocNghe> KhachHang_HocNghe { get; set; }
        public virtual DbSet<KhachHang_NghiepVu> KhachHang_NghiepVu { get; set; }
        public virtual DbSet<KhachHang_TimViec_TrinhDoCMKT_2022> KhachHang_TimViec_TrinhDoCMKT_2022 { get; set; }
        public virtual DbSet<KhachHang_TimViecLam> KhachHang_TimViecLam { get; set; }
        public virtual DbSet<KhachHang_TimViecLam_FileDinhKem> KhachHang_TimViecLam_FileDinhKem { get; set; }
        public virtual DbSet<KhachHang_TrinhDo> KhachHang_TrinhDo { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<NhanVien_LoaiTaiKhoan> NhanVien_LoaiTaiKhoan { get; set; }
        public virtual DbSet<NhatKy_Admin> NhatKy_Admin { get; set; }
        public virtual DbSet<PHU_HSTD_Luu> PHU_HSTD_Luu { get; set; }
        public virtual DbSet<PHU_HSTV_Luu> PHU_HSTV_Luu { get; set; }
        public virtual DbSet<UserWeb> UserWebs { get; set; }
        public virtual DbSet<DM_DiaChi> DM_DiaChi { get; set; }
        public virtual DbSet<DoanhNghiep_TuyenDung_CongVan_List_2023> DoanhNghiep_TuyenDung_CongVan_List_2023 { get; set; }
        public virtual DbSet<NewsStatu> NewsStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DM_CheDoPhucLoi_2022>()
                .Property(e => e.IconShow)
                .IsUnicode(false);

            modelBuilder.Entity<DM_ChucDanh>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<DM_KhuCongNghiep>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<DM_LoaiHinhDoanhNghiep>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<DM_NgheHoc>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<DM_QuocGia>()
                .Property(e => e.QuocGia_NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<DM_ThoiGianLamViec>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<DM_TrinhDoChuyenMon>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

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

            modelBuilder.Entity<DoanhNghiep_TuyenDung_LaoDongNuocNgoai_ThoiGianLamViec2023>()
                .Property(e => e.ThoiGianTu)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung_LaoDongNuocNgoai_ThoiGianLamViec2023>()
                .Property(e => e.ThoiGianDen)
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

            modelBuilder.Entity<DoanhNghiep_TuyenDung_CongVan_List_2023>()
                .Property(e => e.GuiId)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung_CongVan_List_2023>()
                .Property(e => e.SoCongVan)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep_TuyenDung_CongVan_List_2023>()
                .Property(e => e.NgayCongVan)
                .IsUnicode(false);
        }
    }
}
