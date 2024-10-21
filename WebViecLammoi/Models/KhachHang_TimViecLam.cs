namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_TimViecLam
    {

        [Key]
        public int TimViec_ID { get; set; }

        public int KH_ID { get; set; }

        public int? LoaiTimViecLam_ID { get; set; }

        public int? HocNghe_ID { get; set; }

        public long? BHTN_ID { get; set; }

        public string TenHoSo { get; set; }

        public int ChucDanhMongMuon { get; set; }

        public int? MucLuongMongMuonTu { get; set; }

        public int? MucLuongMongMuonDen { get; set; }

        public int ThoiGianLamViecMongMuon { get; set; }

        public bool? CoTheDiCongTac { get; set; }

        public int NganhMongMuon_ID { get; set; }

        public int NgheMongMuon_ID { get; set; }

        public int? LoaiHinhDNMongMuon_ID { get; set; }

        public int? NoiLamViecMongMuon_ID { get; set; }

        public string MongMuonKhac { get; set; }

        public int? MucLuongCu { get; set; }

        public int? LyDoNghiViec_ID { get; set; }

        public int? CongTyNghiViec_ID { get; set; }

        public string MoTaKinhNghiemLamViec { get; set; }

        public int? SoNamKinhNghiem { get; set; }

        public string KhaNangNoiTroi { get; set; }

        public DateTime? NgayDangKy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCoTheLamViec { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHoSoHetHan { get; set; }

        public int? TyLeHoSoHoanThanh { get; set; }

        [StringLength(50)]
        public string DiemHoSo { get; set; }

        public int? LuotXem { get; set; }

        public bool? KichHoat { get; set; }

        public int? TinhTrangPheDuyetHoSo_ID { get; set; }

        public int? NguoiTao { get; set; }

        public DateTime NgayTao { get; set; }

        public int? NguoiCapNhat { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? ChiNhanh_ID { get; set; }

        public bool? NoiBat { get; set; }

        public bool? HienThiTrenWeb { get; set; }

        [StringLength(500)]
        public string ChucVu_2022 { get; set; }

        [StringLength(30)]
        public string NganhNghe34_2022 { get; set; }

        public int? KinhNghiem_2022 { get; set; }

        public int? NoiLamViec_Uutien_Tinh_1 { get; set; }

        public int? NoiLamViec_Uutien_Huyen_1 { get; set; }

        public int? NoiLamViec_Uutien_Tinh_2 { get; set; }

        public int? NoiLamViec_Uutien_Huyen_2 { get; set; }

        [StringLength(50)]
        public string MucLuong_2022 { get; set; }

        public int? HopDongLaoDong_2022 { get; set; }

        public int? MucDichLamViec_2022 { get; set; }

        [StringLength(10)]
        public string KhaNangDapUng_2022 { get; set; }

        public int? NoiLamViec_2022 { get; set; }

        public int? TrongLuongNang_2022 { get; set; }

        public int? DungDiLai_2022 { get; set; }

        public int? NgheNoi_2022 { get; set; }

        public int? ThiLuc_2022 { get; set; }

        public int? ThaoTacBangTay_2022 { get; set; }

        public int? Dung2Tay_2022 { get; set; }

        [StringLength(10)]
        public string SanSangLamViec_2022 { get; set; }

        [StringLength(500)]
        public string HinhThucTuyenDung_2022 { get; set; }
        [ForeignKey("TinhTrangPheDuyetHoSo_ID")]
        public virtual DM_TinhTrangPheDuyetHoSo DM_TinhTrangPheDuyetHoSos { get; set; }
        [ForeignKey("KH_ID")]
        public virtual KhachHang KhachHangs { get; set; }

        [ForeignKey("NoiLamViecMongMuon_ID")]
        public virtual DM_DiaChi DM_DiaChis { get; set; }

        [ForeignKey("ChucDanhMongMuon")]
        public virtual DM_ChucDanh DM_ChucDanhs { get; set; }

        [ForeignKey("LoaiHinhDNMongMuon_ID")]
        public virtual DM_LoaiHinhDoanhNghiep DM_LoaiHinhDoanhNghieps { get; set; }

        [ForeignKey("ThoiGianLamViecMongMuon")]
        public virtual DM_ThoiGianLamViec DM_ThoiGianLamViecs { get; set; }

        [ForeignKey("NganhMongMuon_ID")]
        public virtual DM_NganhLaoDong DM_NganhLaoDongs { get; set; }

        [ForeignKey("NgheMongMuon_ID")]
        public virtual DM_NgheLaoDong DM_NgheLaoDongs { get; set; }
    }
}
