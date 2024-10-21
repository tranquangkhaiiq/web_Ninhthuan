
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public partial class DanhMuc_Dao
    {
        VLDB dbc = null;
        public DanhMuc_Dao()
        {
            dbc = new VLDB();
        }
        public DM_ChucDanh GetChucDanhbyID(int Id)
        {
            var model = dbc.DM_ChucDanh.Find(Id);
            if (model != null)
            {
                return model;
            }return null;
        }
        public DM_TrinhDoChuyenMon GetChuyenMonbyID(int Id)
        {
            var model = dbc.DM_TrinhDoChuyenMon.Find(Id);
            if (model != null)
            {
                return model;
            }
            return null;
        }
        public DM_NganhLaoDong GetNghanhNTVbyID(int Id)
        {
            var model = dbc.DM_NganhLaoDong.Find(Id);
            if (model != null)
            {
                return model;
            }
            return null;
        }
        public DM_NganhKinhDoanh GetNghanhKDbyID(int Id)
        {
            var model = dbc.DM_NganhKinhDoanh.Find(Id);
            if (model != null)
            {
                return model;
            }
            return null;
        }
        public DM_NgheLaoDong GetNgheNTVbyID(int Id)
        {
            var model = dbc.DM_NgheLaoDong.Find(Id);
            if (model != null)
            {
                return model;
            }
            return null;
        }
        public DM_NgheKinhDoanh GetNgheKDbyID(int Id)
        {
            var model = dbc.DM_NgheKinhDoanh.Find(Id);
            if (model != null)
            {
                return model;
            }
            return null;
        }
        public DM_ThoiGianLamViec GetTGbyID(int Id)
        {
            var model = dbc.DM_ThoiGianLamViec.Find(Id);
            
            return model;
        }
        public List<KhachHang_KinhNghiem_LamViec_2022> GetListKNbyKHID(int Id)
        {
            var model = dbc.KhachHang_KinhNghiem_LamViec_2022s.Where(kh=>kh.KH_ID==Id).ToList();
            return model;
        }
    }
}