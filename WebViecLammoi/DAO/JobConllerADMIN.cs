using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebViecLammoi.Models;
using WebViecLammoi;
using System.Drawing.Printing;
using System.Security.Cryptography;
namespace WebViecLammoi.DAO
{
    //public class JobConllerADMIN
    //{
    //    private static object k;

    //    //***************Admin_không quan tâm NgayHetHan*************************

        
    //    public static List<DoanhNghiep_TuyenDung> GetListJob_File_SearchAD(VLDB db, int Sec, int pageSize, string strTK)
    //    {

    //        var mode = ListDuyet_NICEAD(db).Where(p => p.TieuDeTuyenDung.ToLower().Contains(strTK.ToLower())
    //                    || p.DoanhNghieps.TenDoanhNghiep.ToLower().Contains(strTK.ToLower())
    //                    || p.TuyenDung_ID.ToString().Contains(strTK))
    //                    .OrderByDescending(p => p.NoiBat).ThenByDescending(p => p.NgayCapNhat)
    //                    .Skip(Sec * pageSize)
    //                    .Take(pageSize)
    //                    .ToList();
    //        return mode;
    //    }
    //    public static int GetPageCountJob_File_SearchAD(VLDB db, string strTK)
    //    {
    //        //Dùng chung JobController
    //        int mode = 0;
    //        mode = ListDuyet_NICEAD(db).Where(p => p.TieuDeTuyenDung.ToLower().Contains(strTK.ToLower())
    //                    || p.DoanhNghieps.TenDoanhNghiep.ToLower().Contains(strTK.ToLower())
    //                    || p.TuyenDung_ID.ToString().Contains(strTK)).Count();
    //        return mode;
    //    }
    //    public static int GetTotal_DuyetHot_NICEAD(VLDB db)
    //    {
    //        int mode = 0;
    //        mode = ListDuyet_Hot(db).Count();
    //        return mode;
    //    }
        
        
    //    public static List<DoanhNghiep_TuyenDung> GetList_DuyetHot_NICEAD(VLDB db, int Sec, int pageSize)
    //    {
    //        var mode = ListDuyet_Hot(db)
    //                        .OrderByDescending(p => p.NgayCapNhat)
    //                        .Skip(Sec * pageSize)
    //                        .Take(pageSize)
    //                        .ToList();
    //        return mode;
    //    }
    //    public static int GetTotal_Duyet_NiceAD(VLDB db)
    //    {
    //        int mode = 0;
    //        mode = ListDuyet_NICEAD(db).Count();
    //        return mode;
    //    }
    //    public static List<DoanhNghiep_TuyenDung> GetList_Duyet_NICEAD(VLDB db, int Sec, int pageSize)
    //    {

    //        var mode = ListDuyet_NICEAD(db);
    //        return mode;
    //    }
    //    public static List<DoanhNghiep_TuyenDung> GetListCheck_Job(VLDB db, int Sec, int pageSize)
    //    {
    //        var mode = ListChoDuyet(db)
    //                    .OrderByDescending(p => p.TuyenDung_ID)
    //                    .Skip(Sec * pageSize)
    //                    .Take(pageSize)
    //                    .ToList();
    //        return mode;
    //    }
    //    public static List<DoanhNghiep_TuyenDung> CheckAll_Job(VLDB db)
    //    {
    //        var mode = ListChoDuyet(db);
    //        return mode;
    //    }
    //    public static int GetPageCountCheck_Job(VLDB db)
    //    {
    //        var mode = 0;
    //        mode = ListChoDuyet(db).Count();
    //        return mode;
    //    }
    //    private static List<DoanhNghiep_TuyenDung> ListDuyet_Hot(VLDB db)
    //    {
    //        var mode = ListTD(db).Where(p => p.TinhTrangHoSo == 3 && p.NoiBat == true);
    //        return mode.ToList();
    //    }

    //    private static List<DoanhNghiep_TuyenDung> ListDuyet_NICEAD(VLDB db)
    //    {
    //        var model = ListTD(db).Where(p => p.TinhTrangHoSo == 3);
    //        return model.ToList();
    //    }
    //    private static List<DoanhNghiep_TuyenDung> ListChoDuyet(VLDB db)
    //    {
    //        var model = ListTD(db).Where(p => p.TinhTrangHoSo == 2 && p.NgayHetHan > DateTime.Now && p.HienThiWeb ==true);
    //        return model.ToList();
    //    }
    //    private static List<DoanhNghiep_TuyenDung> ListTD(VLDB db)
    //    {
    //        var model = new List<DoanhNghiep_TuyenDung>();
    //        model = db.Database.SqlQuery<DoanhNghiep_TuyenDung>("SELECT top(1000)" +
    //            "[YeuCauCongViec]='',[MoTaCongViec] = '',[QuyenLoi] = ''," +
    //            "[TuyenDung_ID],[DN_ID],[TieuDeTuyenDung],[LoaiViecLamTrong_ID],[SoLuongTuyen],[NgayNhanHoSo]," +
    //            "[NgayHetHan],[NoiLamViec_TinhID],[NoiNopHoSo],[LuongTu],[LuongDen],[ThoiGianLamViec_ID],[ChucDanh_ID]," +
    //            "[YeuCauTrinhDo_ID],[YeuCauNganh_ID],[YeuCauNghe_ID],[SoNamKinhNghiem],[YeuCauGioiTinh],[YeuCauTuoiTu]," +
    //            "[YeuCauTuoiDen],[SoLuotXem],[TinhTrangHoSo],[KichHoat],[NoiBat],[XoaHoSo],[HienThiWeb],[ChiNhanh_ID]," +
    //            "[NgayTao],[NguoiTao],[NgayCapNhat],[NguoiCapNhat],[NgayNoiBat],[SanOnline],[NoiLamViec_HuyenID], " +

    //            "[ChucDanh_2022] =null,[YeuCauNganhNghe_34_2022] =null,[YeuCauKinhNghiem_2022] =null, " +
    //                "[YeuCauHocVan_2022] =null,[YeuCauMucLuong_2022] =null,[DoiTuongUuTien_2022] =null, " +
    //                "[HinhThucTuyenDung_2022] =null,[MongMuonDN_2022] =null,[NoiLamViec_2022] =null, " +
    //                "[TrongLuongNang_2022] =null,[DungDiLai_2022] =null,[NgheNoi_2022] =null, " +
    //                "[ThiLuc_2022] =null,[ThaoTacBangTay_2022] =null,[Dung2Tay_2022] =null, " +
    //                "[YeuCauThem_2022] =null,[MucDichLamViec_2022] =null,HopDongLaoDong_2022 =null " +
    //            "FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung] " +
    //            "where TieuDeTuyenDung is not null and NgayHetHan is not null and DN_ID in (select DN_ID from DoanhNghiep) " +
    //            "and TinhTrangHoSo is not null and TinhTrangHoSo in (select TinhTrangHoSo from DM_TinhTrangPheDuyetHoSo) " +
    //            "and YeuCauNganh_ID is not null and YeuCauNganh_ID in(select YeuCauNganh_ID from DM_NganhLaoDong) " +
    //            "and YeuCauNghe_ID is not null and YeuCauNghe_ID in (select YeuCauNghe_ID from DM_NgheLaoDong) " +
    //            "and ChucDanh_ID is not null and ChucDanh_ID in (select ChucDanh_ID from DM_ChucDanh) " +
    //            "and YeuCauTrinhDo_ID is not null and YeuCauTrinhDo_ID in (select YeuCauTrinhDo_ID from DM_TrinhDoChuyenMon) " +
    //            "and ThoiGianLamViec_ID is not null and ThoiGianLamViec_ID in (select ThoiGianLamViec_ID from DM_ThoiGianLamViec) " +
    //            "order by NgayTao desc").ToList();
                
    //        return model;
    //    }
    //}
}