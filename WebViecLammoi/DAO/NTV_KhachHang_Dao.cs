using System;
using System.Collections.Generic;
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
    }
}