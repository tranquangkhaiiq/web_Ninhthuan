using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebViecLammoi.Filters
{
    public class ProtectNTVAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var quyen = HttpContext.Current.Session["quyen"];
            if (quyen == null || quyen.ToString() != "NTV" || quyen.ToString() == "kh")
            {
                HttpContext.Current.Session["Message"] = "Vui lòng đăng nhập";

                HttpContext.Current.Response.Redirect("/Dang-Nhap");

                return;
            }
        }
    }
}