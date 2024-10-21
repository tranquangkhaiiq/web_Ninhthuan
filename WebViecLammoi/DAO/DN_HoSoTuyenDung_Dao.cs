using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using WebViecLammoi.Models;
using WebViecLammoi.Models.table_mirro;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Data.Entity.Core.Common.CommandTrees;

namespace WebViecLammoi.DAO
{
    public class DN_HoSoTuyenDung_Dao
    {
        VLDB db = new VLDB();
        public static List<DoanhNghiep_TuyenDung> model_DNTD_Job = new List<DoanhNghiep_TuyenDung>();
        public static List<DoanhNghiep_TuyenDung> model_DNTD_Job_Pay = new List<DoanhNghiep_TuyenDung>();
        public static List<DoanhNghiep_TuyenDung> model_ListTD = new List<DoanhNghiep_TuyenDung>();
        public static List<DoanhNghiep_TuyenDung> model_ListTD_Pay = new List<DoanhNghiep_TuyenDung>();
        public static string getSession_JobType_kh()
        {
            string session = "";
            if (HttpContext.Current.Session["customSTR"] != null && HttpContext.Current.Session["customSTR"].ToString() != "")
            {
                session = "custom";
            }
            else if (HttpContext.Current.Session["Location"] != null && HttpContext.Current.Session["Location"].ToString() != "")
            {
                session = "location";
            }
            return session;
        }
        //private static List<DoanhNghiep_TuyenDung> LinQ_DN_TD_StrAPI(VLDB db, string str)
        //{
        //    var mode = new List<DoanhNghiep_TuyenDung>();
        //    if (str.Trim() == "")
        //    {
        //        mode = LinQ_DN_TD(db);
        //    }
        //    else
        //    {
        //        mode = LinQ_DN_TD(db).Where(p => p.DN_ID.ToString().ToLower().Contains(str.ToLower())
        //            || p.TuyenDung_ID.ToString().ToLower().Contains(str.ToLower()) || p.TieuDeTuyenDung.ToLower().Contains(str.ToLower())).ToList();

        //    }
        //    return mode;

        //}
        public static List<DoanhNghiep_TuyenDung> LinQ_DN_TD(VLDB db)
        {
            var model = db.Database.SqlQuery<DoanhNghiep_TuyenDung>("exec GetBase_DN_TD_khai").ToList();
            return model;
        }
        public static List<DoanhNghiep_TuyenDung> LinQ_DN_TD_Pay(VLDB db)
        {
            var model = db.Database.SqlQuery<DoanhNghiep_TuyenDung>("exec GetBase_DN_TD_khai_Pay").ToList();
            return model;
        }
        public static List<DoanhNghiep_TuyenDung> LinQ_Job_left(VLDB db)
        {
            var model = db.Database.SqlQuery<DoanhNghiep_TuyenDung>("exec GetBase_DN_TD_khai_Job")
                                .OrderByDescending(kh=>kh.NoiBat)
                                .ThenByDescending(kh=>kh.TuyenDung_ID)
                                .ToList();
            return model;
        }
        public static List<DoanhNghiep_TuyenDung> LinQ_Job_left_Pay(VLDB db)
        {
            var model = db.Database.SqlQuery<DoanhNghiep_TuyenDung>("exec GetBase_DN_TD_khai_Job_Pay")
                                .OrderByDescending(kh => kh.NoiBat)
                                .ThenByDescending(kh => kh.TuyenDung_ID)
                                .ToList();
            return model;
        }
        //Lấy tất cả cho MainJob
        public List<DoanhNghiep_TuyenDung> LinQ_DN_TD_Job(string str, bool key, int id,List<int>Ids)
        {
            var model = new List<DoanhNghiep_TuyenDung>();
            var model_DNTD = new List<DoanhNghiep_TuyenDung>();
            model_DNTD = (key == true) ? model_DNTD_Job_Pay : model_DNTD_Job;
            if (str == "" && id == 0)
            {
                model = model_DNTD;
            }
            if (str == "duoi10tr" && id == 0)
            {
                model = model_DNTD
                   .Where(kh => kh.LuongDen < 10 && kh.LuongDen > 0 || (kh.LuongDen > 1000000 && kh.LuongDen < 10000000)).ToList();
            }
            if (str == "10den20tr" && id == 0)
            {
                model = model_DNTD
                    .Where(kh => kh.LuongTu >= 10 && kh.LuongDen <= 20 || (kh.LuongTu > 10000000 && kh.LuongDen <= 20000000)).ToList();
            }
            if (str == "hon20tr" && id == 0)
            {
                model = model_DNTD
                    .Where(kh => kh.LuongDen > 20 && kh.LuongDen < 500 || (kh.LuongDen > 20000000)).ToList();
            }
            if (str == "thoathuan" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.LuongTu == 0 && kh.LuongDen == 0).ToList();
            }
            if (str == "noibat" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.NoiBat == true).ToList();
            }
            if (str == "toanthoigian" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 4).ToList();
            }
            if (str == "banthoigian" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 2).ToList();
            }
            if (str == "giohanhchinh" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 1).ToList();
            }
            if (str == "theoca" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 3).ToList();
            }
            if (str == "khac" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 0 || kh.ThoiGianLamViec_ID == 5 || kh.ThoiGianLamViec_ID == 6).ToList();
            }
            if (str == "TDDaXem" && id == 0)
            {
                model = model_DNTD
                        .Where(kh => Ids.Contains(kh.TuyenDung_ID)).ToList();
            }
            if (id > 0 && str == "")
            {
                model = model_DNTD
                        .Where(kh => kh.NoiLamViec_HuyenID == id)
                        .ToList();
            }
            return model;
        }
        public List<DoanhNghiep_TuyenDung> LinQ_DN_TD_Job_Skip(int sec, bool key, int pageSize, string str, int id,List<int>Ids)
        {
            var model = new List<DoanhNghiep_TuyenDung>();
            var model_DNTD = new List<DoanhNghiep_TuyenDung>();
            model_DNTD = (key == true) ? model_DNTD_Job_Pay : model_DNTD_Job;
            if (str == "")
            {
                model = model_DNTD
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "duoi10tr")
            {
                model = model_DNTD
                    .Where(kh => kh.LuongDen < 10 && kh.LuongDen > 0 || (kh.LuongDen > 1000000 && kh.LuongDen < 10000000))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "10den20tr")
            {
                model = model_DNTD
                    .Where(kh => kh.LuongTu > 10 && kh.LuongDen <= 20 || (kh.LuongTu > 10000000 && kh.LuongDen <= 20000000))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "hon20tr")
            {
                model = model_DNTD
                    .Where(kh => kh.LuongDen > 20 && kh.LuongDen < 500 || (kh.LuongDen > 20000000))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "thoathuan")
            {
                model = model_DNTD
                        .Where(kh => kh.LuongTu == 0 && kh.LuongDen == 0)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "noibat")
            {
                model = model_DNTD
                        .Where(kh => kh.NoiBat == true)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "toanthoigian")
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 4)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "banthoigian")
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 2)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "giohanhchinh")
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 1)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "theoca")
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 3)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "khac")
            {
                model = model_DNTD
                        .Where(kh => kh.ThoiGianLamViec_ID == 0 || kh.ThoiGianLamViec_ID == 5 || kh.ThoiGianLamViec_ID == 6)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (str == "TDDaXem")
            {
                model = model_DNTD
                        .Where(kh => Ids.Contains(kh.TuyenDung_ID))
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            if (id > 0)
            {
                model = model_DNTD
                        .Where(kh => kh.NoiLamViec_HuyenID == id)
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
            }
            return model;
        }
//Lấy tất cả cho MainJob
        public List<DoanhNghiep_TuyenDung> GetListTDbyNghanhNghe(int sec, bool key, int pageSize, string searchId)
        {
            
            var model = new List<DoanhNghiep_TuyenDung>();
            var model_DNTD = new List<DoanhNghiep_TuyenDung>();
            model_DNTD = (key == true) ? model_DNTD_Job_Pay : model_DNTD_Job;
            if (searchId== "")
            {
                model = model_DNTD
                        .Skip(sec * pageSize)
                        .Take(pageSize)
                        .ToList();
                return model;
            }
            else
            {
                var nn = int.Parse(searchId);
                var modelmapSub = db.aspnet_mapSubs.Where(kh => kh.mapautoId == nn)
                        .Select(kh => kh.Substr)
                        .ToList();
                if (modelmapSub != null)
                {
                    model = model_DNTD.Where(kh => modelmapSub.Any(x=> kh.TieuDeTuyenDung.ToLower().Contains(x)))
                            .Skip(sec * pageSize)
                            .Take(pageSize)
                            .ToList();
                    return model;
                }
                return null;
            }
        }
        public List<aspnet_mapauto> GetAllmapaotu()
        {
            var model = db.aspnet_mapautos.ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung> GetCountTDbyNghanhNghe(string search, bool key)
        {
            int nn = int.Parse(search);
            var modelmapSub = db.aspnet_mapSubs.Where(kh => kh.mapautoId == nn)
                        .Select(kh => kh.Substr)
                        .ToList();
            var model = new List<DoanhNghiep_TuyenDung>();
            var model_DNTD = new List<DoanhNghiep_TuyenDung>();
            model_DNTD = (key == true) ? model_DNTD_Job_Pay : model_DNTD_Job;
            if (modelmapSub != null)
            {
                model = model_DNTD.Where(kh => modelmapSub.Any(x=> kh.TieuDeTuyenDung.ToLower().Contains(x)))
                        .ToList();
                return model;
            }
            return null;
        }
        public List<DoanhNghiep_TuyenDung> GetmapTDbyTieudeTVL(int KH_ID, bool key)
        {
            ////get hồ sơ được duyệt của NTV
            var modelhsduyet = NTV_HoSoXinViec_Dao.LinQ_HSTV(db).Where(kh=>kh.KH_ID==KH_ID);
            var modelallmap = GetAllmapaotu().Select(kh => kh.Id).ToList();
            /////
            var model = new List<DoanhNghiep_TuyenDung>();
            var model_DNTD = new List<DoanhNghiep_TuyenDung>();
            model_DNTD = (key == true) ? model_DNTD_Job_Pay : model_DNTD_Job;
            for (int i = 0; i < modelallmap.Count(); i++)
            {
                int modelTVL = 0;
                int id = modelallmap[i];
                var modelmapSub = db.aspnet_mapSubs.Where(kh => kh.mapautoId == id)
                    .Select(kh => kh.Substr)
                    .ToList();
                if (modelmapSub != null)
                {
                    modelTVL = modelhsduyet.Where(kh => modelmapSub.Any(x => kh.TenHoSo.ToLower().Contains(x)))
                            .Count();
                    if (modelTVL > 0)
                    {
                        var modelTD = model_DNTD.Where(kh => modelmapSub.Any(x => kh.TieuDeTuyenDung.ToLower().Contains(x)))
                                        .Take(4)
                                        .ToList();
                        model.AddRange(modelTD);
                    }
                }
            }
            if(model != null)
            {
                return model.Distinct().ToList();
            }
            return null;
        }
        public static List<DoanhNghiep_TuyenDung> GetList_DuyetHot_NICEAD_Home_Index(bool key, int skip, int take)
        {
            var mode = new List<DoanhNghiep_TuyenDung>();
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTD_Pay : model_ListTD;
            if (skip == 0)
            {
                mode = model_List.Where(p => p.NgayNoiBat != null && p.NgayNoiBat > DateTime.Now && p.NoiBat ==true)
                                        .OrderByDescending(p => p.NgayCapNhat)
                                        .Take(take)
                                        .ToList();
            }
            else
            {
                mode = model_List.Where(p => p.NgayNoiBat != null && p.NgayNoiBat > DateTime.Now && p.NoiBat==true)
                                        .OrderByDescending(p => p.NgayCapNhat)
                                        .Skip(skip)
                                        .Take(take)
                                        .ToList();
            }

            return mode;
        }

        public static List<DoanhNghiep_TuyenDung> GetListDN_phuhop_NoSkip(bool key, int Sec, int pageSize, int ngheLDID)
        {
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTD_Pay : model_ListTD;
            var mode = model_List.Where(p => p.YeuCauNghe_ID == ngheLDID)
                .OrderByDescending(p => p.NgayCapNhat)
                .Skip(Sec * pageSize)
                .Take(pageSize)
                .ToList();
            return mode;
        }
        public static List<DoanhNghiep_TuyenDung> GetListTD_DaXem_Skip(bool key, int Sec, List<int> Id, int pageSize)
        {
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTD_Pay : model_ListTD;
            var mode = model_List.Where(p => Id.Contains(p.TuyenDung_ID))
                .OrderByDescending(p => p.NgayCapNhat)
                .Skip(Sec * pageSize)
                .Take(pageSize)
                .ToList();
            return mode;
        }
        public static List<DoanhNghiep_TuyenDung> GetListTD_DaXem_NoSkip(string key, int Sec, List<int> Id, int pageSize)
        {
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == "true") ? model_ListTD_Pay : model_ListTD;
            var mode = model_List.Where(p => Id.Contains(p.TuyenDung_ID))
                .OrderByDescending(p => p.NgayCapNhat)
                .Take(pageSize * (Sec + 1))
                .ToList();
            return mode;
        }
        public static List<DoanhNghiep_TuyenDung> GetList_bycty(bool key, int DN_ID, int TuyendungID)
        {
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTD_Pay : model_ListTD;
            var mode = model_List.Where(p => p.DN_ID == DN_ID && p.TuyenDung_ID != TuyendungID)
                        .OrderByDescending(p => p.NgayCapNhat)
                        .ToList();
            return mode;
        }
        //chua xu ly Pay
        public DoanhNghiep_TuyenDung GetList_byTuyendungID(int TuyendungID)
        {
            var mode = model_ListTD.FirstOrDefault(p => p.TuyenDung_ID == TuyendungID);
            return mode;
        }
        ///////////////////////////////////////////////////////////////

        //ok
        public static List<DoanhNghiep_TuyenDung> GetListTD_luongcao(bool key, int skip, int take)
        {
            var mode = new List<DoanhNghiep_TuyenDung>();
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTD_Pay : model_ListTD;
            if (skip == 0)
            {
                mode = model_List.Where(kh => kh.LuongTu > 9000000 || (kh.LuongTu > 8 && kh.LuongDen < 300) || kh.TieuDeTuyenDung.ToLower().Contains("quản lý")
                    || kh.TieuDeTuyenDung.ToLower().Contains("quản đốc") || kh.TieuDeTuyenDung.ToLower().Contains("trưởng nhóm")
                    || kh.TieuDeTuyenDung.ToLower().Contains("leader") || kh.TieuDeTuyenDung.ToLower().Contains("trưởng bộ phận")
                    || kh.TieuDeTuyenDung.ToLower().Contains("trưởng phòng") || kh.TieuDeTuyenDung.ToLower().Contains("phó phòng")
                    || kh.TieuDeTuyenDung.ToLower().Contains("giám đốc") || kh.TieuDeTuyenDung.ToLower().Contains("phó giám đốc"))
                .OrderByDescending(p => p.LuongTu)
                .Take(take)
                .ToList();
            }
            else
            {
                mode = model_List.Where(kh => kh.LuongTu > 9000000 || (kh.LuongTu > 8 && kh.LuongDen < 300))
                .OrderByDescending(p => p.NgayCapNhat)
                .Skip(skip)
                .Take(take)
                .ToList();
            }
            return mode;
        }
        public static List<DoanhNghiep_TuyenDung> GetListTD_moinhat(bool key, int skip, int take)
        {
            var mode = new List<DoanhNghiep_TuyenDung>();
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTD_Pay : model_ListTD;
            if (skip == 0)
            {
                mode = model_List
                .OrderByDescending(p => p.NgayCapNhat)
                .Take(take)
                .ToList();
            }
            else
            {
                mode = model_List
                .OrderByDescending(p => p.NgayCapNhat)
                .Skip(skip)
                .Take(take)
                .ToList();
            }

            return mode;
        }

        //DANH SÁCH HỒ SƠ TUYỂN DUNG XEM NHIỀU
        public static List<DoanhNghiep_TuyenDung> GetListTD_XemNhieu(string key)
        {
            var model_List = new List<DoanhNghiep_TuyenDung>();
            model_List = (key == "true") ? model_ListTD_Pay : model_ListTD; 
            var mode = model_List
                    .OrderByDescending(p => p.SoLuotXem)
                    .Take(6)
                    .ToList();
            return mode;
        }
        //Không quan tâm đã duyệt hay chưa, có hiển thị web không?
        private static List<DoanhNghiep_TuyenDung> LinQ_TDbyDN(VLDB db, int DNID)
        {
            var model = db.Database.SqlQuery<DoanhNghiep_TuyenDung>("exec GetBase_TD_khaibyDN @DN_ID", new SqlParameter("DN_ID", DNID)).ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung> GetDSHSbyDN(int DNID)
        {
            var model = LinQ_TDbyDN(db, DNID)
                        .OrderBy(kh=>kh.TinhTrangHoSo)
                        .ToList();
            return model;
        }
        public int GetTuyenDungbyDNID(int DNID)
        {
            var model = LinQ_TDbyDN(db, DNID).Count();
            return model;
        }
        public int GetTuyenDungbyDNID_tinhtrang(int DNID, int tinhtrang)
        {
            var model = LinQ_TDbyDN(db, DNID).Where(kh => kh.TinhTrangHoSo == tinhtrang).Count();
            return model;
        }
        public int GetTuyenDungbyDNID_Duyet_hethan(int DNID)
        {
            var model = LinQ_TDbyDN(db, DNID).Where(kh => kh.TinhTrangHoSo == 3 && kh.NgayHetHan < DateTime.Now).Count();
            return model;
        }
        public int GetTuyenDungbyDNID_Duyet_conhan(int DNID)
        {
            var model = LinQ_TDbyDN(db, DNID).Where(kh => kh.TinhTrangHoSo == 3 && kh.NgayHetHan > DateTime.Now).Count();
            return model;
        }
        public int Tinchuaxem(int DNID)
        {
            var model = db.SMS_AdmintoDNs.Where(kh => kh.DNID == DNID && kh.DaXem ==false).Count();
            return model;
        }
        public int GetTuyenDungbyDNID_hienthiweb(int DNID, bool hienthi)
        {
            var model = LinQ_TDbyDN(db, DNID).Where(kh => kh.TinhTrangHoSo == 3 && kh.HienThiWeb == hienthi).Count();
            return model;
        }
    }
}