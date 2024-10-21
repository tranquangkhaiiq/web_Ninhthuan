using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class CandidateConllerADMIN
    {

        //***************Admin_không quan tâm NgayHetHan, và tình trạng Hienthiweb*************************
        public static List<KhachHang_TimViecLam> GetListCan_File(VLDB db, int Sec, int pageSize)
        {
            var Model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 3 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != "")
                .OrderByDescending(c => c.TimViec_ID)
                .Skip(Sec * pageSize)
                .Take(pageSize)
                .ToList();
            return Model;
        }
        public static int GetPageCountCan_File(VLDB db)
        {
            int model = 0;
            model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 3 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != "")
                                                .Count();
            return model;
        }
        public static List<KhachHang_TimViecLam> GetListCan_File_Search(VLDB db, int Sec, int pageSize, string strTK)
        {
            var model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 3 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != ""
                                                && (c.KhachHangs.CMND.Contains(strTK) || c.TenHoSo.Contains(strTK)
                                                || c.KhachHangs.HoTen.Contains(strTK) || c.TimViec_ID.ToString().Contains(strTK)))
                        .OrderByDescending(c => c.TimViec_ID)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            return model;
        }
        public static int GetPageCountCan_File_Search(VLDB db, string strTK)
        {
            var model = 0;
            model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 3 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != ""
                                                && (c.KhachHangs.CMND.Contains(strTK) || c.TenHoSo.Contains(strTK)
                                                || c.KhachHangs.HoTen.Contains(strTK) || c.TimViec_ID.ToString().Contains(strTK)))
                                                .Count();
            return model;
        }
        public static List<KhachHang_TimViecLam> GetListCheck_CanADMIN(VLDB db, int Sec, int pageSize)
        {
            var model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 2 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != "")
                                .OrderByDescending(c => c.TimViec_ID)
                                .Skip(Sec * pageSize)
                                .Take(pageSize)
                                .ToList();
            return model;
        }
        public static int GetPageCountCheck_CanADMIN(VLDB db)
        {
            var model = 0;
            model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 2 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != "")
                                                .Count();
            return model;

        }
        public static List<KhachHang_TimViecLam> GetListCan_FileCheckAllADMIN(VLDB db)
        {
            var Model = db.KhachHang_TimViecLam.Where(c => c.TinhTrangPheDuyetHoSo_ID == 2 && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID) != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).HocVan_ID != null
                                        && db.KhachHangs.FirstOrDefault(kh => kh.KH_ID == c.KH_ID).TamTru_Huyen_ID != null
                                                && c.NoiLamViecMongMuon_ID != null && db.DM_DiaChi.FirstOrDefault(kh => kh.Id == c.NoiLamViecMongMuon_ID) != null
                                                && c.ChucDanhMongMuon != null && db.DM_ChucDanh.FirstOrDefault(kh => kh.ChucDanh_ID == c.ChucDanhMongMuon) != null
                                                && c.ThoiGianLamViecMongMuon != null && db.DM_ThoiGianLamViec.FirstOrDefault(kh => kh.ThoiGianLamViec_ID == c.ThoiGianLamViecMongMuon) != null
                                                && c.LoaiHinhDNMongMuon_ID != null && db.DM_LoaiHinhDoanhNghiep.FirstOrDefault(kh => kh.LoaiHinhDoanhNghiep_ID == c.LoaiHinhDNMongMuon_ID) != null
                                                && c.NgheMongMuon_ID != null && db.DM_NgheKinhDoanh.FirstOrDefault(kh => kh.NgheKD_ID == c.NgheMongMuon_ID) != null
                                                && c.NganhMongMuon_ID != null && db.DM_NganhKinhDoanh.FirstOrDefault(kh => kh.Nganh_ID == c.NganhMongMuon_ID) != null
                                                && c.TenHoSo != "").ToList();
            return Model;
        }
    }
}