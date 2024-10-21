using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebViecLammoi.DAO;
using WebViecLammoi.Filters;
using WebViecLammoi.Models;
using WebViecLammoi.Utils;

namespace WebViecLammoi.Controllers
{
    public class JobController : Controller
    {
        VLDB dbc = new VLDB();
        int TT_Tinh = int.Parse(XString.TinhId);
        // GET: Job
        public ActionResult MainJob(int key, string searchNN = "")
        {
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            List<DoanhNghiep_TuyenDung> model = new List<DoanhNghiep_TuyenDung>();
            if ((bool)Session["Pay"] == true)
            {
                DN_HoSoTuyenDung_Dao.model_DNTD_Job_Pay = DN_HoSoTuyenDung_Dao.LinQ_Job_left_Pay(dbc);
                model = DN_HoSoTuyenDung_Dao.model_DNTD_Job_Pay;
            }
            else
            {
                DN_HoSoTuyenDung_Dao.model_DNTD_Job = DN_HoSoTuyenDung_Dao.LinQ_Job_left(dbc);
                model = DN_HoSoTuyenDung_Dao.model_DNTD_Job;
            }
            //Truy van hang da xem
            var cookies = Request.Cookies["TD_DaXem"];
            if (cookies == null)
            {
                cookies = new HttpCookie("TD_DaXem");
            }
            var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            //key =>keyNghanhNghe =>tổng nghành nghề
            ViewBag.duoi10tr = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("duoi10tr", (bool)Session["Pay"], 0,Ids).Count();
            ViewBag.den20tr = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("10den20tr", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.hon20tr = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("hon20tr", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.thoathuan = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("thoathuan", (bool)Session["Pay"], 0, Ids).Count();

            ViewBag.Noibat = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("noibat", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.toanthoigian = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("toanthoigian", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.banthoigian = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("banthoigian", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.giohanhchinh = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("giohanhchinh", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.theoca = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("theoca", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.khac = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("khac", (bool)Session["Pay"], 0, Ids).Count();
            ViewBag.TDDaXem = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job("TDDaXem", (bool)Session["Pay"], 0, Ids).Count();

            ViewBag.QC_QLTuyenDung = new DAO.New_Dao().Get_NewQCslideisActive(4173, 0, 10);

            var tong = new DAO.New_Dao().Get_NewQCslideisActive(4173, 0, 10).Count();
            if (tong < 9)
            {
                ViewBag.QC_TuyenDungTT = 2;
            }
            else
            {
                ViewBag.QC_TuyenDungTT = 0;
            }
            //ViewBag.QC_TuyenDungTieuBieu = new DAO.New_Dao().Get_NewQCslideisActive(4167, 0, 8);

            ViewBag.DMtg = dbc.DM_DiaChi.Where(kh => kh.ParentId == TT_Tinh).ToList();
            if(searchNN != "")
            {
                var ii = int.Parse(searchNN);
                var mapaotu = dbc.aspnet_mapautos.Find(ii);

                Session["ViewNghanhNghe"]=mapaotu !=null? mapaotu.Keystr:"";
            }
            
            Session["keyNghanhNghe"] = key;
            Session["SearchNghanhNghe"] = searchNN;
            Session["requestUri"] = "/Tuyen-Dung/" + key + "/" + searchNN; //"/Job/MainJob?key=" +key.ToString();
            return View(model);
        }
        public ActionResult JobList(int pageNo = 0, int pageSize = 10, string str = "", int id = 0, int keyNghanhNghe = 0, string searchNN = "")
        {
            if (keyNghanhNghe > 0)
            {
                ViewBag.Job_list = new DN_HoSoTuyenDung_Dao().GetListTDbyNghanhNghe(pageNo, (bool)Session["Pay"], pageSize, searchNN);
            }
            else
            {
                //Truy van hang da xem
                var cookies = Request.Cookies["TD_DaXem"];
                if (cookies == null)
                {
                    cookies = new HttpCookie("TD_DaXem");
                }
                var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();
                ViewBag.Job_list = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job_Skip(pageNo, (bool)Session["Pay"], pageSize, str, id,Ids);
            }

            return PartialView();
        }
        public ActionResult Job_timkiem(string str = "", string str_timkiem = "", int id = 0)
        {
            //Truy van hang da xem
            var cookies = Request.Cookies["TD_DaXem"];
            if (cookies == null)
            {
                cookies = new HttpCookie("TD_DaXem");
            }
            var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            //str_timkiem != "", thì vào action này và không phân trang
            ViewBag.Job_timkiem = new DN_HoSoTuyenDung_Dao().LinQ_DN_TD_Job(str, (bool)Session["Pay"], id,Ids)
                                    .Where(kh => kh.TieuDeTuyenDung.ToLower().Contains(str_timkiem.ToLower()))
                                    .Skip(0)
                                    .Take(30)
                                    .ToList();
            return PartialView();
        }
        //public ActionResult Job_TDbyNghanhNghe_PC(int pageNo = 0, int pageSize = 10, string search = "")
        //{
        //    //Index
        //    ViewBag.Job_TDbyNghanhNghe = new DN_HoSoTuyenDung_Dao().GetListTDbyNghanhNghe(pageNo, pageSize, search);
        //    return PartialView();
        //}
        public ActionResult Job_Quantam(int pageNo = 0, int pageSize_ph = 4)
        {
            if (Session["quyen"].ToString() == "NTV")
            {
                //Truy van hang da xem
                var cookies = Request.Cookies["TD_DaXem"];
                if (cookies == null)
                {
                    cookies = new HttpCookie("TD_DaXem");
                }
                var Ids = cookies.Values.AllKeys.Select(k => int.Parse(k)).ToList();

                Session["P_daxem"] = pageNo;
                var model = DAO.DN_HoSoTuyenDung_Dao.GetListTD_DaXem_Skip((bool)Session["Pay"], int.Parse(Session["P_daxem"].ToString()), Ids, pageSize_ph);
                if (model.Count >= 5)
                {
                    return PartialView(model);
                }
                else { return PartialView(); }
            }
            else { return PartialView(); }
        }
        //public ActionResult Job_XemNhieu()
        //{
        //    if (Session["quyen"] == "NTV")
        //    {
        //        var model = DAO.DN_HoSoTuyenDung_Dao.GetListTD_XemNhieu(dbc);

        //        return PartialView("Job_XemNhieu", model);
        //    }

        //    return PartialView();
        //}

        //Cho phép admin khác DuyetHuy trước thời hạn.
        //392583:Motcua
        public ActionResult DisJob(int Id)
        {
            if (int.Parse(Session["UsrID"].ToString()) == 392583)
            {
                var uID = Session["UsrID"];
                var model_uid = dbc.UserWebs.Find(392583);
                var model = dbc.DoanhNghiep_TuyenDung.Find(Id);
                model.TinhTrangHoSo = 4;
                model.NguoiCapNhat = 392583;
                model.NgayCapNhat = DateTime.Now;
                dbc.SaveChanges();
                bool nhatky = DAO.NhatKy_Admin_DAO.InsertNhatKy_Admin(dbc, 392583, model_uid.UserRoles_NVLoaitaikhoan
                            , model_uid.UserName, DateTime.Now, "DisJob (DuyetHuy): " + model.TieuDeTuyenDung, "autu");
            }
            return RedirectToAction("MainJob", "Job");
        }

        public ActionResult JobSave(int Id, int DN_ID, string TenHoSo)
        {
            PHU_HSTD_Luu item = new PHU_HSTD_Luu();
            var uID = Session["UsrID"];
            item.UserId = int.Parse(uID.ToString());
            item.HSTD_Id = Id;
            item.DN_Id = DN_ID;
            item.NgayTao = DateTime.Today;
            item.TenHoSo = TenHoSo;
            dbc.PHU_HSTD_Luu.Add(item);
            int kt = dbc.SaveChanges();

            if (kt > 0)
                TempData["testmsg"] = " Luu Thanh Cong !!! ";
            else
                TempData["testmsg"] = " Lưu Lỗi !!! ";
            return RedirectToAction("JobDetail", "Job", new { Id = Id });
        }
        [ProtectNTV]
        public ActionResult UngTuyen(int idtuyendung, string smsNTVtoDN)
        {
            DoanhNghiep_UngTuyen item = new DoanhNghiep_UngTuyen();
            item.Id = Guid.NewGuid();
            item.TuyenDung_ID = idtuyendung;
            var uID = int.Parse(Session["UsrID"].ToString());
            var KHID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
            item.KH_ID = KHID.UserChild_id;
            item.smsNTVtoDN = smsNTVtoDN;
            item.DN_Daxem = false;
            item.DN_LayTTLienHe = false;
            item.NgayUngTuyen = DateTime.Now;
            item.NTV_TrangThaiUngTuyen = true;
            item.NgayUpdate = DateTime.Now;
            item.NTV_LyDoHuy = "No";
            var file = Request.Files["File_CVUngTuyen"];

            if (file.ContentLength > 0)
            {
                var ten = file.FileName;
                var ext = ten.Substring(ten.LastIndexOf('.'));
                ten = Guid.NewGuid() + ext;
                item.File_CVUngTuyen = ten;

                file.SaveAs(XString.maplocal + "Document\\" + ten);
            }
            else
            {
                item.File_CVUngTuyen = "";
            }
            item.DN_TuChoi = "";
            var kq = DAO.DN_UngTuyen_Dao.Insert_DoanhNghiep_UngTuyen(dbc, item);

            if (kq)
            {
                TempData["testmsg"] = " Ứng tuyển Thanh Cong !!! ";
                //tro lai trang truoc do 
                var requestUri = Session["requestUri"] as string;
                if (requestUri != null)
                {
                    return Redirect(requestUri);
                }
            }
            else TempData["testmsg"] = " Có Lỗi !!! ";
            return RedirectToAction("JobDetail", "Job", new { Id = idtuyendung });
        }
        public ActionResult JobDetail(int Id)
        {
            var model = dbc.DoanhNghiep_TuyenDung.Find(Id);
            if (model != null)
            {
                New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
                Session["Pay"] = New_Dao.Pay_Sys.Pay;
                /////
                Session["requestUri"] = "/Job/JobDetail/" + Id;
                var tenHSTDDX = "HSTDDX" + Id.ToString();
                if (Session[tenHSTDDX] == null)
                {
                    if (model.SoLuotXem == null)
                    {
                        model.SoLuotXem = 1;
                    }
                    else
                    {
                        model.SoLuotXem++;
                    }
                    dbc.Entry(model).State = EntityState.Modified;
                    dbc.SaveChanges();
                    Session[tenHSTDDX] = "daxem";
                }
                ViewBag.bycty = DAO.DN_HoSoTuyenDung_Dao.GetList_bycty((bool)Session["Pay"], model.DN_ID, Id);
                //ghi nhan hang da xem
                var cookies = Request.Cookies["TD_DaXem"];
                if (cookies == null)
                {
                    cookies = new HttpCookie("TD_DaXem");
                }
                cookies.Values[Id.ToString()] = Id.ToString();
                //ghi nhan hang da xem
                if (Session["UsrID"] != null)
                {
                    int uID = int.Parse(Session["UsrID"].ToString());
                    if (Session["quyen"].ToString() == "NTV")
                    {
                        var KH_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                        ViewBag.HSNTVungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByNTV(dbc, KH_ID.UserChild_id)
                                                    .Where(kh => kh.TuyenDung_ID == Id).Count();
                        ViewBag.hsluu = dbc.PHU_HSTD_Luu.Where(kh => kh.UserId == uID && kh.HSTD_Id == Id).Count();
                    }
                }
                Response.Cookies.Add(cookies);
                return View(model);
            }
            //tro lai trang truoc do 
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            return Redirect("/Job/MainJob");
        }
        [ProtectTD]
        public ActionResult JobCreate()
        {
            var sss = Session["User"];
            if (sss != null)
            {
                var UsID = dbc.UserWebs.Where(kh => kh.UserName == sss.ToString() && kh.UserRoles_NVLoaitaikhoan == 4).Single();
                var DNID = dbc.DoanhNghieps.Where(kh => kh.DN_ID == UsID.UserChild_id).Single();
                ViewBag.noinhanhoso = DNID.DiaChi;
            }
            else { ViewBag.noinhanhoso = ""; }
            ViewBag.ChucDanh_ID = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh");
            ViewBag.ThoiGianLamViec_ID = new SelectList(dbc.DM_ThoiGianLamViec.OrderByDescending(kh => kh.ThoiGianLamViec_ID), "ThoiGianLamViec_ID", "TenThoiGianLamViec");
            ViewBag.YeuCauTrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList(), "TrinhDoChuyenMon_ID", "TenChuyenMon");
            ViewBag.YeuCauNganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong");
            ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NoiLamViec_TinhID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == 0).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi", TT_Tinh);
            ViewBag.NoiLamViec_HuyenID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == TT_Tinh).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi");
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            return View("JobCreate");
        }
        [HttpPost]
        [ValidateInput(false)]//13/02/2019
        public ActionResult JobCreate(DoanhNghiep_TuyenDung model, int NghiepVu1, int NghiepVu11, int NghiepVu2, int NghiepVu22, int NghiepVu3)
        {
            if (ModelState.IsValid)
            {
                var sss = Session["User"];
                if (sss != null)
                {
                    var UsID = dbc.UserWebs.Where(kh => kh.UserName == sss.ToString() && kh.UserRoles_NVLoaitaikhoan == 4).Single();
                    var DNID = dbc.DoanhNghieps.Where(kh => kh.DN_ID == UsID.UserChild_id).Single();
                    model.DN_ID = DNID.DN_ID;
                    string tieude = model.TieuDeTuyenDung;
                    model.TieuDeTuyenDung = tieude;
                    model.SoLuotXem = 0;
                    model.TinhTrangHoSo = 1;//Mới tạo;
                    if ((bool)Session["Pay"] == true)
                    {
                        model.KichHoat = false;
                    }
                    else
                    {
                        model.KichHoat = true;//chế độ Free
                    }
                    model.NoiBat = false;
                    model.NguoiTao = DNID.DN_ID;
                    model.LoaiViecLamTrong_ID = 2;
                    model.ChiNhanh_ID = 0;
                    model.NgayTao = DateTime.Now;
                    model.NguoiCapNhat = DNID.DN_ID;
                    model.NgayCapNhat = DateTime.Now;
                    model.HienThiWeb = true;
                    string M = model.MoTaCongViec;
                    string Q = model.QuyenLoi;
                    model.MoTaCongViec = M != null ? M : "";
                    model.QuyenLoi = Q != null ? Q : "";
                    dbc.DoanhNghiep_TuyenDung.Add(model);
                    int Kt = dbc.SaveChanges();
                    if (Kt > 0)
                    {
                        var Tin_NN_NV = new TD_NghiepVu_TinHoc_NgoaiNgu_Dao().Xoa_InsertTuyenDung_NN_TH_NV(
                        NghiepVu1, NghiepVu11, NghiepVu2, NghiepVu22, NghiepVu3, model.TuyenDung_ID, UsID.UserID);
                        Session["ThongBao_DN_TD"] = "Tạo mới thành công hồ sơ: " + tieude;
                    }
                    return Redirect("/Account/_MainJob");
                }
                else
                {
                    Session["requestUri"] = "/Job/JobCreate";
                    return RedirectToAction("_Login", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("", "Có lỗi DL");
                ViewBag.ChucDanh_ID = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh");
                ViewBag.ThoiGianLamViec_ID = new SelectList(dbc.DM_ThoiGianLamViec.OrderByDescending(kh => kh.ThoiGianLamViec_ID), "ThoiGianLamViec_ID", "TenThoiGianLamViec");
                ViewBag.YeuCauTrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList(), "TrinhDoChuyenMon_ID", "TenChuyenMon");
                ViewBag.YeuCauNganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong");
                ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                ViewBag.NoiLamViec_TinhID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == 0).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi", TT_Tinh);
                ViewBag.NoiLamViec_HuyenID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == 0).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi");
                DoanhNghiep_TuyenDung model1 = new DoanhNghiep_TuyenDung();
                return View("JobCreate", model1);
            }
        }
        public ActionResult YeuCauNghe_ID_kh(int MaNghanh)
        {
            var YeuCauNghe_ID = dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == MaNghanh)
                            .Select(kh => new { NgheLaoDong_ID = kh.NgheLaoDong_ID, TenNgheLaoDong = kh.TenNgheLaoDong });

            return Json(YeuCauNghe_ID, JsonRequestBehavior.AllowGet);
        }
        [ProtectTD]
        public ActionResult SMStoDN()
        {
            if (Session["UsrID"] != null)
            {

                int uID = int.Parse(Session["UsrID"].ToString());
                var DN_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);
                if (DN_ID != null)
                {
                    ViewBag.SMS_toDN = dbc.SMS_AdmintoDNs.Where(kh => kh.DNID == DN_ID.UserChild_id)
                            .OrderByDescending(kh => kh.Ngay)
                            .Take(20)
                            .ToList();
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("/Dang-Nhap");
                }
            }
            else
            {
                return RedirectToAction("/Dang-Nhap");
            }
        }
        public ActionResult thaydoitrangthai_daxem(string id)
        {
            var timviec = dbc.SMS_AdmintoDNs.Find(new Guid(id));
            if (timviec.DaXem == false)
            {
                var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[SMS_admintoDN] set DaXem=@daxem where Id=@Id",
                new SqlParameter("@daxem", true),
                new SqlParameter("@Id", new Guid(id)));
            }
            return Json("oooook");
        }
    }
}