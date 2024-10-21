using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class TD_NghiepVu_TinHoc_NgoaiNgu_Dao
    {
        VLDB dbc = new VLDB();
        
        public List<DoanhNghiep_TuyenDung_NghiepVu> NghiepVu_TuyenDung(int TuyenDung_ID)
        {
            var model = dbc.DoanhNghiep_TuyenDung_NghiepVu.Where(kh => kh.TuyenDung_ID == TuyenDung_ID).ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung_NgoaiNgu> NghiepVu_Ngoaingu(int TuyenDung_ID)
        {
            var model = dbc.DoanhNghiep_TuyenDung_NgoaiNgu.Where(kh => kh.TuyenDung_ID == TuyenDung_ID).ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung_TinHoc> NghiepVu_TinHoc(int TuyenDung_ID)
        {
            var model = dbc.DoanhNghiep_TuyenDung_TinHoc.Where(kh => kh.TuyenDung_ID == TuyenDung_ID).ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung_TinHoc> NghiepVu_TinHocbyId(int TuyenDung_ID, int Id)
        {
            var model = dbc.DoanhNghiep_TuyenDung_TinHoc.Where(kh => kh.TuyenDung_ID == TuyenDung_ID && kh.NghiepVu_TinHoc_ID==Id).ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung_NgoaiNgu> NghiepVu_NgoaingubyId(int TuyenDung_ID,int Id)
        {
            var model = dbc.DoanhNghiep_TuyenDung_NgoaiNgu.Where(kh => kh.TuyenDung_ID == TuyenDung_ID && kh.NghiepVu_NgoaiNgu_ID==Id).ToList();
            return model;
        }
        public List<DoanhNghiep_TuyenDung_NghiepVu> NghiepVu_TuyenDungById(int TuyenDung_ID, int Id)
        {
            var model = dbc.DoanhNghiep_TuyenDung_NghiepVu.Where(kh => kh.TuyenDung_ID == TuyenDung_ID && kh.NghiepVu_ID==Id).ToList();
            return model;
        }
        private bool InsertTinhoc(int tuyendungid, int tinhoc, int UserID)
        {
            try
            {
                DoanhNghiep_TuyenDung_TinHoc dn = new DoanhNghiep_TuyenDung_TinHoc();
                dn.TuyenDung_ID = tuyendungid;
                dn.NghiepVu_TinHoc_ID = tinhoc;
                dn.KhaNangSuDung = "";
                dn.GhiChu = "";
                dn.NgayTao = DateTime.Now;
                dn.NguoiTao = UserID;
                dbc.DoanhNghiep_TuyenDung_TinHoc.Add(dn);
                dbc.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
        private bool InsertNgoaiNgu(int tuyendungid, int ngoaingu, int UserID)
        {
            try
            {
                DoanhNghiep_TuyenDung_NgoaiNgu nn = new DoanhNghiep_TuyenDung_NgoaiNgu();
                nn.TuyenDung_ID = tuyendungid;
                nn.NghiepVu_NgoaiNgu_ID = ngoaingu;
                nn.ChungChiNgoaiNgu = "";
                nn.KhaNangSuDung = "";
                nn.GhiChu = "";
                nn.NgayTao = DateTime.Now;
                nn.NguoiTao = UserID;
                dbc.DoanhNghiep_TuyenDung_NgoaiNgu.Add(nn);
                dbc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool Insertnghiepvu(int tuyendungid, int nghiepvu, int UserID)
        {
            try
            {
                DoanhNghiep_TuyenDung_NghiepVu nv = new DoanhNghiep_TuyenDung_NghiepVu();
                nv.TuyenDung_ID = tuyendungid;
                nv.NghiepVu_ID = nghiepvu;
                nv.MoTaTrinhDoNghiepVu = "";
                nv.GhiChu = "";
                nv.NgayTao = DateTime.Now;
                nv.NguoiTao = UserID;
                dbc.DoanhNghiep_TuyenDung_NghiepVu.Add(nv);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Xoa_InsertTuyenDung_NN_TH_NV(int NghiepVu1, int NghiepVu11, int NghiepVu2, int NghiepVu22, int NghiepVu3,int TuyenDung_ID,int UserID)
        {
            var XoaNLD = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung_NghiepVu] where TuyenDung_ID=" + TuyenDung_ID);
            var XoaNLD2 = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung_NgoaiNgu] where TuyenDung_ID=" + TuyenDung_ID);
            var XoaNLD3 = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung_TinHoc] where TuyenDung_ID=" + TuyenDung_ID);
            if (NghiepVu1 != 0)
            {
                bool kt1 = InsertTinhoc(TuyenDung_ID, NghiepVu1,UserID);
            }
            if (NghiepVu11 != 0)
            {
                bool kt11 = InsertTinhoc(TuyenDung_ID, NghiepVu11, UserID);
            }
            if (NghiepVu2 != 0)
            {
                bool kt2 = InsertNgoaiNgu(TuyenDung_ID, NghiepVu2, UserID);
            }
            if (NghiepVu22 != 0)
            {
                bool kt22 = InsertNgoaiNgu(TuyenDung_ID, NghiepVu22,UserID);
            }
            if (NghiepVu3 != 0)
            {
                bool kt3 = Insertnghiepvu(TuyenDung_ID, NghiepVu3,UserID);
            }
            return true;
        }
    }
}