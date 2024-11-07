using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebViecLammoi.Models.Model_VLBN;

using WebViecLammoi.Models.Model_Cty;
using WebViecLammoi.Models;

namespace WebViecLammoi.DAO
{
    public partial class VieclamKhuVuc
    {
        VLCty VLCty = null;
        VLBN VLBN = null;
        public VieclamKhuVuc()
        {
            VLCty = new VLCty();
            VLBN = new VLBN();
        }
        public List<Models.Model_Cty.News> GetLoGobyDB_Cty()
        {

            var model = VLCty.News.Where(kh => kh.CategoryId == 4176 && kh.isActive == true)
                .OrderByDescending(kh => kh.NewId)
                .Take(1)
                .ToList();
            if (model != null)
            {
                return model.ToList();
            }
            return null;
        }
        public List<Models.Model_VLBN.News> GetLoGobyDB_VLBN()
        {
            var model = VLBN.News.Where(kh => kh.CategoryId == 4176 && kh.isActive == true)
                .OrderByDescending(kh => kh.NewId)
                .Take(1)
                .ToList();
            if (model != null)
            {
                return model.ToList();
            }
            return null;
        }
        public static List<Models.Model_Cty.DoanhNghiep_TuyenDung> LinQ_DN_TDCty(VLCty db)
        {
            var model = db.Database.SqlQuery<Models.Model_Cty.DoanhNghiep_TuyenDung>("exec GetBase_DN_TD_khai").ToList();
            return model;
        }
        public static List<Models.Model_VLBN.DoanhNghiep_TuyenDung> LinQ_DN_TDVLBN(VLBN db)
        {
            var model = db.Database.SqlQuery<Models.Model_VLBN.DoanhNghiep_TuyenDung>("exec GetBase_DN_TD_khai").ToList();
            return model;
        }
        public static List<Models.Model_Cty.DoanhNghiep_TuyenDung> model_ListTDCty = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
        public static List<Models.Model_Cty.DoanhNghiep_TuyenDung> GetListTDCty_moinhat(int skip, int take)
        {
            var mode = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
            var model_List = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
            model_List = model_ListTDCty;
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
        public static List<Models.Model_VLBN.DoanhNghiep_TuyenDung> model_ListTDVLBN = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
        public static List<Models.Model_VLBN.DoanhNghiep_TuyenDung> GetListTDVLBN_moinhat(int skip, int take)
        {
            var mode = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
            var model_List = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
            model_List = model_ListTDVLBN;
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
        ////
        public Models.Model_Cty.DM_DiaChi GetDiaChiCtyById(int Id)
        {
            var mode = VLCty.DM_DiaChis.Find(Id);
            return mode;
        }
        public Models.Model_VLBN.DM_DiaChi GetDiaChiVLBNById(int Id)
        {
            var mode = VLBN.DM_DiaChis.Find(Id);
            return mode;
        }
        public Models.Model_Cty.DoanhNghiep GetDNCty_ByDNID(int DN_ID)
        {
            var mode = VLCty.DoanhNghieps.Find(DN_ID);
            return mode;
        }
        public Models.Model_VLBN.DoanhNghiep GetDNVLBN_ByDNID(int DN_ID)
        {
            var mode = VLBN.DoanhNghieps.Find(DN_ID);
            return mode;
        }
    }
}