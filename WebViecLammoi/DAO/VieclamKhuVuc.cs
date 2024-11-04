using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebViecLammoi.Models.Model_VLBN;

using WebViecLammoi.Models.Model_Cty;

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
    }
}