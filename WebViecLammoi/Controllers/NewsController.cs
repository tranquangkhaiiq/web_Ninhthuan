using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebViecLammoi.DAO;
using WebViecLammoi.Models;

namespace WebViecLammoi.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        VLDB dbc = new VLDB();
        public ActionResult MainNews(int page, int LoaiTinTuc_ID)
        {
            //isActive = 1
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            var model = new NewsCategory();
            if (LoaiTinTuc_ID > 0)
            {
                model = dbc.NewsCategories.Find(LoaiTinTuc_ID);
                ViewBag.TTTTLD = new List<New_small>();
                if (model != null)
                {
                    ViewBag.TTTTLD = new DAO.New_Dao().Get_NewQCslideisActive(LoaiTinTuc_ID, page - 1, 10);
                    ViewBag.TTTTLD_count = new DAO.New_Dao().Get_NewQCslideisActive(LoaiTinTuc_ID, 0, 0).Count();
                    ViewBag.TTTTLD_page = page;
                }
                else
                {
                    ViewBag.TTTTLD_count = 0;
                    ViewBag.TTTTLD_page = 1;
                }
            }
            else
            {
                model = dbc.NewsCategories.Find(4166);
                //ViewBag.TTTTLD = new List<New_small>();
                ViewBag.TTTTLD = DAO.New_Dao.Get_QCslideisActive(dbc, page - 1, 10);
                ViewBag.TTTTLD_count = DAO.New_Dao.Get_QCslideisActive(dbc, page - 1, 10).Count();
                ViewBag.TTTTLD_page = page;
            }

            return View(model);
        }
        public ActionResult Panel_Right()
        {
            //CategoryId == 3303: Thông báo và kết quả Phiên giao dịch việc làm(chỉ lấy tin mới nhất)
            //CategoryId == 3302: Thông báo của trung tâm
            //CategoryId == 3680: Đào tạo, dạy nghề

            ViewBag.TTTTLD = new DAO.New_Dao().Get_NewQCslideisActive(3309, 0, 10);
            ViewBag.TTNoiBat10 = DAO.New_Dao.Get_NewQCslideNOisActive(dbc, 0, 10);
            return PartialView();
        }

        public ActionResult NewsCategory()
        {
            //Không hiển thị
            //3304:văn bản;             3310:TỔNG ĐÀI TƯ VẤN LAO ĐỘNG,VIỆC LÀM, CHÍNH SÁCH PHÁP LUẬT LAO ĐỘNG
            //3311:Cơ hội việc làm      3312:Tuyển sinh             3315:Giới thiệu trung tâm 
            //3323:Văn bản khác         3324:Bảo hiểm thất nghiệp   3326:Giáo dục đào tạo 
            //3344:Sàn giao dịch        3680:Đào tạo, dạy nghề      3681:Lao động, việc làm
            //3743: Xuất khẩu lao động

            return PartialView();
        }
        public ActionResult FindNews()
        {
            return PartialView();
        }

        public ActionResult LatestNews()
        {
            var model = dbc.News.Where(n => n.Status == 3 && n.PortalId == 81).OrderByDescending(n => n.NewId)
                                .Take(5)
                                .ToList();
            return PartialView(model);
        }
        public ActionResult NewsDetail(int id)
        {
            var model = dbc.News.Find(id);
            //tang view
            var newgues = "newgues" + id.ToString();
            if (Session[newgues] == null)
            {
                if(model.View == null)
                {
                    model.View = 1;
                }
                else
                {
                    model.View++;
                }
                
                dbc.SaveChanges();
                Session[newgues] = "daxemnews";
            }
            ViewBag.footee = new DAO.New_Dao().Get_NewQCslideisActive(model.CategoryId, 0, 5);
            return View(model);
        }
        public ActionResult GetList_Default(int PageNo = 0, int PageSize = 5)
        {
            ViewBag.Items = dbc.News.Where(c => c.Status == 3 && c.PortalId == 81)
                .OrderByDescending(c => c.NewId)
                .Skip(PageNo * PageSize)
                .Take(PageSize)
                .ToList();
            return PartialView("ListNews");
        }
        public ActionResult GetList_ByCategory(int Id, int PageNo = 0, int PageSize = 5)
        {
            ViewBag.Items = dbc.News.Where(n => n.CategoryId == Id && n.Status == 3 && n.PortalId == 81)
                .OrderByDescending(c => c.NewId)
                .Skip(PageNo * PageSize)
                .Take(PageSize)
                .ToList();
            return PartialView("ListNews");
        }
        public ActionResult GetList_Search(string Keyword, int PageNo = 0, int PageSize = 5)
        {
            ViewBag.Items = dbc.News.Where(p => p.Title.ToLower().Contains(Keyword.ToLower()) && p.Status == 3 && p.PortalId == 81)
                .OrderByDescending(c => c.NewId)
                .Skip(PageNo * PageSize)
                .Take(PageSize)
                .ToList();
            return PartialView("ListNews");
        }
        public ActionResult NewsByCategory(int Id)
        {
            Session["CatId"] = Id;
            Session["NewsType"] = "cat";
            return RedirectToAction("MainNews");
        }
        public ActionResult SearchNews(string KeywordsTT)
        {
            Session["KeywordsTT"] = KeywordsTT;
            Session["NewsType"] = "search";
            return RedirectToAction("MainNews");
        }
    }
}