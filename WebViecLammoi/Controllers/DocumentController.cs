using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebViecLammoi.Models;

namespace WebViecLammoi.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        VLDB dbc = new VLDB();
        public ActionResult MainDocument()
        {
            //1910:Phiếu Đăng Ký Tham Gia Sàn
            var model = dbc.VBPQ_TaiLieus.Where(t => t.PortalId == 81 && t.LoaiTaiLieuId == 1910)
                                            .Take(10)
                                         .OrderByDescending(t => t.Id)
                                         .ToList();
            Session["TenLoaiTaiLieu"] = dbc.VBPQ_LoaiTaiLieus.FirstOrDefault(p => p.PortalId == 81 && p.Id == 1910).TenLoaiTaiLieu;
            return View(model);
        }
        public ActionResult DocumentCategory()
        {
            var model = dbc.VBPQ_LoaiTaiLieus.Where(p => p.PortalId == 81)
                                            .OrderByDescending(p => p.Id)
                                            .ToList();
            return PartialView(model);
        }
        public ActionResult DocumentByCategory(int Id)
        {
            var model = dbc.VBPQ_TaiLieus.Where(t => t.LoaiTaiLieuId == Id && t.PortalId == 81)
                                         .OrderByDescending(t => t.Id)
                                         .ToList();
            Session["TenLoaiTaiLieu"] = dbc.VBPQ_LoaiTaiLieus.FirstOrDefault(p => p.PortalId == 81 && p.Id == Id).TenLoaiTaiLieu;
            return View("MainDocument", model);
        }
    }
}