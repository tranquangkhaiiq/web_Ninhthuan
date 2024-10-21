using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebViecLammoi.Models;
namespace WebViecLammoi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        VLDB dbc = new VLDB();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var Max = dbc.aspnet_getVisitors.OrderByDescending(kh => kh.Id)
                                            .Take(1)
                                            .Single();

            Application["VisiYesterday"] = 0;
            Application["Visitoday"] = 0;
            Application["Visitors"] = Max.TongLuotTruyCap;
            Application["TotalOnlineUsers"] = 0;

            Application["Maunen"] = "";
            //UserLogin dang test 22/03/2024
            Application["UserVisitor"] = 0;
        }
        protected void Application_BeginRequest()
        {
            var Lang = "vi";
            if (Request["Lang"] != null)
            {
                Lang = Request["Lang"];
            }
            var Culture = new CultureInfo(Lang);
            Thread.CurrentThread.CurrentCulture = Culture;
            Thread.CurrentThread.CurrentUICulture = Culture;
            
        }
        protected void Application_End()
        {
            //lay so dem trong applicayion
            var text = Application["Visitors"].ToString();

            var path = Server.MapPath("~/App_Data/SoTruyCap.txt");
            File.WriteAllText(path, text);
        }
        protected void Session_Start()
        {
            Application.Lock();
            var visitors = (int)Application["Visitors"];

            Application["Visitors"] = visitors + 1;
            Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] + 1;
            Application.UnLock();

            var text = Application["Visitors"].ToString();
            var path = Server.MapPath("~/App_Data/SoTruyCap.txt");
            File.WriteAllText(path, text);
            //luu so luot truy cap
            aspnet_getVisitors sum = new aspnet_getVisitors();
            var Max = dbc.aspnet_getVisitors.OrderByDescending(kh => kh.Id)
                                            .Take(1)
                                            .Single();
            var Maxold = dbc.aspnet_getVisitors.OrderByDescending(kh => kh.Id)
                                            .Skip(1)
                                            .Take(1)
                                            .Single();
            var MaxoldPre = dbc.aspnet_getVisitors.OrderByDescending(kh => kh.Id)
                                            .Skip(2)
                                            .Take(1)
                                            .Single();
            Application["VisiYesterday"] = Maxold.TongLuotTruyCap - MaxoldPre.TongLuotTruyCap;
            Application["Visitoday"] = Max.TongLuotTruyCap - Maxold.TongLuotTruyCap;
            if (Max.Ngay.HasValue)
            {
                if (int.Parse(text) > Max.TongLuotTruyCap)
                {
                    var dayyy = Max.Ngay.Value.ToShortDateString();
                    if (Max.Ngay.Value.ToShortDateString() != DateTime.Now.ToShortDateString())
                    {
                        sum.TongLuotTruyCap = int.Parse(text);

                        sum.Online = int.Parse(Application["TotalOnlineUsers"].ToString());
                        sum.Ghichu = "Global";
                        sum.Ngay = DateTime.Now;
                        dbc.aspnet_getVisitors.Add(sum);
                        dbc.SaveChanges();
                    }
                    else
                    {
                        var ar = dbc.aspnet_getVisitors.Find(Max.Id);
                        ar.TongLuotTruyCap = int.Parse(text);
                        if ((int)Application["TotalOnlineUsers"] > ar.Online)
                        {
                            ar.Online = (int)Application["TotalOnlineUsers"];
                            ar.Ngay = DateTime.Now;
                        }
                        dbc.Entry(ar).State = EntityState.Modified;
                        dbc.SaveChanges();
                    }
                }
            }
        }
        void Session_End(object sender, EventArgs e)
        {
            //Xảy ra khi phiên làm việc không có gởi yêu cầu hoặc làm tươi trang aspx của ứng dụng web trong một khoảng thời gian (mặc định là 20 phút)
            Application.Lock();
            Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] - 1;
            Application.UnLock();
        }
    }
}
