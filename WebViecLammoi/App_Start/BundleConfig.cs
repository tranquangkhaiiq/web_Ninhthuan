using System.Web;
using System.Web.Optimization;

namespace WebViecLammoi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/js/jquery.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/AdminBSB-js").Include(
                     "~/Content/AdminBSB-cssjs/plugins/bootstrap/js/bootstrap.min.js",
                     "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/AdminBSB-css").Include(
                      "~/Content/AdminBSB-cssjs/plugins/bootstrap/css/bootstrap.min.css"
                       ));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          //"~/Scripts/bootstrap.js",
            //          "~/Content/Tien/bootstrap-3.3.7-dist/js/bootstrap.min.js",
            //          //"~/Scripts/respond.js",
            //          "~/Content/js/jquery.lazy.min.js"));
            //bundles.Add(new StyleBundle("~/Content/css/MainLayout").Include(
            //            "~/Content/bootstrap.css",
            //                "~/Content/css/bundlesa3c7.css",
            //            "~/Content/css/Tab_Style.css",
            //            "~/Content/css/TD_LD_Style.css",

            //            "~/Content/NhutCC/cssUpdate1707.css",
            //          "~/Content/fontawesome-free-5.9.0-web/css/fontawesome.min.css",
            //          "~/Content/Tien/SliderNumber/css/number_slideshow.css",
            //          //QuangCaoVD
            //          "~/Content/NhutCC/bootstrap-3.3.7-dist/Video_css_Nh.css",
            //          "~/Content/NhutCC/owl.carousel.min.css",
            //          "~/Content/NhutCC/owl.theme.default.css",
            //          //QuangCao2
            //          "~/Content/Tien/bootstrap-3.3.7-dist/css/Style1.css",
            //          "~/Content/Tien/bootstrap-3.3.7-dist/css/owl.theme.css",
            //          "~/Content/Tien/bootstrap-3.3.7-dist/css/owl.carousel.css",
            //          "~/Content/css/Login_Style.css",
            //          //Summernote
            //          "~/Content/css/summernote.css"

            //          ));
            //bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
            //     "~/Areas/Admin/Content/js/summernote.min.js",
            //      "~/Areas/Admin/Content/js/summernote-vi-VN.js"
            //    ));
            //đóng gói
            BundleTable.EnableOptimizations = true;
        }
    }
}
