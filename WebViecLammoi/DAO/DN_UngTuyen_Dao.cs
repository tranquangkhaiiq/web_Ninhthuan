using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class DN_UngTuyen_Dao
    {
        public static bool Insert_DoanhNghiep_UngTuyen(VLDB dbc, DoanhNghiep_UngTuyen model)
        {
            var kq = dbc.Database.ExecuteSqlCommand("Exec Insert_DoanhNghiep_UngTuyen_khai @Id,@TuyenDung_ID,@KH_ID,@smsNTVtoDN," +
                "@DN_Daxem,@DN_LayTTLienHe,@NgayUngTuyen,@NTV_TrangThaiUngTuyen,@NgayUpdate,@NTV_LyDoHuy,@File_CVUngTuyen,@DN_TuChoi",
                new SqlParameter("Id", model.Id),
                new SqlParameter("TuyenDung_ID", model.TuyenDung_ID),
                new SqlParameter("KH_ID", model.KH_ID),
                new SqlParameter("smsNTVtoDN", model.smsNTVtoDN),
                new SqlParameter("DN_Daxem", model.DN_Daxem),
                new SqlParameter("DN_LayTTLienHe", model.DN_LayTTLienHe),
                new SqlParameter("NgayUngTuyen", model.NgayUngTuyen),
                new SqlParameter("NTV_TrangThaiUngTuyen", model.NTV_TrangThaiUngTuyen),
                new SqlParameter("NgayUpdate", model.NgayUpdate),
                new SqlParameter("NTV_LyDoHuy", model.NTV_LyDoHuy),
                new SqlParameter("File_CVUngTuyen", model.File_CVUngTuyen),
                new SqlParameter("DN_TuChoi", model.DN_TuChoi));
            if (kq > 0)
            {
                return true;
            }
            return false;
        }
        public static bool Update_DoanhNghiep_UngTuyen(VLDB dbc, DoanhNghiep_UngTuyen model)
        {
            var kq = dbc.Database.ExecuteSqlCommand("Exec Update_Doanhnghiep_ungtuyen_byNTV_khai @Id,@smsNTVtoDN,@DN_Daxem," +
                            "@DN_LayTTLienHe,@NTV_TrangThaiUngTuyen,@NgayUpdate,@NTV_LyDoHuy,@File_CVUngTuyen,@DN_TuChoi",
                            new SqlParameter("Id", model.Id),
                            new SqlParameter("smsNTVtoDN", model.smsNTVtoDN),
                            new SqlParameter("DN_Daxem", model.DN_Daxem),
                            new SqlParameter("DN_LayTTLienHe", model.DN_LayTTLienHe),
                            new SqlParameter("NTV_TrangThaiUngTuyen", model.NTV_TrangThaiUngTuyen),
                            new SqlParameter("NgayUpdate", model.NgayUpdate),
                            new SqlParameter("NTV_LyDoHuy", model.NTV_LyDoHuy),
                            new SqlParameter("File_CVUngTuyen", model.File_CVUngTuyen),
                            new SqlParameter("DN_TuChoi", model.DN_TuChoi));
            if (kq > 0)
            {
                return true;
            }
            return false;
        }
        //public static List<DoanhNghiep_UngTuyen> GetListUngTuyenByNTV(VLDB dbc, int KH_ID)
        //{
        //    var kq = dbc.Database.SqlQuery<DoanhNghiep_UngTuyen>("Exec Get_Doanhnghiep_ungtuyen_byNTV_khai @KH_ID", new SqlParameter("KH_ID", KH_ID)).ToList();
        //    return kq;
        //}
        public static List<DoanhNghiep_UngTuyen> GetListUngTuyenByDN(VLDB dbc, int DN_ID)
        {
            var kq = dbc.DoanhNghiep_UngTuyens.Where(kh => kh.DN_TuChoi != "Từ chối" && kh.NTV_TrangThaiUngTuyen ==true &&
                dbc.DoanhNghiep_TuyenDung.FirstOrDefault(a => a.TuyenDung_ID == kh.TuyenDung_ID && a.DN_ID==DN_ID) != null
                && dbc.DoanhNghieps.FirstOrDefault(a=>a.DN_ID==DN_ID)!=null
                && dbc.KhachHangs.FirstOrDefault(a=>a.KH_ID ==kh.KH_ID)!=null)
                .OrderByDescending(kh=>kh.NgayUngTuyen).ToList();
            return kq;
        }
        public static List<DoanhNghiep_UngTuyen> GetListUngTuyenByNTV(VLDB dbc, int KH_ID)
        {
            var kq = dbc.DoanhNghiep_UngTuyens.Where(kh => kh.NTV_TrangThaiUngTuyen == true &&
                dbc.DoanhNghiep_TuyenDung.FirstOrDefault(a => a.TuyenDung_ID == kh.TuyenDung_ID && a.NgayHetHan > DateTime.Now)!= null
                && kh.KH_ID == KH_ID).OrderByDescending(kh => kh.NgayUngTuyen).ToList();
            return kq;
        }
    }
}