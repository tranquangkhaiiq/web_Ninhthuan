using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class NTV_KhachHang_Dao
    {
        VLDB dbc = new VLDB();
        
        public KhachHang GetNTV_ByKHID(int KH_ID)
        {
            var mode = dbc.KhachHangs.Find(KH_ID);
             
            return mode;
        }
        public static bool Update_NTV112024(VLDB dbc, KhachHang model)
        {
            var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.KhachHang set CMND=@CMND,HoTen=@HoTen,NgaySinh=@NgaySinh," +
                "GioiTinh=@GioiTinh,NgayCap=@NgayCap,NoiCap_ID=@NoiCap_ID,DienThoai=@DienThoai,TamTru_Tinh_ID=@TamTru_Tinh_ID," +
                "TamTru_Huyen_ID=@TamTru_Huyen_ID,TamTru_Xa_ID=@TamTru_Xa_ID,TamTru_DiaChi=@TamTru_DiaChi," +
                "TinhTrangHonNhan=@TinhTrangHonNhan,HocVan_ID=@HocVan_ID,NgayCapNhat=@NgayCapNhat,SoLanCapNhat=@SoLanCapNhat " +
                "where KH_ID=@KH_ID",
                new SqlParameter("@CMND", model.CMND),
                new SqlParameter("@HoTen", model.HoTen),
                new SqlParameter("@NgaySinh", model.NgaySinh),
                new SqlParameter("@GioiTinh", model.GioiTinh),
                new SqlParameter("@NgayCap", model.NgayCap),
                new SqlParameter("@NoiCap_ID", model.NoiCap_ID),
                new SqlParameter("@DienThoai", model.DienThoai),
                new SqlParameter("@TamTru_Tinh_ID", model.TamTru_Tinh_ID),
                new SqlParameter("@TamTru_Huyen_ID", model.TamTru_Huyen_ID),
                new SqlParameter("@TamTru_Xa_ID", model.TamTru_Xa_ID),
                new SqlParameter("@TamTru_DiaChi", model.TamTru_DiaChi),
                //new SqlParameter("@Hinh", model.Hinh),
                new SqlParameter("@TinhTrangHonNhan", model.TinhTrangHonNhan),
                new SqlParameter("@HocVan_ID", model.HocVan_ID),
                new SqlParameter("@NgayCapNhat", model.NgayCapNhat),
                new SqlParameter("@SoLanCapNhat", model.SoLanCapNhat),
                new SqlParameter("@KH_ID", model.KH_ID));
            if (update > 0)
            {
                return true;
            }
            return false;
        }
    }
}