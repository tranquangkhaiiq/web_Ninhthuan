using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebViecLammoi.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using WebViecLammoi.DAO;
using System.IO;
using System.Web.UI;
using System.Configuration;
using WebViecLammoi.Filters;
using WebViecLammoi.Utils;

namespace WebViecLammoi.Controllers
{
    public class HomeController : Controller
    {
        VLDB dbc = new VLDB();
        public ActionResult Index()
        {
            //CategoryId == 4168 : QuangCao_Small
            Session["requestUri"] = "/Home/Index";
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            New_Dao.model_NewsSlide = New_Dao.LinQ_NewsSlide(dbc);
            if ((bool)Session["Pay"]==true)
            {
                DN_HoSoTuyenDung_Dao.model_ListTD_Pay = DN_HoSoTuyenDung_Dao.LinQ_DN_TD_Pay(dbc);
            }
            else
            {
                DN_HoSoTuyenDung_Dao.model_ListTD = DN_HoSoTuyenDung_Dao.LinQ_DN_TD(dbc);
            }
            NTV_HoSoXinViec_Dao.model_ListNTV = NTV_HoSoXinViec_Dao.LinQ_HSTV(dbc);
            //ConfigurationManager.AppSettings["Type"]
            var tong = new DAO.New_Dao().Get_NewQCslideisActive(4166, 0, 4).Count();
            ViewBag.QC_TuyenDungPanel = new DAO.New_Dao().Get_NewQCslideisActive(4166, 0, 4);
            if (tong < 4)
            {
                ViewBag.QC_PanelTT = 4 - tong;
            }
            else
            {
                ViewBag.QC_PanelTT = 0;
            }
            ViewBag.QC_TuyenDungTieuBieu = new DAO.New_Dao().Get_NewQCslideisActive(4167, 0, 8);
            ViewBag.QC_NewLong = new DAO.New_Dao().Get_NewSTTOne(4170);
            return View();
        }
        public ActionResult mlienket()
        {
            ViewBag.Lienket1 = dbc.m_Lienkets.Where(kh=>kh.loaiId==1).ToList();
            ViewBag.Lienket2 = dbc.m_Lienkets.Where(kh => kh.loaiId == 2).ToList();
            ViewBag.Lienket3 = dbc.m_Lienkets.Where(kh => kh.loaiId == 3).ToList();
            return PartialView();
        }
        public ActionResult About()
        {
            ViewBag.GioiThieu = dbc.m_gioithieus.Where(d => d.id == 1).ToList();
            ViewBag.LichSu = dbc.m_gioithieus.Where(d => d.id == 2).ToList();
            ViewBag.CSVC = dbc.m_gioithieus.Where(d => d.id == 3).ToList();

            return View();
        }
        public ActionResult QLLoGo()
        {
            ViewBag.Logo = dbc.News.Where(kh => kh.CategoryId == 4176 && kh.isActive==true)
                .OrderByDescending(kh=>kh.NewId)
                .Take(1)
                .ToList();

            return PartialView();
        }
        public ActionResult QCslide_Mobile()
        {
            //CategoryId == 4169: QuangCaoTT_Slide

            //ưu tiên tin 1 mới nhất CategoryId == 3303: Thông báo và kết quả Phiên giao dịch việc làm
            //ưu tiên tin 2 CategoryId == 3680: Đào tạo, dạy nghề (2 tin)
            //ưu tiên tin 3 CategoryId == 3302: Thông báo của trung tâm(7 tin)

            ViewBag.TTNoiBat1 = DAO.New_Dao.Get_NewQCslideNOisActive(dbc, 0, 1);
            ViewBag.TTNoiBat5 = DAO.New_Dao.Get_NewQCslideNOisActive(dbc, 1, 5);
            return PartialView("_Slider_Mobile");
        }
        //ok
        public ActionResult QCslide_PC()
        {
            //ưu tiên tin 1 mới nhất CategoryId == 3303: Thông báo và kết quả Phiên giao dịch việc làm
            //ưu tiên tin 2 CategoryId == 3680: Đào tạo, dạy nghề (2 tin)
            //ưu tiên tin 3 CategoryId == 3302: Thông báo của trung tâm(7 tin)
            ViewBag.TTNoiBat1 = DAO.New_Dao.Get_NewQCslideNOisActive(dbc, 0, 1);
            ViewBag.TTNoiBat5 = DAO.New_Dao.Get_NewQCslideNOisActive(dbc, 1, 5);
            return PartialView("_Slider_PC");
        }
        public ActionResult QCTT_small()
        {
            var tong = new DAO.New_Dao().Get_NewQCslideisActive(4168, 0, 4).Count();
            ViewBag.QCTT_small = new DAO.New_Dao().Get_NewQCslideisActive(4168, 0, 4);
            if (tong < 4)
            {
                ViewBag.QCTT_smallTT = 4 - tong;
            }
            else
            {
                ViewBag.QCTT_smallTT = 0;
            }
            return PartialView();
        }
        public ActionResult HotJob_PC()
        {
            bool key = (bool)Session["Pay"];
            ViewBag.HotJob = DN_HoSoTuyenDung_Dao.GetList_DuyetHot_NICEAD_Home_Index(key, 1, 3);
            ViewBag.HotJob_Active = DN_HoSoTuyenDung_Dao.GetList_DuyetHot_NICEAD_Home_Index(key, 0, 1);
            return PartialView("HotJob_PC");
        }
        public ActionResult UngVienXemNhieu_PC()
        {
            ViewBag.CandidateXemNhieu = NTV_HoSoXinViec_Dao.GetListNTV_XemNhieu(dbc, 1, 3);
            ViewBag.CandidateXemNhieu_Active = NTV_HoSoXinViec_Dao.GetListNTV_XemNhieu(dbc, 0, 1);
            
            return PartialView("");
        }
        public ActionResult LastestJob_PC()
        {
            //Index
            ViewBag.LastestJob = DAO.DN_HoSoTuyenDung_Dao.GetListTD_moinhat((bool)Session["Pay"], 0, 10);
            return PartialView("_TD_MoiNhat");
        }
        public ActionResult TDbyNghanhNghe_PC()
        {
            //Index
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            if ((bool)Session["Pay"] == true)
            {
                DN_HoSoTuyenDung_Dao.model_DNTD_Job_Pay = DN_HoSoTuyenDung_Dao.LinQ_Job_left_Pay(dbc);
            }
            else
            {
                DN_HoSoTuyenDung_Dao.model_DNTD_Job = DN_HoSoTuyenDung_Dao.LinQ_Job_left(dbc);
            }
             ViewBag.strmapaotu = dbc.aspnet_mapautos.ToList();
            var model = new DN_HoSoTuyenDung_Dao().GetListTDbyNghanhNghe(0, (bool)Session["Pay"], 10, "");
            return PartialView("_TD_NganhNghe", model);
        }
        public ActionResult LastestCandidate_PC()
        {
            //Index
            ViewBag.LastestCandidate = DAO.NTV_HoSoXinViec_Dao.GetListNTV_moinhat(dbc, 0, 10);
            //var model = DAO.NTV_HoSoXinViec_Dao.GetListNTV_moinhat(dbc, 0, 10);

            return PartialView("_LD_MoiNhat");
        }

        public ActionResult LuxuryJob_PC()
        {
            //Index
            ViewBag.LuxuryJob = DAO.DN_HoSoTuyenDung_Dao.GetListTD_luongcao((bool)Session["Pay"], 0, 15);
            return PartialView("_TD_Luxury");
        }
        public ActionResult Slide_Right()
        {
            //Index
            //ViewBag.LuxuryJob = DAO.DN_HoSoTuyenDung_Dao.GetListTD_luongcao(dbc, 0, 10);
            if (Session["quyen"] != null && Session["quyen"].ToString() == "TD")
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var DN_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);
                if (DN_ID != null)
                {
                    var DNId = dbc.Database.SqlQuery<int>("select DN_ID from [VLDB].[dbo].[DoanhNghiep] where DN_ID=" + DN_ID.UserChild_id);
                    ViewBag.Tonghoso = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID(DNId.First());
                    ViewBag.TonghosoHuy = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID_tinhtrang(DNId.First(), 4);
                    ViewBag.TonghosoDuyet = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID_tinhtrang(DNId.First(), 3);
                    ViewBag.TonghosoChoDuyet = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID_tinhtrang(DNId.First(), 2);
                    ViewBag.TonghosoDuyet_hethan = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID_Duyet_hethan(DNId.First());
                    ViewBag.Tonghoso_NoWeb = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID_hienthiweb(DNId.First(), false);
                    ViewBag.Tonghoso_Online = new DAO.DN_HoSoTuyenDung_Dao().GetTuyenDungbyDNID_Duyet_conhan(DNId.First());
                    ViewBag.Tinchuaxem = new DAO.DN_HoSoTuyenDung_Dao().Tinchuaxem(DNId.First());
                    ViewBag.totalungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByDN(dbc, DN_ID.UserChild_id).Count();
                }
            }
            if (Session["quyen"] != null && Session["quyen"].ToString() == "NTV")
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var NTV_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                if (NTV_ID != null)
                {
                    var KHId = dbc.Database.SqlQuery<int>("select KH_ID from [VLDB].[dbo].[KhachHang] where KH_ID=" + NTV_ID.UserChild_id);
                    ViewBag.Tonghoso = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID(KHId.First());
                    ViewBag.TonghosoHuy = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID_tinhtrang(KHId.First(), 4);
                    ViewBag.TonghosoDuyet = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID_tinhtrang(KHId.First(), 3);
                    ViewBag.TonghosoChoDuyet = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID_tinhtrang(KHId.First(), 2);
                    ViewBag.TonghosoDuyet_hethan = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID_Duyet_hethan(KHId.First());
                    ViewBag.Tonghoso_NoWeb = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID_hienthiweb(KHId.First(), false);
                    ViewBag.Tonghoso_Online = new DAO.NTV_HoSoXinViec_Dao().GetTimViecbyKHID_Duyet_conhan(KHId.First());
                    ViewBag.Tonghoso_ungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByNTV(dbc, NTV_ID.UserChild_id).Count();
                }
            }
            ViewBag.QC_NewSlide = new DAO.New_Dao().Get_NewSTTOne(4169);
            ViewBag.QC_Right = new DAO.New_Dao().Get_NewQCslideisActive(4171, 0, 15);
            var tong = new DAO.New_Dao().Get_NewQCslideisActive(4171, 0, 15).Count();
            if (tong < 14)
            {
                ViewBag.QC_RightTT = 2;
            }
            else
            {
                ViewBag.QC_RightTT = 0;
            }
            //ViewBag.QC_TuyenDungTieuBieu = new DAO.New_Dao().Get_NewQCslideisActive(4167, 0, 8);
            return PartialView("");
        }
        [ProtectUser]
        public ActionResult GopY(string customer_name, string email, string phone, string msg_content)
        {
            if (Session["User"] != null)
            {
                string Conemail = ConfigurationManager.AppSettings["Email"];
                var kq = Mailer.Send(Conemail, "Mail liên hệ góp ý từ Web VL "+ XString.tinh, "Tôi tên " + customer_name + ", email: " + email + ", điện thoại: " + phone + "<br /><br />" + msg_content);
                if (kq)
                {
                    ModelState.AddModelError("", "Cám ơn bạn đã gửi thư góp ý.");
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi gửi mail góp ý!!!");
                }
                
            }
            else return RedirectToAction("_Login", "Account");
            //CategoryId == 4168 : QuangCao_Small
            Session["requestUri"] = "/Home/Index";
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            New_Dao.model_NewsSlide = New_Dao.LinQ_NewsSlide(dbc);
            if ((bool)Session["Pay"] == true)
            {
                DN_HoSoTuyenDung_Dao.model_ListTD_Pay = DN_HoSoTuyenDung_Dao.LinQ_DN_TD_Pay(dbc);
            }
            else
            {
                DN_HoSoTuyenDung_Dao.model_ListTD = DN_HoSoTuyenDung_Dao.LinQ_DN_TD(dbc);
            }
            NTV_HoSoXinViec_Dao.model_ListNTV = NTV_HoSoXinViec_Dao.LinQ_HSTV(dbc);
            //ConfigurationManager.AppSettings["Type"]
            var tong = new DAO.New_Dao().Get_NewQCslideisActive(4166, 0, 4).Count();
            ViewBag.QC_TuyenDungPanel = new DAO.New_Dao().Get_NewQCslideisActive(4166, 0, 4);
            if (tong < 4)
            {
                ViewBag.QC_PanelTT = 4 - tong;
            }
            else
            {
                ViewBag.QC_PanelTT = 0;
            }
            ViewBag.QC_TuyenDungTieuBieu = new DAO.New_Dao().Get_NewQCslideisActive(4167, 0, 8);
            ViewBag.QC_NewLong = new DAO.New_Dao().Get_NewSTTOne(4170);
            return View("Index");
        }
    }
}