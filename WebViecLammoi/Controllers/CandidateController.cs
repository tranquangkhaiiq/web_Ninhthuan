using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebViecLammoi.Filters;
using WebViecLammoi.Models;
using WebViecLammoi.Utils;
using WebViecLammoi.DAO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace WebViecLammoi.Controllers
{
    public class CandidateController : Controller
    {
        VLDB dbc = new VLDB();
        int TT_Tinh = int.Parse(XString.TinhId);
        // GET: Candidate
        public ActionResult MainCandidate()
        {
            List<KhachHang_TimViecLam> model = new List<KhachHang_TimViecLam>();
            NTV_HoSoXinViec_Dao.model_kh_tvl = NTV_HoSoXinViec_Dao.LinQ_TimViec_left(dbc);
            model = NTV_HoSoXinViec_Dao.model_kh_tvl;
            //Truy van hang da xem
            var cookies = Request.Cookies["NTVDaXem"];
            if (cookies == null)
            {
                cookies = new HttpCookie("NTVDaXem");
            }
            var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            //new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("", 0);
            ViewBag.duoi10tr = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("duoi10tr", 0,Ids).Count();
            ViewBag.den20tr = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("10den20tr", 0, Ids).Count();
            ViewBag.hon20tr = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("hon20tr", 0, Ids).Count();
            ViewBag.thoathuan = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("thoathuan", 0, Ids).Count();

            ViewBag.xemnhieu = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("xemnhieu", 0, Ids).Count();
            ViewBag.toanthoigian = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("toanthoigian", 0, Ids).Count();
            ViewBag.banthoigian = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("banthoigian", 0, Ids).Count();
            ViewBag.giohanhchinh = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("giohanhchinh", 0, Ids).Count();
            ViewBag.theoca = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("theoca", 0, Ids).Count();
            ViewBag.khac = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("khac", 0, Ids).Count();
            ViewBag.NTVDaXem = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main("NTVDaXem", 0, Ids).Count();

            ViewBag.DMtg_ntv = dbc.DM_DiaChi.Where(kh => kh.ParentId == TT_Tinh).ToList();
            ViewBag.QC_QLTimViec = new DAO.New_Dao().Get_NewQCslideisActive(4164, 0, 10);
            var tong = new DAO.New_Dao().Get_NewQCslideisActive(4164, 0, 10).Count();
            if (tong < 9)
            {
                ViewBag.QC_TimViecTT = 2;
            }
            else
            {
                ViewBag.QC_TimViecTT = 0;
            }
            //ViewBag.QC_TuyenDungTieuBieu = new DAO.New_Dao().Get_NewQCslideisActive(4167, 0, 8);
            Session["requestUri"] = "/Candidate/MainCandidate";
            return View(model);
        }
        public ActionResult CandidateList(int pageNo = 0, int pageSize = 10, string str = "", int id = 0)
        {
            //Truy van hang da xem
            var cookies = Request.Cookies["NTVDaXem"];
            if (cookies == null)
            {
                cookies = new HttpCookie("NTVDaXem");
            }
            var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            ViewBag.TimViec_list = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main_Skip(pageNo, pageSize, str, id,Ids);
            return PartialView();
        }
        public ActionResult TangluottaiCV(int Id)
        {
            //Dùng TyLeHoSoHoanThanh làm số lượt tải
            if (Session["UsrID"] != null)
            {
                var uID= Session["UsrID"].ToString();
                if (Session["quyen"].ToString() == "TD")
                {
                    var tenDNtaicv = "DN" + uID;
                    if (Session[tenDNtaicv] == null)
                    {
                        var model = dbc.KhachHang_TimViecLam.Find(Id);
                        if (model.TyLeHoSoHoanThanh == null)
                        {
                            model.TyLeHoSoHoanThanh = 1;
                        }
                        else
                        {
                            model.TyLeHoSoHoanThanh ++;
                        }
                        dbc.Entry(model).State = EntityState.Modified;
                        dbc.SaveChanges();
                        Session[tenDNtaicv] = "dataiCV";
                    }
                }
            }
            
            return Json(JsonRequestBehavior.AllowGet);
        }
        public ActionResult Candidate_timkiem(string str = "", string str_timkiem = "", int id = 0)
        {
            //Truy van hang da xem
            var cookies = Request.Cookies["NTVDaXem"];
            if (cookies == null)
            {
                cookies = new HttpCookie("NTVDaXem");
            }
            var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            //str_timkiem != "", thì vào action này và không phân trang
            ViewBag.Candidate_timkiem = new NTV_HoSoXinViec_Dao().LinQ_NTV_Main(str, id,Ids)
                                    .Where(kh => kh.TenHoSo.ToLower().Contains(str_timkiem.ToLower()))
                                    .Skip(0)
                                    .Take(30)
                                    .ToList();
            return PartialView();
        }

        [ProtectUser]
        public ActionResult Candidate_XemNhieu()
        {
            if (Session["quyen"].ToString() == "TD")
            {
                int uID = int.Parse(Session["UsrID"].ToString());

                var model = DAO.NTV_HoSoXinViec_Dao.GetListNTV_XemNhieu(dbc);

                return PartialView("Candidate_XemNhieu", model);
            }

            return PartialView();
        }

        public ActionResult CandidateDetail(int Id)
        {
            Session["requestUri"] = "/Candidate/CandidateDetail/" + Id;
            var model = dbc.KhachHang_TimViecLam.Find(Id);
            if (model != null)
            {
                var tenHSNTVDX = "NTVDX" + Id.ToString();
                if (Session[tenHSNTVDX] == null)
                {
                    if (model.LuotXem == null)
                    {
                        model.LuotXem =1;
                    }
                    else
                    {
                        model.LuotXem++;
                    }
                    dbc.Entry(model).State = EntityState.Modified;
                    dbc.SaveChanges();
                    Session[tenHSNTVDX] = "daxem";
                }
                ViewBag.HSkhac = DAO.NTV_HoSoXinViec_Dao.GetList_HSNTV_Cty(dbc, Id, model.KH_ID);
                if (Session["UsrID"] != null)
                {
                    int uID = int.Parse(Session["UsrID"].ToString());
                    if (Session["quyen"].ToString() == "TD")
                    {
                        var TD_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);
                        ViewBag.HSNTVungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByNTV(dbc, TD_ID.UserChild_id)
                                                    .Where(kh => kh.TuyenDung_ID == Id).Count();
                        ViewBag.hsluu = dbc.PHU_HSTV_Luu.Where(kh => kh.UserId == uID && kh.HSTV_Id == Id).Count();
                    }
                }
                //ghi nhan hang da xem
                var cookies = Request.Cookies["NTVDaXem"];
                if (cookies == null)
                {
                    cookies = new HttpCookie("NTVDaXem");
                }
                cookies.Values[Id.ToString()] = Id.ToString();
                Response.Cookies.Add(cookies);
                return PartialView(model);
            }
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return Redirect("/Candidate/MainCandidate");
            
        }
        public ActionResult CandidateSave(int Id, int NTV_ID, string TenHoSo)
        {

            PHU_HSTV_Luu item = new PHU_HSTV_Luu();
            var uID = Session["UsrID"];
            item.UserId = int.Parse(uID.ToString());
            item.HSTV_Id = Id;
            item.NTV_Id = NTV_ID;
            item.NgayTao = DateTime.Today;
            item.TenHoSo = TenHoSo;
            dbc.PHU_HSTV_Luu.Add(item);
            int kt = dbc.SaveChanges();
            if (kt > 0)
                TempData["testmsg"] = " Luu Thanh Cong !!! ";
            else
                TempData["testmsg"] = " Lưu Lỗi !!! ";
            return RedirectToAction("CandidateDetail", "Candidate", new { Id = Id });
        }
        public ActionResult NgheMongMuon_ID(int NganhMongMuon_ID)
        {
            var NgheMongMuon_ID = dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == NganhMongMuon_ID)
                           .Select(kh => new { NgheLaoDong_ID = kh.NgheLaoDong_ID, TenNgheLaoDong = kh.TenNgheLaoDong });
            //var NgheMongMuon_ID = dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == NganhMongMuon_ID)
            //               .Select(kh => new { NgheKD_ID = kh.NgheKD_ID, TenNgheKD = kh.TenNgheKD });
            return Json(NgheMongMuon_ID, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NgheMongMuon_edit(int NganhMongMuon_ID, int nghemm)
        {
            var NgheMongMuon_ID = dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == NganhMongMuon_ID && kh.NgheLaoDong_ID != nghemm)
                           .Select(kh => new { NgheLaoDong_ID = kh.NgheLaoDong_ID, TenNgheLaoDong = kh.TenNgheLaoDong });
            //var NgheMongMuon_ID = dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == NganhMongMuon_ID)
            //               .Select(kh => new { NgheKD_ID = kh.NgheKD_ID, TenNgheKD = kh.TenNgheKD });
            return Json(NgheMongMuon_ID, JsonRequestBehavior.AllowGet);
        }
        [ProtectNTV]
        public ActionResult CandidateCreate()
        {
            //ViewBag.NganhKD_ID = new SelectList(dbc.DM_NganhKinhDoanh.ToList().OrderBy(nh => nh.Nganh_ID), "Nganh_ID", "TenNganhKD");
            ViewBag.ChucDanhMongMuon = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh");
            ViewBag.ThoiGianLamViecMongMuon = new SelectList(dbc.DM_ThoiGianLamViec.ToList(), "ThoiGianLamViec_ID", "TenThoiGianLamViec");
            ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhLaoDong.ToList().OrderBy(nh => nh.NganhLaoDong_ID), "NganhLaoDong_ID", "TenNganhLaoDong");
            ViewBag.NoiLamViecMongMuon_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh), "Id", "TenDiaChi");
            ViewBag.LoaiHinhDNMongMuon_ID = new SelectList(dbc.DM_LoaiHinhDoanhNghiep.ToList(), "LoaiHinhDoanhNghiep_ID", "TenLoaiHinhDoanhNghiep");
            return View("CandidateCreate");
        }
        [HttpPost]
        [ProtectNTV]
        [ValidateInput(false)]
        public ActionResult CandidateCreate(KhachHang_TimViecLam tvl, int dicongtac,int NgheMongMuon_2ID, int NgheMongMuon_3ID)
        {
            if (ModelState.IsValid)
            {
                var sss = Session["User"];
                if (sss != null)
                {
                    try
                    {
                        var UsID = dbc.UserWebs.Where(kh => kh.UserName == sss.ToString()).FirstOrDefault();
                        KhachHang_TimViecLam model = new KhachHang_TimViecLam();

                        model = tvl;
                        model.TenHoSo = tvl.TenHoSo;
                        model.CoTheDiCongTac = dicongtac == 0 ? false : true;
                        model.KH_ID = UsID.UserChild_id;
                        model.NgayTao = DateTime.Now;
                        model.NgayCapNhat = DateTime.Now;
                        model.TinhTrangPheDuyetHoSo_ID = 2;
                        model.LoaiTimViecLam_ID = 2;
                        model.HienThiTrenWeb = true;
                        model.NguoiTao = UsID.UserChild_id;
                        model.KichHoat = true;
                        model.ChiNhanh_ID = 0;
                        model.NgayDangKy = DateTime.Now;
                        model.NgayCapNhat = DateTime.Now;
                        model.LuotXem = 0;
                        model.MucLuongCu = 0;
                        model.NguoiCapNhat = UsID.UserChild_id;
                        model.NgayHoSoHetHan = model.NgayTao.AddMonths(1);
                        model.NganhNghe34_2022 = NgheMongMuon_2ID.ToString() +","+ NgheMongMuon_3ID.ToString();
                        var file = Request.Files["CVdinhkem"];
                        if (file.ContentLength > 0)
                        {
                            var ten = file.FileName;
                            var ext = ten.Substring(ten.LastIndexOf('.'));
                            ten = Guid.NewGuid() + ext;
                            model.KhaNangNoiTroi = ten;
                            file.SaveAs(XString.maplocal + "Document\\" + ten);
                        }
                        else
                        {
                            model.KhaNangNoiTroi = "";
                        }

                        dbc.KhachHang_TimViecLam.Add(model);
                        int kt = dbc.SaveChanges();
                        if (kt > 0)
                        {
                            Session["ThongBao_DN_TD"] = "Insert thành công hồ sơ: " + tvl.TenHoSo;
                            return Redirect("/Account/_MainCandidate");
                        }
                        else { ModelState.AddModelError("", "Có lỗi xãy ra!!"); }
                    }
                    catch
                    {
                        Session["ThongBao_DN_TD"] = "Có Lỗi thêm mới hồ sơ: " + tvl.TenHoSo;
                        return Redirect("/Account/_MainCandidate");
                    }
                    

                    return PartialView("CandidateCreate");
                }
                else
                {
                    Session["requestUri"] = "/Candidate/CandidateCreate";
                    return RedirectToAction("_Login", "Account");
                }
            }
            else
            {
                ViewBag.ChucDanhMongMuon = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh");
                ViewBag.ThoiGianLamViecMongMuon = new SelectList(dbc.DM_ThoiGianLamViec.ToList(), "ThoiGianLamViec_ID", "TenThoiGianLamViec");
                //ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhKinhDoanh.ToList().OrderBy(nh => nh.Nganh_ID), "Nganh_ID", "TenNganhKD");
                ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhLaoDong.ToList().OrderBy(nh => nh.NganhLaoDong_ID), "NganhLaoDong_ID", "TenNganhLaoDong");
                ViewBag.NoiLamViecMongMuon_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh).ToList(), "Id", "TenDiaChi");
                ViewBag.LoaiHinhDNMongMuon_ID = new SelectList(dbc.DM_LoaiHinhDoanhNghiep.ToList(), "LoaiHinhDoanhNghiep_ID", "TenLoaiHinhDoanhNghiep");
                KhachHang_TimViecLam model1 = new KhachHang_TimViecLam();
                return View("CandidateCreate", model1);
            }
        }
        [ProtectNTV]
        public ActionResult _CreateCV()
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var KH_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                if (KH_ID != null)
                {
                    var model = dbc.KhachHangs.Find(KH_ID.UserChild_id);
                    return PartialView(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return PartialView("_Login");
        }
        [HttpPost]
        [ValidateInput(false)]
        public EmptyResult Export(string GridHtml)
        {
            var html = XString.EditStringCV(GridHtml);
            string htmlcv = html;

            string _fileCSS = Server.MapPath("~/Content/CVTimViec.css");
            string _strCSS = System.IO.File.ReadAllText(_fileCSS);
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<html " +
             " xmlns:o='urn:schemas-microsoft-com:office:office' " +
             " xmlns:w='urn:schemas-microsoft-com:office:word'" +
              " xmlns='http://www.w3.org/TR/REC-html40'>" +
              "<head><title>Invoice Sample</title>");
            strBody.Append("<xml>" +
            "<w:WordDocument>" +
            " <w:View>Print</w:View>" +
            " <w:Zoom>50</w:Zoom>" +
            " <w:DoNotOptimizeForBrowser/>" +
            " </w:WordDocument>" +
            " </xml>");

            strBody.Append("<style>" + _strCSS + "</style></head>");
            //strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" + "<div class=Section1>");
            strBody.Append("<body><div class='page-settings'>" + htmlcv + "</div></body></html>");
            //strBody.Append("</div></body></html>");
            Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml");
            Response.AppendHeader("Content-disposition", "attachment;filename=myword.doc");
            Response.Write(strBody.ToString());

            return new EmptyResult();
        }
    }
}