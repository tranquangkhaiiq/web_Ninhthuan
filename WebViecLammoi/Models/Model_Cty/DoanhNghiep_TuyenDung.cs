namespace WebViecLammoi.Models.Model_Cty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNghiep_TuyenDung
    {
        [Key]
        public int TuyenDung_ID { get; set; }

        public int DN_ID { get; set; }

        [StringLength(3000)]
        public string TieuDeTuyenDung { get; set; }

        public int? LoaiViecLamTrong_ID { get; set; }

        public int? SoLuongTuyen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNhanHoSo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHetHan { get; set; }

        public int? NoiLamViec_TinhID { get; set; }

        [StringLength(1000)]
        public string NoiNopHoSo { get; set; }

        public int? LuongTu { get; set; }

        public int? LuongDen { get; set; }

        public int ThoiGianLamViec_ID { get; set; }

        public int ChucDanh_ID { get; set; }

        public int YeuCauTrinhDo_ID { get; set; }

        public int YeuCauNganh_ID { get; set; }

        public int YeuCauNghe_ID { get; set; }

        public int? SoNamKinhNghiem { get; set; }

        public int? YeuCauGioiTinh { get; set; }

        public int? YeuCauTuoiTu { get; set; }

        public int? YeuCauTuoiDen { get; set; }

        public string YeuCauCongViec { get; set; }

        public string MoTaCongViec { get; set; }

        public string QuyenLoi { get; set; }

        public int? SoLuotXem { get; set; }

        public int? TinhTrangHoSo { get; set; }

        public bool? KichHoat { get; set; }

        public bool? NoiBat { get; set; }

        [StringLength(3000)]
        public string XoaHoSo { get; set; }

        public bool? HienThiWeb { get; set; }

        public int? ChiNhanh_ID { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? NguoiCapNhat { get; set; }

        public DateTime? NgayNoiBat { get; set; }

        public int? SanOnline { get; set; }

        public int? NoiLamViec_HuyenID { get; set; }

        [StringLength(500)]
        public string ChucDanh_2022 { get; set; }

        [StringLength(10)]
        public string YeuCauNganhNghe_34_2022 { get; set; }

        public int? YeuCauKinhNghiem_2022 { get; set; }

        public int? YeuCauHocVan_2022 { get; set; }

        [StringLength(500)]
        public string YeuCauMucLuong_2022 { get; set; }

        public string DoiTuongUuTien_2022 { get; set; }

        [StringLength(500)]
        public string HinhThucTuyenDung_2022 { get; set; }

        [StringLength(10)]
        public string MongMuonDN_2022 { get; set; }

        public int? NoiLamViec_2022 { get; set; }

        public int? TrongLuongNang_2022 { get; set; }

        public int? DungDiLai_2022 { get; set; }

        public int? NgheNoi_2022 { get; set; }

        public int? ThiLuc_2022 { get; set; }

        public int? ThaoTacBangTay_2022 { get; set; }

        public int? Dung2Tay_2022 { get; set; }

        [StringLength(10)]
        public string YeuCauThem_2022 { get; set; }

        public int? MucDichLamViec_2022 { get; set; }

        public int? HopDongLaoDong_2022 { get; set; }
        [ForeignKey("TinhTrangHoSo")]
        public virtual DM_TinhTrangPheDuyetHoSo DM_TinhTrangPheDuyetHoSos { get; set; }

        [ForeignKey("DN_ID")]
        public virtual DoanhNghiep DoanhNghieps { get; set; }

        [ForeignKey("ThoiGianLamViec_ID")]
        public virtual DM_ThoiGianLamViec DM_ThoiGianLamViecs { get; set; }

        [ForeignKey("YeuCauTrinhDo_ID")]
        public virtual DM_TrinhDoChuyenMon DM_TrinhDoChuyenMons { get; set; }

        [ForeignKey("ChucDanh_ID")]
        public virtual DM_ChucDanh DM_ChucDanhs { get; set; }

        [ForeignKey("YeuCauNganh_ID")]
        public virtual DM_NganhLaoDong DM_NganhLaoDongs { get; set; }

        [ForeignKey("YeuCauNghe_ID")]
        public virtual DM_NgheLaoDong DM_NgheLaoDongs { get; set; }
    }
}
