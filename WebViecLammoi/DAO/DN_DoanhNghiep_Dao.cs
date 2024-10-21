using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class DN_DoanhNghiep_Dao
    {
        VLDB dbc = null;
        public DN_DoanhNghiep_Dao(){
            dbc = new VLDB();
        }
        public DM_DiaChi GetDiaChiById(int Id)
        {
            var mode = dbc.DM_DiaChi.Find(Id);
            return mode;
        }
        public DM_LoaiHinhDoanhNghiep GetLoaiDNById(int Id)
        {
            var mode = dbc.DM_LoaiHinhDoanhNghiep.Find(Id);
            return mode;
        }
        public DM_KhuCongNghiep GetkhucnById(int Id)
        {
            var mode = dbc.DM_KhuCongNghiep.Find(Id);
            return mode;
        }
        public DoanhNghiep GetDN_ByDNID(int DN_ID)
        {
            var mode = dbc.DoanhNghieps.Find(DN_ID);
            return mode;
        }
        public List<DoanhNghiep> GetList_DN(int Sec, int pageSize)
        {
            var mode = dbc.DoanhNghieps.Where(kh => kh.TenDoanhNghiep != null
                && kh.Huyen_ID != null && dbc.DM_DiaChi.FirstOrDefault(p => p.Id == kh.Huyen_ID) != null
                && kh.KhuCongNghiep_ID != null && dbc.DM_KhuCongNghiep.FirstOrDefault(p => p.KhuCongNghiep_ID == kh.KhuCongNghiep_ID) != null)
                .OrderByDescending(kh => kh.DN_ID)
                .Skip(Sec * pageSize)
                .Take(pageSize)
                .ToList();
            return mode;
        }
        public static string GetName_DN(VLDB dbc, string ten, int DNID)
        {
            string name = "";
            if (ten == "TenDoanhNghiep")
            {
                name = dbc.DoanhNghieps.FirstOrDefault(kh => kh.DN_ID == DNID).TenDoanhNghiep;
            }
            else if (ten == "Huyen_ID")
            {
                name = dbc.DoanhNghieps.FirstOrDefault(kh => kh.DN_ID == DNID).Huyen_ID.ToString();
            }
            else if (ten == "Tinh_ID")
            {
                name = dbc.DoanhNghieps.FirstOrDefault(kh => kh.DN_ID == DNID).Tinh_ID.ToString();
            }
            else if (ten == "Logo")
            {
                name = dbc.DoanhNghieps.FirstOrDefault(kh => kh.DN_ID == DNID).Logo;
            }
            else if (ten == "TenDiaChi")
            {
                name = dbc.DoanhNghieps.FirstOrDefault(kh => kh.DN_ID == DNID).DM_DiaChi.TenDiaChi;
            }
            //else if(ten == "TenNgheLaoDong")
            //{
            //    name = dbc.DoanhNghiep_TuyenDung.FirstOrDefault(kh => kh.TuyenDung_ID == TDID).DM_NgheLaoDongs.TenNgheLaoDong;
            //}

            return name;
        }
        public int GetTotal_DN()
        {
            int mode = dbc.DoanhNghieps.Where(kh => kh.TenDoanhNghiep != null
                && kh.Huyen_ID != null && dbc.DM_DiaChi.FirstOrDefault(p => p.Id == kh.Huyen_ID) != null
                && kh.KhuCongNghiep_ID != null && dbc.DM_KhuCongNghiep.FirstOrDefault(p => p.KhuCongNghiep_ID == kh.KhuCongNghiep_ID) != null)
                .Count();
            return mode;
        }
        public List<DoanhNghiep> GetList_DNSearch(int Sec, int pageSize, string strTK)
        {
            var mode = dbc.DoanhNghieps
                .Where(n => n.TenDoanhNghiep != null
                && n.Huyen_ID != null && dbc.DM_DiaChi.FirstOrDefault(p => p.Id == n.Huyen_ID) != null
                && n.KhuCongNghiep_ID != null && dbc.DM_KhuCongNghiep.FirstOrDefault(p => p.KhuCongNghiep_ID == n.KhuCongNghiep_ID) != null &&
                (n.TenDoanhNghiep.Contains(strTK) || n.DienThoai.Contains(strTK) || n.Email.Contains(strTK)))
                .OrderByDescending(n => n.DN_ID)
                .Skip(Sec * pageSize)
                .Take(pageSize)
                .ToList();
            return mode;
        }
        public int GetTotal_DNSearch(string strTK)
        {
            int mode = 0;
            mode = dbc.DoanhNghieps.Where(n => n.TenDoanhNghiep != null
                && n.Huyen_ID != null && dbc.DM_DiaChi.FirstOrDefault(p => p.Id == n.Huyen_ID) != null
                && n.KhuCongNghiep_ID != null && dbc.DM_KhuCongNghiep.FirstOrDefault(p => p.KhuCongNghiep_ID == n.KhuCongNghiep_ID) != null &&
                (n.TenDoanhNghiep.Contains(strTK) || n.DienThoai.Contains(strTK) || n.Email.Contains(strTK)))
                .Count();
            return mode;
        }
    }
}