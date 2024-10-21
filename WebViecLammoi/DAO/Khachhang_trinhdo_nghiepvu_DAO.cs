using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class Khachhang_trinhdo_nghiepvu_DAO
    {
        VLDB dbc = null;
        public Khachhang_trinhdo_nghiepvu_DAO()
        {
            dbc = new VLDB();
        }
        public static List<KhachHang_TrinhDo> Getlist_khachhangtrinhdo_Single(VLDB db, int KH_id)
        {
            var model = db.KhachHang_TrinhDo.Where(kh => kh.KH_ID == KH_id && db.DM_TrinhDoChuyenMon.FirstOrDefault(a => a.TrinhDoChuyenMon_ID == kh.TrinhDo_ID) != null
                                             && db.DM_NganhLaoDong.FirstOrDefault(a => a.NganhLaoDong_ID == kh.Nganh_ID) != null
                                             && db.DM_NgheLaoDong.FirstOrDefault(a => a.NgheLaoDong_ID == kh.Nghe_ID) != null)
                .Take(1)
                .ToList();
            return model;
        }
        public KhachHang_TrinhDo Getlist_khachhangtrinhdo(int KH_id)
        {
            var model = dbc.KhachHang_TrinhDo.FirstOrDefault(kh => kh.KH_ID == KH_id && dbc.DM_TrinhDoChuyenMon.FirstOrDefault(a => a.TrinhDoChuyenMon_ID == kh.TrinhDo_ID) != null
                                             && dbc.DM_NganhLaoDong.FirstOrDefault(a => a.NganhLaoDong_ID == kh.Nganh_ID) != null
                                             && dbc.DM_NgheLaoDong.FirstOrDefault(a => a.NgheLaoDong_ID == kh.Nghe_ID) != null);
                
            return model;
        }
        public List<KhachHang_NghiepVu> Getlist_khachhangNghiepVu(int KH_id)
        {
            var model = dbc.KhachHang_NghiepVu.Where(kh => kh.KH_ID == KH_id && dbc.DM_NghiepVu.FirstOrDefault(a => a.NghiepVu_ID == kh.NghiepVu_ID) != null)
                .ToList();
            return model;
        }
        public List<KhachHang_TimViec_TinHoc>GetList_KH_tinhoc(int KH_id)
        {
            var model = dbc.KhachHang_TimViec_TinHocs.Where(kh => kh.KH_ID == KH_id && 
                dbc.DM_NghiepVu.FirstOrDefault(a => a.NghiepVu_ID == kh.NghiepVu_TinHoc_ID) != null);
            return model.ToList();
            //if(model != null) { return model.ToList(); }else { return null; }
        }
        public List<KhachHang_TimViec_NgoaiNgu_2022> GetList_KH_NgoaiNgu(int KH_id)
        {
            var model = dbc.KhachHang_TimViec_NgoaiNgu_2022s.Where(kh => kh.KH_ID == KH_id &&
                dbc.DM_NghiepVu.FirstOrDefault(a => a.NghiepVu_ID == kh.NghiepVu_NgoaiNgu_ID) != null);
            return model.ToList();
            //if (model != null) { return model.ToList(); } else { return null; }
        }
        public static string GetName_TV(VLDB dbc, string ten, int KHID)
        {
            string name = "";
            if (ten == "HoTen")
            {
                name = dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == KHID).HoTen;
            }
            else if (ten == "GioiTinh")
            {
                name = dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == KHID).GioiTinh.ToString();
            }
            else if (ten == "Hinh")
            {
                name = dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == KHID).Hinh;
            }
            else if (ten == "TenDiaChi")
            {
                var DM = dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == KHID).TamTru_Huyen_ID;
                if (DM != null && DM>0)
                {
                    var dc = dbc.DM_DiaChi.FirstOrDefault(p => p.Id == DM);
                    if (dc != null)
                    {
                        name = dc.TenDiaChi;
                    }
                }
            }
            else if (ten == "NgaySinh")
            {
                name = dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == KHID).NgaySinh.ToString();
            }
            else if (ten == "TenNgheKD")
            {
                var DMN = dbc.KhachHang_TimViecLam.FirstOrDefault(kh => kh.KH_ID == KHID).NgheMongMuon_ID;
                var TDMN = dbc.DM_NgheKinhDoanh.FirstOrDefault(p => p.NgheKD_ID == DMN);
                if (TDMN != null)
                {
                    name = TDMN.TenNgheKD;
                }
            }else if (ten == "TenChucDanh")
            {
                var chucdanhid = dbc.KhachHang_TimViecLam.FirstOrDefault(kh => kh.KH_ID == KHID).ChucDanhMongMuon;
                var chucdanh = dbc.DM_ChucDanh.FirstOrDefault(p => p.ChucDanh_ID == chucdanhid);
                if(chucdanh != null)
                {
                    name = chucdanh.TenChucDanh;
                }
            }

            else if (ten == "TenChuyenMon")
            {
                var DMCM = dbc.KhachHang_TrinhDo.FirstOrDefault(kh => kh.KH_ID == KHID);
                if (DMCM != null)
                {
                    var TDMCM = dbc.DM_TrinhDoChuyenMon.FirstOrDefault(kh => kh.TrinhDoChuyenMon_ID == DMCM.TrinhDo_ID);
                    if (TDMCM != null)
                    {
                        name = TDMCM.TenChuyenMon;
                    }
                }
            }
            return name;
        }
        private bool InsertTinhoc(int khachhangid, int tinhoc, int UserID)
        {
            try
            {
                KhachHang_TimViec_TinHoc kh = new KhachHang_TimViec_TinHoc();
                kh.KH_ID = khachhangid;
                kh.NghiepVu_TinHoc_ID = tinhoc;
                kh.KhaNangSuDung = "";
                kh.GhiChu = "";
                kh.NgayTao = DateTime.Now;
                kh.NguoiTao = UserID;
                dbc.KhachHang_TimViec_TinHocs.Add(kh);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool InsertNgoaiNgu(int khachhangid, int ngoaingu, int UserID)
        {
            try
            {
                KhachHang_TimViec_NgoaiNgu_2022 nn = new KhachHang_TimViec_NgoaiNgu_2022();
                nn.KH_ID = khachhangid;
                nn.NghiepVu_NgoaiNgu_ID = ngoaingu;
                nn.ChungChiNgoaiNgu = "";
                nn.KhaNangSuDung = "";
                nn.GhiChu = "";
                nn.NgayTao = DateTime.Now;
                nn.NguoiTao = UserID;
                dbc.KhachHang_TimViec_NgoaiNgu_2022s.Add(nn);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool Insertnghiepvu(int khachhangid, int nghiepvu, int UserID)
        {
            try
            {
                KhachHang_NghiepVu nv = new KhachHang_NghiepVu();
                nv.KH_ID = khachhangid;
                nv.NghiepVu_ID = nghiepvu;
                nv.MoTaTrinhDoNghiepVu = "";
                nv.GhiChu = "";
                nv.NgayTao = DateTime.Now;
                nv.NguoiTao = UserID;
                dbc.KhachHang_NghiepVu.Add(nv);
                dbc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Xoa_InsertKhachHang_NN_TH_NV(int NghiepVu1, int NghiepVu11, int NghiepVu2, int NghiepVu22, int NghiepVu3, int khachhangid, int UserID)
        {
            var XoaNLD = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[KhachHang_NghiepVu] where KH_ID=" + khachhangid);
            var XoaNLD2 = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[KhachHang_TimViec_NgoaiNgu_2022] where KH_ID=" + khachhangid);
            var XoaNLD3 = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[KhachHang_TimViec_TinHoc] where KH_ID=" + khachhangid);
            if (NghiepVu1 != 0)
            {
                bool kt1 = InsertTinhoc(khachhangid, NghiepVu1, UserID);
            }
            if (NghiepVu11 != 0)
            {
                bool kt11 = InsertTinhoc(khachhangid, NghiepVu11, UserID);
            }
            if (NghiepVu2 != 0)
            {
                bool kt2 = InsertNgoaiNgu(khachhangid, NghiepVu2, UserID);
            }
            if (NghiepVu22 != 0)
            {
                bool kt22 = InsertNgoaiNgu(khachhangid, NghiepVu22, UserID);
            }
            if (NghiepVu3 != 0)
            {
                bool kt3 = Insertnghiepvu(khachhangid, NghiepVu3, UserID);
            }
            return true;
        }
        private bool Inserttrinhdo(int khachhangid, int trinhdo, int Nganh_ID,
             int Nghe_ID, int UserID)
        {
            try
            {
                KhachHang_TrinhDo KhTd = new KhachHang_TrinhDo();
                KhTd.KH_ID = khachhangid;
                KhTd.TrinhDo_ID = trinhdo;
                KhTd.Nganh_ID = Nganh_ID;
                KhTd.Nghe_ID = Nghe_ID;
                KhTd.NgayTao = DateTime.Now;
                KhTd.MoTa = "";
                KhTd.GhiChu = "";
                KhTd.NguoiTao = UserID.ToString();
                dbc.KhachHang_TrinhDo.Add(KhTd);
                int kt2 = dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Xoa_InsertKhachHang_trinhdo(int TrinhDo_ID, int Nganh_ID,int Nghe_ID ,int khachhangid, int UserID)
        {
            var check_khtd = dbc.KhachHang_TrinhDo.Where(p => p.KH_ID == khachhangid).ToList();
            if (check_khtd.Count > 0)
            {
                var XoaNLD_TrinhDo = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[KhachHang_TrinhDo] where KH_ID=" + khachhangid);

            }
            if (TrinhDo_ID != 0 || Nganh_ID != 0 || Nghe_ID != 0)
            {
                var kt2 = Inserttrinhdo(khachhangid, TrinhDo_ID, Nganh_ID, Nghe_ID, UserID);
            }
            return true;
        }
    }
}