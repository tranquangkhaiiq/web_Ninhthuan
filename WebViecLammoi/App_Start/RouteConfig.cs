using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebViecLammoi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "TrangChu",
               url: "Trang-Chu",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "DangNhap",
               url: "Dang-Nhap",
               defaults: new { controller = "Account", action = "_Login", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );

            routes.MapRoute(
               name: "DangKy",
               url: "Dang-Ky",
               defaults: new { controller = "Account", action = "_Register", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "DoiPass",
               url: "Doi-Pass",
               defaults: new { controller = "Account", action = "_ChangePass", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "QuanLyTaiKhoanDN",
               url: "Quan-Ly-Tai-Khoan-DN",
               defaults: new { controller = "Account", action = "_MainJob", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "QuanLyTaiKhoanNTV",
               url: "Quan-Ly-Tai-Khoan-NTV",
               defaults: new { controller = "Account", action = "_MainCandidate", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "DoiThongTinDN",
               url: "Doi-Thong-Tin-DN",
               defaults: new { controller = "Account", action = "_EditJobAccount", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "DoiThongTinNTV",
               url: "Doi-Thong-Tin-NTV",
               defaults: new { controller = "Account", action = "_EditCandidateAccount", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
             name: "UngVien",
             url: "Ung-Vien",
             defaults: new { controller = "Candidate", action = "MainCandidate", id = UrlParameter.Optional },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );
            routes.MapRoute(
               name: "NguoiTimViec",
               url: "Nguoi-Tim-Viec/{metatitle}-{Id}",
               defaults: new { controller = "Candidate", action = "CandidateDetail", id = UrlParameter.Optional },
               namespaces: new string[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
               name: "TuyenDungChiTiet",
               url: "Tuyen-Dung/{metatitle}-{Id}",
               defaults: new { controller = "Job", action = "JobDetail", id = UrlParameter.Optional },
               namespaces: new string[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
             name: "TuyenDung",
             url: "Tuyen-Dung/{key}/{searchNN}",
             defaults: new { controller = "Job", action = "MainJob", key = "", searchNN = "" },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );
            
            routes.MapRoute(
             name: "GioiThieu",
             url: "Gioi-Thieu",
             defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );
            routes.MapRoute(
             name: "KhuVuc",
             url: "Khu-Vuc",
             defaults: new { controller = "Home", action = "ViecLamKhuVuc", id = UrlParameter.Optional },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );
            routes.MapRoute(
             name: "VanBan",
             url: "Van-Ban",
             defaults: new { controller = "Document", action = "MainDocument", id = UrlParameter.Optional },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );
            ////////////////////
            routes.MapRoute(
               name: "CTVanBan",
               url: "Van-Ban/{metatitle}-{Id}",
               defaults: new { controller = "Document", action = "DocumentByCategory", id = UrlParameter.Optional },
               namespaces: new[] { "WebViecLammoi.Controllers" }
           );
            routes.MapRoute(
             name: "ChiTietTinTuc",
             url: "Tin-Tuc/{metatitle}-{Id}",
             defaults: new { controller = "News", action = "NewsDetail", id = UrlParameter.Optional },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );
            routes.MapRoute(
             name: "TinTuc",
             url: "Tin-Tuc/{page}/{LoaiTinTuc_ID}",
             defaults: new { controller = "News", action = "MainNews", page = "", LoaiTinTuc_ID = "" },
             namespaces: new[] { "WebViecLammoi.Controllers" }
         );

            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}
