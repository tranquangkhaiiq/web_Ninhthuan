using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using WebViecLammoi.Models;
using System.Drawing.Printing;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;

namespace WebViecLammoi.DAO
{
    public class NTV_HoSoXinViec_Dao
    {
        VLDB dbc = new VLDB();
        public static List<KhachHang_TimViecLam> model_kh_tvl = new List<KhachHang_TimViecLam>();
        public static List<KhachHang_TimViecLam> model_ListNTV = new List<KhachHang_TimViecLam>();
        
        public static string getSession_CanType_kh()
        {
            string session = "";
            if (HttpContext.Current.Session["customSTRNTV"] != null && HttpContext.Current.Session["customSTRNTV"].ToString() != "")
            {
                session = "custom";
            }
            else if (HttpContext.Current.Session["LocationNTV"] != null && HttpContext.Current.Session["LocationNTV"].ToString() != "")
            {
                session = "location";
            }
            return session;
        }
        public static List<KhachHang_TimViecLam> LinQ_HSTV(VLDB db)
        {
            //Load o trang Home/index thong qua bien model_ListNTV
            var model = db.Database.SqlQuery<KhachHang_TimViecLam>("exec GetBase_KH_NTV_khai").ToList();
            return model;
        }
        private static List<KhachHang_TimViecLam> LinQ_HSTV_StrAPI(VLDB db, string str)
        {
            var mode = new List<KhachHang_TimViecLam>();
            if (str.Trim() == "")
            {
                mode = model_ListNTV;
            }
            else
            {
                mode = model_ListNTV.Where(p=>p.TenHoSo.ToLower().Contains(str.ToLower())).ToList();
            }
            return mode;
        }
        //01/04/2020
        public static List<KhachHang_TimViecLam> GetListNTV_phuhop_NoSkip(VLDB db, int Sec, int pageSize, int nganhKDID,string strnganh, string strtencty)
        {
            var mode = model_ListNTV.Where(p => p.NganhMongMuon_ID == nganhKDID || p.TenHoSo.ToLower().Contains(strnganh.ToLower())
                            || p.TenHoSo.ToLower().Contains(strtencty.ToLower()))
                    .OrderByDescending(p => p.NgayCapNhat)
                    .Take(pageSize * (Sec + 1))
                    .ToList();
            return mode;
        }
        public static int GetListNTV_phuhop_NoSkip_count(VLDB db, int nganhKDID, string strnganh,string strtencty)
        {
            var mode = model_ListNTV.Where(p => p.NganhMongMuon_ID == nganhKDID || p.TenHoSo.ToLower().Contains(strnganh.ToLower())
                            || p.TenHoSo.ToLower().Contains(strtencty.ToLower()))
                    .Count();
            return mode;
        }
        public List<KhachHang_TimViecLam> LinQ_NTV_Main_Skip(int sec, int pageSize, string str, int id,List<int>Ids)
        {
            var model = new List<KhachHang_TimViecLam>();
            if (str == "")
            {
                model = model_kh_tvl
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "duoi10tr")
            {
                model = model_kh_tvl
                    .Where(kh => kh.MucLuongMongMuonDen < 10 && kh.MucLuongMongMuonDen > 0 ||
                    (kh.MucLuongMongMuonDen > 1000000 && kh.MucLuongMongMuonDen < 10000000))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "10den20tr")
            {
                model = model_kh_tvl
                    .Where(kh => kh.MucLuongMongMuonTu >= 10 && kh.MucLuongMongMuonDen <= 20 ||
                    (kh.MucLuongMongMuonTu > 10000000 && kh.MucLuongMongMuonDen <= 20000000))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "hon20tr")
            {
                model = model_kh_tvl
                    .Where(kh => kh.MucLuongMongMuonDen > 20 && kh.MucLuongMongMuonDen < 500 || (kh.MucLuongMongMuonDen > 20000000))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "thoathuan")
            {
                model = model_kh_tvl
                        .Where(kh => kh.MucLuongMongMuonTu == 0 && kh.MucLuongMongMuonDen == 0)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "xemnhieu")
            {
                model = model_kh_tvl
                        .OrderByDescending(kh => kh.LuotXem)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "toanthoigian")
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 4)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "banthoigian")
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 2)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "giohanhchinh")
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 1)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "theoca")
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 3)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "khac")
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 0 || kh.ThoiGianLamViecMongMuon == 5 || kh.ThoiGianLamViecMongMuon == 6)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "NTVDaXem")
            {
                model = model_kh_tvl
                        .Where(kh => Ids.Contains(kh.TimViec_ID))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (id > 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.NoiLamViecMongMuon_ID == id)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            return model;
        }
        public static List<KhachHang_TimViecLam> LinQ_TimViec_left(VLDB db)
        {
            //load o trang candidate =>model_kh_tvl
            var model = db.Database.SqlQuery<KhachHang_TimViecLam>("exec GetBase_KH_NTV_khai_NTV").ToList();
            return model;
        }
        public List<KhachHang_TimViecLam> LinQ_NTV_Main(string str, int id,List<int>Ids)
        {
            var model = new List<KhachHang_TimViecLam>();
            if (str == "" && id == 0)
            {
                model = model_kh_tvl;
            }
            if (str == "duoi10tr" && id == 0)
            {
                model = model_kh_tvl
                   .Where(kh => kh.MucLuongMongMuonDen < 10 && kh.MucLuongMongMuonDen > 0 ||
                   (kh.MucLuongMongMuonDen > 1000000 && kh.MucLuongMongMuonDen < 10000000)).ToList();
            }
            if (str == "10den20tr" && id == 0)
            {
                model = model_kh_tvl
                    .Where(kh => kh.MucLuongMongMuonTu >= 10 && kh.MucLuongMongMuonDen <= 20 ||
                    (kh.MucLuongMongMuonTu > 10000000 && kh.MucLuongMongMuonDen <= 20000000)).ToList();
            }
            if (str == "hon20tr" && id == 0)
            {
                model = model_kh_tvl
                    .Where(kh => kh.MucLuongMongMuonDen > 20 && kh.MucLuongMongMuonDen < 500 ||
                    (kh.MucLuongMongMuonDen > 20000000)).ToList();
            }
            if (str == "thoathuan" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.MucLuongMongMuonTu == 0 && kh.MucLuongMongMuonDen == 0).ToList();
            }
            if (str == "xemnhieu" && id == 0)
            {
                model = model_kh_tvl
                        .OrderByDescending(kh=>kh.LuotXem).ToList();
            }
            if (str == "toanthoigian" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 4).ToList();
            }
            if (str == "banthoigian" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 2).ToList();
            }
            if (str == "giohanhchinh" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 1).ToList();
            }
            if (str == "theoca" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 3).ToList();
            }
            if (str == "khac" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => kh.ThoiGianLamViecMongMuon == 0 || kh.ThoiGianLamViecMongMuon == 5 || kh.ThoiGianLamViecMongMuon == 6).ToList();
            }
            if (str == "NTVDaXem" && id == 0)
            {
                model = model_kh_tvl
                        .Where(kh => Ids.Contains(kh.TimViec_ID)).ToList();
            }
            
            if (id > 0 && str == "")
            {
                model = model_kh_tvl
                        .Where(kh => kh.NoiLamViecMongMuon_ID == id)
                        .ToList();
            }
            return model;
        }
        
        public static Object GetList_HSNTV_Cty(VLDB db, int Id, int KH_ID)
        {
            var mode = model_ListNTV
                    .Where(p => p.TimViec_ID != Id && p.KH_ID == KH_ID)
                    .OrderByDescending(p => p.NgayCapNhat)
                    .ToList();
            return mode;
        }
        public List<KhachHang_TimViecLam> GetmapTVLbyTieudeTD(int TD_ID)
        {
            model_kh_tvl = LinQ_TimViec_left(dbc);
            var modelhsduyet = new DAO.DN_HoSoTuyenDung_Dao().GetDSHSbyDN(TD_ID).Where(kh=>kh.TinhTrangHoSo == 3);
            var modelallmap = new DAO.DN_HoSoTuyenDung_Dao().GetAllmapaotu().Select(kh => kh.Id).ToList();
            /////
            var model = new List<KhachHang_TimViecLam>();
            var model_TVL = new List<KhachHang_TimViecLam>();
            
            model_TVL = model_kh_tvl;
            ////duyệt bang mapaotu
            for (int i = 0; i < modelallmap.Count(); i++)
            {
                int modeltotalTD = 0;
                int id = modelallmap[i];
                var modelmapSub = dbc.aspnet_mapSubs.Where(kh => kh.mapautoId == id)
                    .Select(kh => kh.Substr)
                    .ToList();
                if (modelmapSub != null)
                {
                    ////Dò trong ds hồ sơ đã duyệt trước
                    modeltotalTD = modelhsduyet.Where(kh => modelmapSub.Any(x => kh.TieuDeTuyenDung.ToLower().Contains(x)))
                            .Count();
                    if (modeltotalTD > 0)
                    {
                        ////mang mapSub ra dò ds hồ sơ ntv
                        var modelTD = model_TVL.Where(kh => modelmapSub.Any(x => kh.TenHoSo.ToLower().Contains(x)))
                                        .Take(4)
                                        .ToList();
                        model.AddRange(modelTD);
                    }
                }
            }
            if (model != null)
            {
                return model.Distinct().ToList();
            }
            return null;
        }
        //Thứ tự ưu tiên:     LuotXem
        public static List<KhachHang_TimViecLam> GetListNTV_XemNhieu(VLDB db, int Skip, int take)
        {
            var mode = new List<KhachHang_TimViecLam>();
            if (Skip == 0)
            {
                mode = model_ListNTV
                        .OrderByDescending(p => p.LuotXem)
                        .Take(take)
                        .ToList();
            }
            else
            {
                mode = model_ListNTV
                        .OrderByDescending(p => p.LuotXem)
                        .Skip(Skip)
                        .Take(take)
                        .ToList();
            }
            
            return mode;
        }
        //ok
        //Thứ tự ưu tiên:     Ngaycapnhat
        public static List<KhachHang_TimViecLam> GetListNTV_moinhat(VLDB db,int skip, int take)
        {
            var mode = new List<KhachHang_TimViecLam>();
            if (skip == 0)
            {
                mode = model_ListNTV
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take(take)
                        .ToList();
            }
            else
            {
                mode = model_ListNTV
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(skip)
                        .Take(take)
                        .ToList();
            }
            return mode;
        }
        public static Object GetListNTV_Skip(VLDB db, int Sec, int pageSize)
        {
            var mode = model_ListNTV
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            return mode;
        }
        public static Object GetListNTV_NoSkip(VLDB db, int Sec, int pageSize)
        {
            var mode = model_ListNTV
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take(pageSize * (Sec + 1))
                        .ToList();
            return mode;
        }
        public static Object GetListCustomNTV_NoSkip(VLDB db, int Sec, int pageSize, string strTK)
        {
            var mode = LinQ_HSTV_StrAPI(db, strTK)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take((Sec + 1) * pageSize)
                        .ToList();
            return mode;
        }
        public static Object GetListCustomNTV_Skip(VLDB db, int Sec, int pageSize, string strTK)
        {
            var mode = LinQ_HSTV_StrAPI(db, strTK)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            return mode;
        }
        public static Object GetListLocationNTV_Skip(VLDB dbc, int Sec, int pageSize, int Id)
        {

            if (HttpContext.Current.Session["searchStrNTV"] != null)
            {
                string strTK = HttpContext.Current.Session["searchStrNTV"].ToString();
                var model = LinQ_HSTV_StrAPI(dbc, strTK)
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Huyen_ID == Id)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
                if (Id == 9999)
                    model = LinQ_HSTV_StrAPI(dbc, strTK)
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Tinh_ID != 42)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
                return model;
            }
            else
            {
                var model = model_ListNTV
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Huyen_ID == Id)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
                if (Id == 9999)
                    model = model_ListNTV
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Tinh_ID != 42)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Skip(Sec * pageSize)
                        .Take(pageSize)
                        .ToList();
                return model;
            }
        }
        public static Object GetListLocationNTV_NoSkip(VLDB dbc, int Sec, int pageSize, int Id)
        {
            if (HttpContext.Current.Session["searchStrNTV"] != null)
            {
                string strTK = HttpContext.Current.Session["searchStrNTV"].ToString();
                var model = LinQ_HSTV_StrAPI(dbc, strTK)
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Huyen_ID == Id)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take((Sec + 1) * pageSize)
                        .ToList();
                if (Id == 9999)
                    model = LinQ_HSTV_StrAPI(dbc, strTK)
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Tinh_ID != 42)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take((Sec + 1) * pageSize)
                        .ToList();
                return model;
            }
            else
            {
                var model = model_ListNTV
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Huyen_ID == Id)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take((Sec + 1) * pageSize)
                        .ToList();
                if (Id == 9999)
                    model = model_ListNTV
                        .Where(p => dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == p.KH_ID).TamTru_Tinh_ID != 42)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .Take((Sec + 1) * pageSize)
                        .ToList();
                return model;
            }
        }
        //nhutcc
        public static List<KhachHang_TimViecLam> GetListNTV_XemNhieu(VLDB db)
        {
            var mode = model_ListNTV
                        .OrderByDescending(p => p.LuotXem)
                        .Take(7)
                        .ToList();
            return mode;
        }
        private static List<KhachHang_TimViecLam> LinQ_HSTV_byKH(VLDB db, int KHID)
        {
            var model = db.Database.SqlQuery<KhachHang_TimViecLam>("exec GetBase_NTV_khaibyKH @KH_ID", new SqlParameter("KH_ID", KHID)).ToList();
            return model;
        }
        public List<KhachHang_TimViecLam> GetListTimViecbyKHID(int KHID)
        {
            var model = LinQ_HSTV_byKH(dbc,KHID).ToList();
            return model;
        }
        public List<KhachHang_TimViecLam> GetListTimViecbyKHID_conhan(int KHID)
        {
            var model = LinQ_HSTV_byKH(dbc,KHID).Where(kh => kh.NgayHoSoHetHan > DateTime.Now &&
                                kh.HienThiTrenWeb == true && kh.TinhTrangPheDuyetHoSo_ID == 3).ToList();
            return model;
        }
        public int GetTimViecbyKHID(int KHID)
        {
            var model = LinQ_HSTV_byKH(dbc,KHID).Count();
            return model;
        }
        public int GetTimViecbyKHID_tinhtrang(int KHID, int tinhtrang)
        {
            var model = LinQ_HSTV_byKH(dbc,KHID).Where(kh => kh.TinhTrangPheDuyetHoSo_ID == tinhtrang).Count();
            return model;
        }
        public int GetTimViecbyKHID_Duyet_hethan(int KHID)
        {
            var model = LinQ_HSTV_byKH(dbc, KHID).Where(kh => kh.TinhTrangPheDuyetHoSo_ID == 3 && kh.NgayHoSoHetHan < DateTime.Now).Count();
            return model;
        }
        public int GetTimViecbyKHID_Duyet_conhan(int KHID)
        {
            var model = LinQ_HSTV_byKH(dbc, KHID).Where(kh => kh.TinhTrangPheDuyetHoSo_ID == 3 && kh.NgayHoSoHetHan > DateTime.Now).Count();
            return model;
        }
        public int GetTimViecbyKHID_hienthiweb(int KHID, bool hienthi)
        {
            var model = LinQ_HSTV_byKH(dbc, KHID).Where(kh => kh.HienThiTrenWeb == hienthi).Count();
            return model;
        }
    }
}