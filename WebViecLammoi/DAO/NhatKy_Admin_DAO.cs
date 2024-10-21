using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public class NhatKy_Admin_DAO
    {
        VLDB dbc = null;
        public NhatKy_Admin_DAO()
        {
            dbc = new VLDB();
        }
        public static bool InsertNhatKy_Admin(VLDB dbc, int UserID, int LoaiUser, string UserName, DateTime CreateDate, string CongViec, string GhiChu)
        {
            try
            {
                if (LoaiUser == 1 || LoaiUser == 5)
                {
                    NhatKy_Admin model = new NhatKy_Admin();
                    model.UserID = UserID;
                    model.UserName = UserName;
                    model.LoaiUser = LoaiUser;
                    model.CreateDate = CreateDate;
                    model.CongViec = CongViec;
                    model.GhiChu = GhiChu;
                    dbc.NhatKy_Admin.Add(model);
                    dbc.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                return false;
            }

        }
        public bool InsertUserWeb(string UserName,string EmailConnection, int DN_khachhangid, string PasswordSalt, string CirpherPass,int Loaitaikhoan)
        {
            UserWeb model2 = new UserWeb();

            model2.Password = CirpherPass;
            model2.PasswordSalt = PasswordSalt;
            model2.UserName = UserName;
            model2.EmailConnection = EmailConnection;

            model2.UserRoles_NVLoaitaikhoan = Loaitaikhoan;
            model2.CreateDate = DateTime.Now;
            model2.UserChild_id = DN_khachhangid;
            model2.FailedPasswordAttemptCount = 0;
            model2.IsLocked = false;
            model2.LastLockedChangedDate = DateTime.Now;
            model2.LastPasswordChangedDate = DateTime.Now;

            dbc.UserWebs.Add(model2);
            dbc.SaveChanges();
            return true;
        }
    }
}