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
        public static Models.Model_Cty.Pay_Sys Pay_Sys_Cty = new Models.Model_Cty.Pay_Sys();
        public static Models.Model_VLBN.Pay_Sys Pay_Sys_VLBN = new Models.Model_VLBN.Pay_Sys();
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
        public static List<Models.Model_Cty.DoanhNghiep_TuyenDung> model_ListTDCty_Pay = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
        public static List<Models.Model_Cty.DoanhNghiep_TuyenDung> model_ListTDCty = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
        public static List<Models.Model_Cty.DoanhNghiep_TuyenDung> GetListTDCty_moinhat(bool key, int skip, int take)
        {
            var mode = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
            var model_List = new List<Models.Model_Cty.DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTDCty_Pay : model_ListTDCty;
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
        public static List<Models.Model_VLBN.DoanhNghiep_TuyenDung> model_ListTDVLBN_Pay = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
        public static List<Models.Model_VLBN.DoanhNghiep_TuyenDung> model_ListTDVLBN = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
        public static List<Models.Model_VLBN.DoanhNghiep_TuyenDung> GetListTDVLBN_moinhat(bool key, int skip, int take)
        {
            var mode = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
            var model_List = new List<Models.Model_VLBN.DoanhNghiep_TuyenDung>();
            model_List = (key == true) ? model_ListTDVLBN_Pay : model_ListTDVLBN;
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
    }
}