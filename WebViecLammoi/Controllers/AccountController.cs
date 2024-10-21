using MaHoa_GiaiMa_TaiKhoan;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebViecLammoi.Models;
using WebViecLammoi.Utils;
using WebViecLammoi.DAO;
using WebViecLammoi.Filters;
using System.Data.SqlClient;
using System.Configuration;
using Facebook;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Net.PeerToPeer;
//using DotNetOpenAuth.AspNet.Clients;
namespace WebViecLammoi.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        VLDB dbc = new VLDB();
        int TT_Tinh = int.Parse(XString.TinhId);
        public ActionResult _Login()
        {
            New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
            Session["Pay"] = New_Dao.Pay_Sys.Pay;
            var cookie = Request.Cookies["user_log_new"];
            if (cookie != null)
            {
                ViewBag.UserName = cookie.Values["User"];
                ViewBag.Password = cookie.Values["Pw"];
                ViewBag.reme = "kh";
            }
            else
            {
                ViewBag.UserName = "";
                ViewBag.Password = "";
                ViewBag.reme = "";
            }
            return View("_Login");
        }
        [HttpPost]
        public ActionResult Login(String UserName, String Password, System.Boolean? remember)
        {
            Session.Remove("User"); Session.Remove("quyen"); Session.Remove("UsrID");
            Session.Remove("emailFace"); Session.Remove("Maunen"); //Session.Remove("SanMoi");
            //session Nhut
            Session.Remove("NTV_ID");
            Session.Remove("TenNTV");
            Session.Remove("NgaySinhNTV");
            Session.Remove("DiaChiNTV");
            Session.Remove("DienThoaiNTV");
            Session.Remove("DN_ID");
            Session.Remove("TenDN");
            Session.Remove("DiaChiDN");
            Session.Remove("DienThoaiDN");
            Session.Remove("ThongBao_DN_TD");
            Session.Remove("ThongBao_KH_TimViec");
            var tk = new UserWeb();
            TaiKhoanInfo tk_check = new TaiKhoanInfo();
            //tài khoản không phân biệt hoa thường.
            var user = dbc.UserWebs.Where(p => p.UserName.ToLower() == UserName.ToLower()).SingleOrDefault();
            if (user != null)
            {
                var time_locked = user.LastLockedChangedDate;
                var check_time = DateTime.Now - time_locked;

                // Kiểm tra xem thời giạn bị khóa trên 10 chưa ? Nếu trên 10p reset lại thành false
                if (check_time.Minutes > 5 && user.IsLocked == true)
                {
                    user.IsLocked = false;
                    user.FailedPasswordAttemptCount = 0;
                    dbc.SaveChanges();
                }
                // Sau khi kiểm tra tình trạng khóa tài khoản thì kiểm tra đăng nhập
                if (user.IsLocked == true)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa.Vui lòng đăng nhập sau " + (5 - check_time.Minutes) + " phút " + (60 - check_time.Seconds) + " giây");
                }
                else
                {
                    string check_pass = tk_check.EnCryptDotNetNukePassword(Password, "", user.PasswordSalt);//pass ma hoa
                    if (user.Password == check_pass)
                    {
                        int UserFirt = user.UserID;
                        var quyenAd = dbc.UserWebs.Where(p => p.UserName == UserName && p.UserRoles_NVLoaitaikhoan == 1).ToList();
                        var quyensub = dbc.UserWebs.Where(p => p.UserName == UserName && p.UserRoles_NVLoaitaikhoan == 5).ToList();
                        //21/11/2019***************************************************
                        var ntv = dbc.KhachHangs.FirstOrDefault(kh => kh.KH_ID == user.UserChild_id && user.UserRoles_NVLoaitaikhoan == 3);
                        var dn = dbc.DoanhNghieps.FirstOrDefault(kh => kh.DN_ID == user.UserChild_id && user.UserRoles_NVLoaitaikhoan == 4);
                        if (UserFirt == -1 || UserFirt == 0 || (ntv == null && dn == null) && quyenAd.Count == 0 && quyensub.Count == 0)
                        {
                            ModelState.AddModelError("", "Chào bạn, tài khoản của bạn không sử dụng được trên web của chúng tôi. Liên Hệ trung tâm để được hổ trợ, hoặc tạo tài khoản mới để đăng nhập web !!");
                        }
                        else
                        {
                            // đăng nhập thành công set lại số lần nhập sai = 0
                            if (user.FailedPasswordAttemptCount > 0)
                            {
                                user.FailedPasswordAttemptCount = 0;
                                dbc.Entry(user).State = EntityState.Modified;
                                dbc.SaveChanges();
                            }


                            string[] user_log = new string[2];
                            user_log[0] = UserName;
                            user_log[1] = Password;
                            Session["User"] = UserName;
                            Session["UsrID"] = UserFirt;

                            var cookie = new HttpCookie("user_log_new");
                            if (remember != null)
                            {
                                if (remember == true)
                                {
                                    cookie.Values["User"] = UserName;
                                    cookie.Values["Pw"] = Password;
                                    cookie.Expires = DateTime.Now.AddMonths(1);
                                }
                                else
                                {
                                    cookie.Expires = DateTime.Now;
                                }
                            }
                            else
                            {
                                cookie.Expires = DateTime.Now.AddDays(-1);
                            }
                            Response.Cookies.Add(cookie);

                            if (user.UserRoles_NVLoaitaikhoan == 1)
                            {
                                Session["quyen"] = "ADMIN";
                                var uID = Session["UsrID"];
                                var model_uid = dbc.UserWebs.Find(int.Parse(uID.ToString()));
                                bool nhatky = DAO.NhatKy_Admin_DAO.InsertNhatKy_Admin(dbc, int.Parse(uID.ToString()), model_uid.UserRoles_NVLoaitaikhoan
                                            , model_uid.UserName, DateTime.Now, "Đăng Nhập", "autu");
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else if (user.UserRoles_NVLoaitaikhoan == 5)
                            {
                                Session["quyen"] = "QT";
                                var uID = Session["UsrID"];
                                var model_uid = dbc.UserWebs.Find(int.Parse(uID.ToString()));
                                bool nhatky = DAO.NhatKy_Admin_DAO.InsertNhatKy_Admin(dbc, int.Parse(uID.ToString()), model_uid.UserRoles_NVLoaitaikhoan
                                            , model_uid.UserName, DateTime.Now, "Đăng Nhập", "autu");
                            }
                            else
                            {
                                //mơi 14/08/2019 *******************************************************
                                FormsAuthentication.RedirectFromLoginPage(Session["UsrID"].ToString(), true);
                                //mơi 14/08/2019 *******************************************************
                                var uID = Session["UsrID"];
                                if (user.UserRoles_NVLoaitaikhoan == 3)
                                {
                                    Session["quyen"] = "NTV";

                                    int UsId = int.Parse(uID.ToString());
                                    var UserWeb = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == UsId);
                                    var ThongTinNTV = dbc.KhachHangs.Where(kh => kh.KH_ID == UserWeb.UserChild_id).Single();

                                    Session["NTV_ID"] = ThongTinNTV.KH_ID;
                                    Session["TenNTV"] = ThongTinNTV.HoTen;
                                    Session["NgaySinhNTV"] = ThongTinNTV.NgaySinh.Date;
                                    Session["DiaChiNTV"] = ThongTinNTV.TamTru_DiaChi;
                                    Session["DienThoaiNTV"] = ThongTinNTV.DienThoai;

                                }
                                if (user.UserRoles_NVLoaitaikhoan == 4)
                                {
                                    Session["quyen"] = "TD";
                                    int UsId = int.Parse(uID.ToString());
                                    var UserWeb = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == UsId);
                                    var ThongTinDN = dbc.DoanhNghieps.Where(kh => kh.DN_ID == UserWeb.UserChild_id).Single();
                                    //Session Nhut
                                    Session["DN_ID"] = ThongTinDN.DN_ID;
                                    Session["TenDN"] = ThongTinDN.TenDoanhNghiep;
                                    Session["DiaChiDN"] = ThongTinDN.DiaChi;
                                    Session["DienThoaiDN"] = ThongTinDN.DienThoai;

                                }
                            }
                            Session.Remove("Thongbaodangky");
                            //tro lai trang truoc do 
                            var requestUri = Session["requestUri"] as string;
                            if (requestUri != null)
                            {
                                return Redirect(requestUri);
                            }
                            else { return RedirectToAction("Index", "Home"); }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu sai vui lòng nhập lại");
                        //đếm số lần nhập sai
                        user.FailedPasswordAttemptCount += 1;
                        if (user.FailedPasswordAttemptCount == 5)
                        {
                            //Sai 5 lần tài khoản bị khóa
                            user.IsLocked = true;
                            user.LastLockedChangedDate = DateTime.Now;
                            dbc.Entry(user).State = EntityState.Modified;
                            dbc.SaveChanges();
                            ModelState.AddModelError("", "Nhập sai password liên tiếp 5 lần !!! Tài khoản của bạn bị khóa !!!");
                        }
                        else
                        {
                            dbc.Entry(user).State = EntityState.Modified;
                            dbc.SaveChanges();
                            ModelState.AddModelError("", "Nhập sai password lần: " + user.FailedPasswordAttemptCount);
                        }
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Sai tên đăng nhập");
            }
            return PartialView("_Login");
        }
        public ActionResult _Register()
        {
            Session.Remove("User_re");
            ViewBag.UserRoles_NVLoaitaikhoan = new SelectList(dbc.NhanVien_LoaiTaiKhoan, "NhanVien_LoaiTaiKhoan_ID", "[TenLoaiTaiKhoan]");

            return View("_Register");
        }
        [HttpPost]
        public ActionResult _Register(string captcha, string UserName, string Password, string PasswordSec, string EmailConnection, int UserRoles_NVLoaitaikhoan)
        {
            if (captcha != null)
            {
                //kiemtra captcha
                string getcaptcha = Session["captchar"].ToString();
                if (captcha == getcaptcha)
                {
                    //tài khoản không phân biệt hoa thường.
                    var user = dbc.UserWebs.Where(p => p.UserName.ToLower() == UserName.ToLower()).ToList();

                    if (Password != PasswordSec)
                    {
                        ModelState.AddModelError("", "Xác nhận mật khẩu không thành công");

                    }
                    else if (user.Count == 0)
                    {
                        //lưu thông tin user vào session
                        string[] user_register = new string[4];
                        user_register[0] = UserName;
                        user_register[1] = Password;
                        user_register[2] = PasswordSec;
                        user_register[3] = EmailConnection;

                        Session["User_re"] = user_register;


                        if (UserRoles_NVLoaitaikhoan == 4)
                        {
                            return RedirectToAction("_JobAccount", "Account");

                        }
                        else return RedirectToAction("_CandidateAccount", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản này đã được sử dụng");
                    }
                }
                else { ViewData["captcha"] = " Điền Chính Xác Dòng Mã Này !!!"; }
            }


            return PartialView("_Register");
        }
        public FileResult GetCaptchaImage()
        {
            //get Random text
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            Session["captchar"] = randomText.ToString();

            string text = randomText.ToString();

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }
        [ProtectUser]
        public ActionResult _ChangePass()
        {

            return View("_ChangePass");
        }
        [HttpPost]
        public ActionResult _ChangePass(string old_password, string new_password, string new_password2)
        {
            if (ModelState.IsValid)
            {
                TaiKhoanInfo tk = new TaiKhoanInfo();
                //kiểm tra tồn tại pass và user trong csdl

                var member = dbc.UserWebs.Find(int.Parse(Session["UsrID"].ToString()));
                string pass = tk.DeCryptDotNetNukePassword(member.Password.ToString(), "A872EDF100E1BC806C0E37F1B3FF9EA279F2F8FD378103CB", member.PasswordSalt.ToString());
                if (pass == old_password)
                {
                    if (new_password == new_password2)
                    {
                        string PasswordSalt = Convert.ToBase64String(tk.GenerateSalt()); //tạo chuổi salt ngẫu nhiên
                        string cipherPass2 = tk.EnCryptDotNetNukePassword(new_password, "", PasswordSalt);

                        member.Password = cipherPass2;
                        member.PasswordSalt = PasswordSalt;
                        member.LastPasswordChangedDate = DateTime.Now;
                        dbc.Entry(member).State = EntityState.Modified;
                        dbc.SaveChanges();
                        ModelState.AddModelError("", "Đổi mật khẩu thành công");
                    }
                    else { ModelState.AddModelError("", "Mật khẩu mới phải viết 2 lần giống nhau!"); }

                }
                else { ModelState.AddModelError("", "Sai mật khẩu hiện tại!"); }
            }
            return View("_ChangePass");
        }
        public ActionResult _ForgotPass()
        {
            return PartialView("_ForgotPass");
        }
        [HttpPost]
        public ActionResult _ForgotPass(string UserName)
        {
            if (UserName != "")
            {
                var user_mem = dbc.UserWebs.Where(kh => kh.UserName == UserName).Single();
                TaiKhoanInfo tk = new TaiKhoanInfo();
                string pas = tk.DeCryptDotNetNukePassword(user_mem.Password.ToString(), "A872EDF100E1BC806C0E37F1B3FF9EA279F2F8FD378103CB", user_mem.PasswordSalt.ToString());
                string Conemail = ConfigurationManager.AppSettings["Email"];
                var kq = Mailer.Send(user_mem.EmailConnection, "Quên password", "password của bạn là: " + pas);
                if (kq)
                {
                    ModelState.AddModelError("", "Đã gửi mật khẩu đến email " + user_mem.EmailConnection);
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi !!! Không thể gửi mật khẩu đến email " + user_mem.EmailConnection + " !!!");
                }
            }
            return PartialView("_ForgotPass");
        }
        public ActionResult _JobAccount()
        {
            //load trang NTV
            ViewBag.LoaiHinhDoanhNghiep_ID = new SelectList(dbc.DM_LoaiHinhDoanhNghiep.ToList(), "LoaiHinhDoanhNghiep_ID", "TenLoaiHinhDoanhNghiep");
            ViewBag.Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == 0 && nh.KichHoat == true).OrderByDescending(nh => nh.MoTa), "Id", "TenDiaChi", TT_Tinh);
            ViewBag.ChucVu = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh");
            ViewBag.NganhKD_ID = new SelectList(dbc.DM_NganhKinhDoanh.ToList().OrderBy(nh => nh.Nganh_ID), "Nganh_ID", "TenNganhKD");
            ViewBag.KhuCongNghiep_ID = new SelectList(dbc.DM_KhuCongNghiep.ToList(), "KhuCongNghiep_ID", "TenKhuCongNghiep");

            return PartialView("_JobAccount");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _JobAccount(int Xa_ID, DoanhNghiep dn)//DoanhNghiep đăng ký
        {
            ViewBag.LoaiHinhDoanhNghiep_ID = new SelectList(dbc.DM_LoaiHinhDoanhNghiep.ToList(), "LoaiHinhDoanhNghiep_ID", "TenLoaiHinhDoanhNghiep");
            ViewBag.Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == 0 && nh.KichHoat == true).OrderByDescending(nh => nh.MoTa), "Id", "TenDiaChi", TT_Tinh);
            ViewBag.ChucVu = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh");
            ViewBag.NganhKD_ID = new SelectList(dbc.DM_NganhKinhDoanh.ToList().OrderBy(nh => nh.Nganh_ID), "Nganh_ID", "TenNganhKD");
            ViewBag.KhuCongNghiep_ID = new SelectList(dbc.DM_KhuCongNghiep.ToList(), "KhuCongNghiep_ID", "TenKhuCongNghiep");
            if (Session["User_re"] != null)
            {
                try
                {
                    var user_register = new string[4];
                    user_register = Session["User_re"] as string[];

                    TaiKhoanInfo tk = new TaiKhoanInfo();
                    // ghi bảng DoanhNghiep
                    DoanhNghiep model = new DoanhNghiep();
                    model = dn;
                    model.TenDoanhNghiep = dn.TenDoanhNghiep;
                    model.TenNgan = dn.TenNgan;
                    model.DienThoai = dn.DienThoai;
                    model.NguoiDaiDien_DienThoai = dn.NguoiDaiDien_DienThoai;
                    model.Email = dn.Email;
                    model.NguoiTao = 0;
                    model.NgayTao = DateTime.Now;
                    model.NgayCapNhat = DateTime.Now;
                    model.NguoiCapNhat = 0;
                    model.KichHoat = true;
                    var file = Request.Files["Logo"];
                    if (file.ContentLength > 0)
                    {
                        var ten = file.FileName;
                        var ext = ten.Substring(ten.LastIndexOf('.'));
                        if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                        {
                            ten = Guid.NewGuid() + ext;
                            dn.Logo = "logo/" + ten;
                            //Thu nho hinh anh qua to
                            WebImage img = new WebImage(file.InputStream);
                            if (img.Width > 300)
                                img.Resize(300, 350);
                            img.Save(XString.maplocal + "Logo_DN\\" + ten);
                        }
                        else dn.Logo = "logo/TNThumbnail.jpg";
                    }
                    else dn.Logo = "logo/TNThumbnail.jpg";
                    model.Logo = dn.Logo;

                    dbc.DoanhNghieps.Add(model);
                    int kt1 = dbc.SaveChanges();
                    // ghi bảng UserWeb
                    string PasswordSalt = Convert.ToBase64String(tk.GenerateSalt());
                    string CirpherPass = tk.EnCryptDotNetNukePassword(user_register[1].ToString(), "", PasswordSalt);

                    var InserUserDN = new DAO.NhatKy_Admin_DAO().InsertUserWeb(
                        user_register[0].ToString(), user_register[3].ToString(), model.DN_ID, PasswordSalt, CirpherPass, 4);
                }
                catch (Exception ex)
                {
                    Session["Thongbaodangky"] = ex.Message + ".Có Lỗi, xem lại đường truyền internet rồi đăng ký lại !!!";
                    Session.Remove("User_re");
                    return PartialView("_Login");
                }

            }
            else
            {

                Session["Thongbaodangky"] = "Thông báo, đã hết thời gian chờ đăng ký, mời bạn đăng ký lại !!!";
                return PartialView("_Login");
            }
            Session.Remove("User_re");
            Session["Thongbaodangky"] = "Đăng Ký thành công, mời bạn đăng nhập lại.";
            return PartialView("_Login");
        }
        [ProtectTD]
        public ActionResult _MainJob()
        {

            return View();
        }
        public ActionResult _JobCategory()
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var TD_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);
                ViewBag.totalungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByDN(dbc, TD_ID.UserChild_id).Count();
                ViewBag.TotalDaLuu = dbc.Database.SqlQuery<PHU_HSTV_Luu>("select t1.Id,t1.UserId,t1.NTV_Id,t1.HSTV_Id,t1.TenHoSo,t1.NgayTao " +
                    "from [VLDB].[dbo].[PHU_HSTV_Luu] t1 " +
                    "inner join " +
                    "[VLDB].[dbo].[KhachHang_TimViecLam] t2 on t1.HSTV_Id = t2.TimViec_ID" +
                    " where t2.TinhTrangPheDuyetHoSo_ID = 3 and t2.HienThiTrenWeb = 1 and t2.NgayHoSoHetHan >= GETDATE() and UserId=" + uID).Count();
            }
            return PartialView();
        }
        public ActionResult _JobInfo()
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var DN_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);
                if (DN_ID != null)
                {
                    ViewBag.DoanhNghiep = dbc.Database.SqlQuery<DoanhNghiep>("select * from [VLDB].[dbo].[DoanhNghiep] where DN_ID=" + DN_ID.UserChild_id);
                    return PartialView();
                }
                else
                {
                    //tro lai trang truoc do 27/06/2020
                    var requestUri = Session["requestUri"] as string;
                    if (requestUri != null)
                    {
                        return Redirect(requestUri);
                    }
                    else { return RedirectToAction("Index", "Home"); }

                }
            }
            return Json("No", JsonRequestBehavior.AllowGet);

        }
        public ActionResult _JobDoc()
        {
            if (Session["UsrID"] != null)
            {

                int uID = int.Parse(Session["UsrID"].ToString());
                var DN_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);
                if (DN_ID != null)
                {

                    var DNId = dbc.Database.SqlQuery<int>("select DN_ID from [VLDB].[dbo].[DoanhNghiep] where DN_ID=" + DN_ID.UserChild_id);
                    ViewBag.TongdsSMS = new DN_HoSoTuyenDung_Dao().Tinchuaxem(DNId.First());
                    ViewBag.DSHSbyDN = new DAO.DN_HoSoTuyenDung_Dao().GetDSHSbyDN(DNId.First());
                    New_Dao.Pay_Sys = New_Dao.GetPay_Sys(dbc);
                    Session["Pay"] = New_Dao.Pay_Sys.Pay;
                    if ((bool)Session["Pay"] == true)
                    {
                        var dsTongdiem = dbc.Pay_TonCuois.FirstOrDefault(kh => kh.DNID == DNId.FirstOrDefault());
                        ViewBag.Tongdiem = (dsTongdiem != null) ? dsTongdiem.SoDiemTon : 0;
                        var dsgiasp = dbc.Pay_GiaSPs.Find(1);
                        ViewBag.GiaSP = (dsgiasp != null) ? dsgiasp.Gia : 0;
                        return PartialView("_JobDoc_Pay");
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                else
                {
                    //tro lai trang truoc do 27/06/2020
                    var requestUri = Session["requestUri"] as string;
                    if (requestUri != null)
                    {
                        return Redirect(requestUri);
                    }
                    else { return RedirectToAction("Index", "Home"); }
                }
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }

        public ActionResult _JobManagement()
        {
            if (Session["UsrID"] != null)
            {

                int uID = int.Parse(Session["UsrID"].ToString());
                var TD_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 4);

                ViewBag.HSPhuHop = new NTV_HoSoXinViec_Dao().GetmapTVLbyTieudeTD(TD_ID.UserChild_id);
                ViewBag.HSDaLuu = dbc.Database.SqlQuery<PHU_HSTV_Luu>("select t1.Id,t1.UserId,t1.NTV_Id,t1.HSTV_Id,t1.TenHoSo,t1.NgayTao " +
                    "from [VLDB].[dbo].[PHU_HSTV_Luu] t1 " +
                    "inner join " +
                    "[VLDB].[dbo].[KhachHang_TimViecLam] t2 on t1.HSTV_Id = t2.TimViec_ID" +
                    " where t2.TinhTrangPheDuyetHoSo_ID = 3 and t2.HienThiTrenWeb = 1 and t2.NgayHoSoHetHan >= GETDATE() and UserId=" + uID);
                //
                var listTDbyDN = new DAO.DN_HoSoTuyenDung_Dao().GetDSHSbyDN(TD_ID.UserChild_id);
                for(int i= 0;i< listTDbyDN.Count; i++)
                {
                    var num = listTDbyDN[i].TuyenDung_ID;
                    //var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[DoanhNghiep_UngTuyen] set DN_Daxem=1 where TuyenDung_ID=@Id",
                    //                        new SqlParameter("@Id", listTDbyDN[i].TuyenDung_ID));
                    var item = dbc.DoanhNghiep_UngTuyens.FirstOrDefault(kh => kh.TuyenDung_ID == num);
                    if (item != null)
                    {
                        if (item.DN_Daxem == false)
                        {
                            item.NgayUpdate = DateTime.Now;
                            item.DN_Daxem = true;
                            try
                            {
                                var kq = DAO.DN_UngTuyen_Dao.Update_DoanhNghiep_UngTuyen(dbc, item);
                            }
                            catch (Exception ex)
                            {
                                var kq = ex.Message;
                            }
                        }
                    }
                }
                //thêm hienthitrenweb
                ViewBag.HSungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByDN(dbc, TD_ID.UserChild_id);

                return PartialView();
            }
            return Json("No", JsonRequestBehavior.AllowGet);

        }
        [ProtectTD]
        public ActionResult DeleteJobDoc(int Id)
        {
            var model = dbc.DoanhNghiep_TuyenDung.Find(Id);
            if (model != null)
            {
                try
                {
                    var XoaNLD = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung_NghiepVu] where TuyenDung_ID=" + model.TuyenDung_ID);
                    var XoaNLD2 = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung_NgoaiNgu] where TuyenDung_ID=" + model.TuyenDung_ID);
                    var XoaNLD3 = dbc.Database.ExecuteSqlCommand("DELETE  FROM [VLDB].[dbo].[DoanhNghiep_TuyenDung_TinHoc] where TuyenDung_ID=" + model.TuyenDung_ID);
                    dbc.DoanhNghiep_TuyenDung.Remove(model);
                    dbc.SaveChanges();

                    Session["ThongBao_DN_TD"] = "Delete thành công hồ sơ: " + model.TieuDeTuyenDung + "-" + model.TuyenDung_ID;
                    return View("_MainJob");
                }
                catch
                {
                    Session["ThongBao_DN_TD"] = "Có lỗi delete hồ sơ: " + model.TieuDeTuyenDung + " !!!";
                    return View("_MainJob");
                }
            }
            return View("_MainJob");
        }
        [ProtectTD]
        public ActionResult _EditJobAccount()//update tài khoản doanh nghiệp
        {
            //load trang DN
            var uID = Session["UsrID"];
            int UsId = int.Parse(uID.ToString());
            var UserWeb = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == UsId && kh.UserRoles_NVLoaitaikhoan == 4);
            var model = dbc.DoanhNghieps.Where(kh => kh.DN_ID == UserWeb.UserChild_id).Single();

            ViewBag.LoaiHinhDoanhNghiep_ID = new SelectList(dbc.DM_LoaiHinhDoanhNghiep.ToList(), "LoaiHinhDoanhNghiep_ID", "TenLoaiHinhDoanhNghiep", model.LoaiHinhDoanhNghiep_ID);
            ViewBag.Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == 0).OrderByDescending(nh => nh.MoTa), "Id", "TenDiaChi", model.Tinh_ID);
            ViewBag.Huyen_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == model.Tinh_ID), "Id", "TenDiaChi", model.Huyen_ID);
            ViewBag.Xa_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == model.Huyen_ID), "Id", "TenDiaChi", model.Xa_ID);

            ViewBag.ChucVu = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh", model.ChucVu);
            ViewBag.NganhKD_ID = new SelectList(dbc.DM_NganhKinhDoanh.ToList().OrderBy(nh => nh.Nganh_ID), "Nganh_ID", "TenNganhKD", model.NganhKD_ID);

            ViewBag.NgheKD_ID = new SelectList(dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == model.NganhKD_ID), "NgheKD_ID", "TenNgheKD", model.NgheKD_ID);

            ViewBag.KhuCongNghiep_ID = new SelectList(dbc.DM_KhuCongNghiep.ToList(), "KhuCongNghiep_ID", "TenKhuCongNghiep", model.KhuCongNghiep_ID);
            return View("_EditJobAccount", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _EditJobAccount(DoanhNghiep modleDN, string Logo1, DateTime? NgayThanhLap1,
            string TenDoanhNghiep_kh, string TenNgan_kh, string DienThoai_kh, string Email_kh, string NguoiDaiDien_DienThoai_kh)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["Logo1"];
                if (file.ContentLength > 0)
                {
                    //Xoas hinh cu
                    var tenhinhcu = XString.Cutstring(modleDN.Logo);
                    bool xoahinh = XString.Xoahinhcu("Logo_DN", tenhinhcu);
                    //chep hinh moi
                    var ten = file.FileName;
                    var ext = ten.Substring(ten.LastIndexOf('.'));
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                    {
                        ten = Guid.NewGuid() + ext;
                        modleDN.Logo = "logo/" + ten;
                        //Thu nho hinh anh qua to
                        WebImage img = new WebImage(file.InputStream);
                        if (img.Width > 300)
                            img.Resize(300, 350);
                        img.Save(XString.maplocal + "Logo_DN\\" + ten);
                    }
                    else modleDN.Logo = "";
                }
                else if (modleDN.Logo == null) { modleDN.Logo = ""; }
                if (NgayThanhLap1 != null)
                {
                    modleDN.NgayThanhLap = NgayThanhLap1;
                }
                var UsId = Session["UsrID"];
                modleDN.NguoiCapNhat = int.Parse(UsId.ToString());
                modleDN.NgayCapNhat = DateTime.Now;
                if (modleDN.KichHoat == null)
                {
                    modleDN.KichHoat = true;
                }
                if (modleDN.NgayTao == null) { modleDN.NgayTao = DateTime.Now; }
                modleDN.Skype = "";
                modleDN.Facebook = "";
                modleDN.SoDangKyKinhDoanh = "";
                modleDN.MaSoThue = "";
                modleDN.MaVach = "";
                modleDN.TenDoanhNghiep = TenDoanhNghiep_kh;
                modleDN.TenNgan = TenNgan_kh;
                modleDN.DienThoai = DienThoai_kh;
                modleDN.Email = Email_kh;
                modleDN.NguoiDaiDien_DienThoai = NguoiDaiDien_DienThoai_kh;
                dbc.Entry(modleDN).State = EntityState.Modified;
                dbc.SaveChanges();
                Session["ThongBao_DN_TD"] = "Update thông tin Cty thành công.";
                return PartialView("_MainJob");
            }
            else
            {
                var uID = Session["UsrID"];
                int UsId = int.Parse(uID.ToString());
                var UserWeb = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == UsId && kh.UserRoles_NVLoaitaikhoan == 4);
                var model1 = dbc.DoanhNghieps.Where(kh => kh.DN_ID == UserWeb.UserChild_id).Single();
                ModelState.AddModelError("", "Có lỗi xãy ra, không thể lưu dữ liệu !!!");
                ViewBag.LoaiHinhDoanhNghiep_ID = new SelectList(dbc.DM_LoaiHinhDoanhNghiep.ToList(), "LoaiHinhDoanhNghiep_ID", "TenLoaiHinhDoanhNghiep", model1.LoaiHinhDoanhNghiep_ID);
                ViewBag.Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == 0).OrderByDescending(nh => nh.MoTa), "Id", "TenDiaChi", model1.Tinh_ID);
                ViewBag.ChucVu = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh", model1.ChucVu);
                ViewBag.NganhKD_ID = new SelectList(dbc.DM_NganhKinhDoanh.ToList().OrderBy(nh => nh.Nganh_ID), "Nganh_ID", "TenNganhKD", model1.NganhKD_ID);
                ViewBag.NgheKD_ID = new SelectList(dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == model1.NganhKD_ID), "NgheKD_ID", "TenNgheKD", model1.NgheKD_ID);
                ViewBag.KhuCongNghiep_ID = new SelectList(dbc.DM_KhuCongNghiep.ToList(), "KhuCongNghiep_ID", "TenKhuCongNghiep", model1.KhuCongNghiep_ID);
                return View("_EditJobAccount", model1);
            }
        }
        [ProtectTD]
        public ActionResult EditJobDoc(int Id)
        {
            var model = dbc.DoanhNghiep_TuyenDung.Find(Id);
            ViewBag.ChucDanh_ID = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh", model.ChucDanh_ID);
            ViewBag.ThoiGianLamViec_ID = new SelectList(dbc.DM_ThoiGianLamViec.OrderByDescending(kh => kh.ThoiGianLamViec_ID), "ThoiGianLamViec_ID", "TenThoiGianLamViec", model.ThoiGianLamViec_ID);
            ViewBag.YeuCauTrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList(), "TrinhDoChuyenMon_ID", "TenChuyenMon", model.YeuCauTrinhDo_ID);
            ViewBag.YeuCauNganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong", model.YeuCauNganh_ID);

            ViewBag.YeuCauNghe_ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == model.YeuCauNganh_ID), "NgheLaoDong_ID", "TenNgheLaoDong", model.YeuCauNghe_ID);

            var modal_TD_nghiepvu3 = dbc.DoanhNghiep_TuyenDung_NghiepVu.Where(kh => kh.TuyenDung_ID == model.TuyenDung_ID)
                .OrderBy(kh => kh.TuyenDung_NghiepVu_ID)
                .Take(1)
                .ToList();
            var modal_TD_nghiepvu22 = dbc.DoanhNghiep_TuyenDung_NgoaiNgu.Where(kh => kh.TuyenDung_ID == model.TuyenDung_ID)
                .OrderBy(kh => kh.TuyenDung_NgoaiNgu_ID)
                .Skip(1)
                .Take(1)
                .ToList();
            var modal_TD_nghiepvu2 = dbc.DoanhNghiep_TuyenDung_NgoaiNgu.Where(kh => kh.TuyenDung_ID == model.TuyenDung_ID)
                .OrderBy(kh => kh.TuyenDung_NgoaiNgu_ID)
                .Take(1)
                .ToList();
            var modal_TD_nghiepvu11 = dbc.DoanhNghiep_TuyenDung_TinHoc.Where(kh => kh.TuyenDung_ID == model.TuyenDung_ID)
                .OrderBy(kh => kh.NghiepVu_TinHoc_ID)
                .Skip(1)
                .Take(1)
                .ToList();
            var modal_TD_nghiepvu1 = dbc.DoanhNghiep_TuyenDung_TinHoc.Where(kh => kh.TuyenDung_ID == model.TuyenDung_ID)
                .OrderBy(kh => kh.NghiepVu_TinHoc_ID)
                .Take(1)
                .ToList();
            ///////////khác
            if (modal_TD_nghiepvu3.Count != 0)
            {
                ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_TD_nghiepvu3.First().NghiepVu_ID);
            }
            else { ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }
            //////////////NgoaiNgu
            if (modal_TD_nghiepvu2.Count != 0)
            {
                ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_TD_nghiepvu2.First().NghiepVu_NgoaiNgu_ID);
            }
            else
            {
                ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            }
            if (modal_TD_nghiepvu22.Count != 0)
            {
                ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_TD_nghiepvu22.First().NghiepVu_NgoaiNgu_ID);
            }
            else { ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }
            ////////////Tinhoc
            if (modal_TD_nghiepvu1.Count != 0)
            {
                ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_TD_nghiepvu1.First().NghiepVu_TinHoc_ID);
            }
            else
            {
                ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            }
            if (modal_TD_nghiepvu11.Count != 0)
            {
                ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_TD_nghiepvu11.First().NghiepVu_TinHoc_ID);
            }
            else { ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }


            ViewBag.NoiLamViec_TinhID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == 0).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi", model.NoiLamViec_TinhID);
            ViewBag.NoiLamViec_HuyenID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == model.NoiLamViec_TinhID).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi", model.NoiLamViec_HuyenID);

            return View("_JobEdit", model);
        }
        [ProtectTD]
        public ActionResult DeleteCandidateSave(int Id)
        {
            var model = dbc.PHU_HSTV_Luu.Find(Id);
            try
            {
                dbc.PHU_HSTV_Luu.Remove(model);
                dbc.SaveChanges();
                ModelState.Clear();//xoa thong tin tren form
                ModelState.AddModelError("", "Xóa Thành Công !!!");
                return View("_MainJob");
            }
            catch
            {
                ModelState.AddModelError("", "Xóa Lỗi !!!");
                return View("_MainJob");
            }
        }
        [ProtectTD]
        [ValidateInput(false)]
        public ActionResult UpdateDocFile(DoanhNghiep_TuyenDung model, DateTime? NgayNhanHoSo1, DateTime NgayHetHan1, int NghiepVu1, int NghiepVu11, int NghiepVu2, int NghiepVu22, int NghiepVu3, string Tieude_kh)
        {
            try
            {
                //var update = dbc.DoanhNghiep_TuyenDung.Find(model.TuyenDung_ID);

                int uID = int.Parse(Session["UsrID"].ToString());
                if (ModelState.IsValid)
                {

                    model.NgayHetHan = NgayHetHan1 > DateTime.Now ? NgayHetHan1 : model.NgayHetHan;
                    model.NgayNhanHoSo = NgayNhanHoSo1 != null ? NgayNhanHoSo1 : model.NgayNhanHoSo;
                    //model.YeuCauNghe_ID = NgheKD_ID;
                    model.NgayCapNhat = DateTime.Now;
                    string M = model.MoTaCongViec;
                    string Q = model.QuyenLoi;
                    string TD = Tieude_kh;
                    model.MoTaCongViec = M != null ? M : "";
                    model.QuyenLoi = Q != null ? Q : "";
                    model.YeuCauCongViec = model.YeuCauCongViec == null ? " " : model.YeuCauCongViec;
                    var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[DoanhNghiep_TuyenDung] set [TieuDeTuyenDung]=@TenHoSo," +
                    "[ChucDanh_ID]=@ChucDanh,[YeuCauCongViec]=@yccv, [MoTaCongViec]=@MoTa, [QuyenLoi]=@QuyenLoi,[LuongTu]=@LuongTu,[LuongDen]=@LuongDen," +
                    "[ThoiGianLamViec_ID]=@ThoiGianLamViec_ID,[YeuCauTrinhDo_ID]=@YeuCauTrinhDo_ID,[SoNamKinhNghiem]=@SoNamKinhNghiem," +
                    "YeuCauNghe_ID=@YeuCauNghe_ID,YeuCauNganh_ID=@YeuCauNganh_ID," +
                    "[YeuCauGioiTinh]=@YeuCauGioiTinh,[SoLuongTuyen]=@SoLuongTuyen," +
                    "[YeuCauTuoiTu]=@YeuCauTuoiTu,[YeuCauTuoiDen]=@YeuCauTuoiDen," +
                    "[NgayNhanHoSo]=@NgayNhanHoSo,[NoiNopHoSo]=@NoiNopHoSo," +
                    "[NgayHetHan]=@NgayHetHanHoSo,[NgayCapNhat]=@NgayCapNhat,[NoiLamViec_TinhID]=@NoiLamViec_TinhID," +
                    "[NoiLamViec_HuyenID]=@NoiLamViec_HuyenID where TuyenDung_ID=@Id",
                    new SqlParameter("@TenHoSo", TD),
                    new SqlParameter("@ChucDanh", model.ChucDanh_ID),
                    new SqlParameter("@yccv", model.YeuCauCongViec),
                    new SqlParameter("@MoTa", model.MoTaCongViec),
                    new SqlParameter("@QuyenLoi", model.QuyenLoi),
                    new SqlParameter("@LuongTu", model.LuongTu),
                    new SqlParameter("@LuongDen", model.LuongDen),
                    new SqlParameter("@ThoiGianLamViec_ID", model.ThoiGianLamViec_ID),
                    new SqlParameter("@YeuCauTrinhDo_ID", model.YeuCauTrinhDo_ID),
                    new SqlParameter("@SoNamKinhNghiem", model.SoNamKinhNghiem),

                    new SqlParameter("@YeuCauNghe_ID", model.YeuCauNghe_ID),
                    new SqlParameter("@YeuCauNganh_ID", model.YeuCauNganh_ID),

                    new SqlParameter("@YeuCauGioiTinh", model.YeuCauGioiTinh),
                    new SqlParameter("@SoLuongTuyen", model.SoLuongTuyen),
                    new SqlParameter("@YeuCauTuoiTu", model.YeuCauTuoiTu),
                    new SqlParameter("@YeuCauTuoiDen", model.YeuCauTuoiDen),

                    new SqlParameter("@NgayNhanHoSo", model.NgayNhanHoSo),
                    new SqlParameter("@NoiNopHoSo", model.NoiNopHoSo),
                    new SqlParameter("@NgayHetHanHoSo", model.NgayHetHan),
                    new SqlParameter("@NgayCapNhat", DateTime.Now),
                    new SqlParameter("@NoiLamViec_TinhID", model.NoiLamViec_TinhID),
                    new SqlParameter("@NoiLamViec_HuyenID", model.NoiLamViec_HuyenID),
                    new SqlParameter("@Id", model.TuyenDung_ID));

                    var Tin_NN_NV = new TD_NghiepVu_TinHoc_NgoaiNgu_Dao().Xoa_InsertTuyenDung_NN_TH_NV(
                        NghiepVu1, NghiepVu11, NghiepVu2, NghiepVu22, NghiepVu3, model.TuyenDung_ID, uID);
                    if (update > 0)
                    {
                        Session["ThongBao_DN_TD"] = "Update thành công hồ sơ: " + Tieude_kh;
                    }


                    return View("_MainJob");
                }
                else
                {
                    ViewBag.ChucDanh_ID = new SelectList(dbc.DM_ChucDanh.ToList(), "ChucDanh_ID", "TenChucDanh", model.ChucDanh_ID);
                    //ViewBag.MucLuongId = new SelectList(dbc.DM_MucLuong_Phus.ToList(), "Id", "TenMucLuong");
                    ViewBag.ThoiGianLamViec_ID = new SelectList(dbc.DM_ThoiGianLamViec.OrderByDescending(kh => kh.ThoiGianLamViec_ID), "ThoiGianLamViec_ID", "TenThoiGianLamViec", model.ThoiGianLamViec_ID);
                    ViewBag.YeuCauTrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList(), "TrinhDoChuyenMon_ID", "TenChuyenMon", model.YeuCauTrinhDo_ID);
                    ViewBag.YeuCauNganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong", model.YeuCauNganh_ID);
                    ViewBag.YeuCauNghe_ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == model.YeuCauNganh_ID), "NgheLaoDong_ID", "TenNgheLaoDong", model.YeuCauNghe_ID);
                    ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                    ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                    ViewBag.NghiepVu0 = new SelectList(dbc.DM_NghiepVu.OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
                    ViewBag.NoiLamViec_TinhID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == 0).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi", model.NoiLamViec_TinhID);
                    ViewBag.NoiLamViec_HuyenID = new SelectList(dbc.DM_DiaChi.Where(kh => kh.ParentId == model.NoiLamViec_TinhID).OrderByDescending(kh => kh.MoTa), "Id", "TenDiaChi", model.NoiLamViec_HuyenID);
                    var update1 = dbc.DoanhNghiep_TuyenDung.Find(model.TuyenDung_ID);
                    ModelState.AddModelError("", "Có Lỗi !!! ");
                    return View("_JobEdit", update1);
                }
            }
            catch (Exception ex)
            {
                Session["ThongBao_DN_TD"] = "Update Có Lỗi !!!" + Tieude_kh;
                return View("_MainJob");
            }
        }
        [ProtectTD]
        public ActionResult thaydoitrangthai(int tuyendungid)
        {
            var tuyendung = dbc.DoanhNghiep_TuyenDung.FirstOrDefault(kh => kh.TuyenDung_ID == tuyendungid);
            Boolean hienthi = tuyendung.HienThiWeb == true ? false : true;
            var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[DoanhNghiep_TuyenDung] set HienThiWeb=@HienThiWeb where TuyenDung_ID=@Id",
                new SqlParameter("@HienThiWeb", hienthi),
                new SqlParameter("@Id", tuyendungid));
            return Json("oooook");
        }

        //public ActionResult Guikichhoat(int tuyendungid)
        //{
        //    if(Session["UsrID"] != null)
        //    {
        //        var uID = Session["UsrID"];
        //        int UsId = int.Parse(uID.ToString());
        //        var UserWeb = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == UsId && kh.UserRoles_NVLoaitaikhoan == 4);
        //        //thêm mới CTXuatDiem
        //        Pay_CTXuatDiem model = new Pay_CTXuatDiem();
        //        model.Id = Guid.NewGuid();
        //        model.DNID = UserWeb.UserChild_id;
        //        model.Idsp = tuyendungid.ToString();
        //        model.ID_giaSP = 1;
        //        model.Ngay = DateTime.Now;
        //        dbc.Pay_CTXuatDiems.Add(model);
        //        var kq= dbc.SaveChanges();
        //        if(kq > 0)
        //        {
        //            //update tồn cuối
        //            var xuatdiem = dbc.Pay_CTXuatDiems.Where(kh=>kh.DNID == UserWeb.UserChild_id).ToList();
        //            double tongxd = 0;
        //            for(int i = 0;i< xuatdiem.Count; i++)
        //            {//cộng tất cả điểm xuất
        //                var giasp = dbc.Pay_GiaSPs.Find(xuatdiem[i].ID_giaSP);
        //                tongxd = tongxd + giasp.Gia;
        //            }
        //            var diemton = dbc.Pay_TonCuois.Find(UserWeb.UserChild_id);
        //            if(diemton != null)
        //            {//update Pay_toncuoi
        //                diemton.TongXuat = tongxd;
        //                diemton.SoDiemTon = diemton.TongNhap - tongxd;
        //                diemton.Ghichuxuat = UserWeb.UserChild_id + "_" + tuyendungid;
        //                diemton.ngay = DateTime.Now;
        //                dbc.Entry(diemton).State = EntityState.Modified;
        //                var kup= dbc.SaveChanges();
        //                if (kup > 0)
        //                {
        //                    var dsTongdiem = dbc.Pay_TonCuois.FirstOrDefault(kh => kh.DNID == UserWeb.UserChild_id);
        //                    ViewBag.Tongdiem = (dsTongdiem != null) ? dsTongdiem.SoDiemTon : 0;
        //                    //update kickhoat==False
        //                    var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[DoanhNghiep_TuyenDung] set KichHoat=0 where TuyenDung_ID=@Id",
        //                new SqlParameter("@Id", tuyendungid));
        //                    Session["ThongBao_DN_TD"] = "Update kích hoạt thành công hồ sơ :"+ tuyendungid+" .Hồ sơ của bạn đã online.";
        //                }
        //            }


        //            return Json("oooook");
        //        }
        //        return Json("error");
        //    }
        //    else
        //    {
        //        return Json("error");
        //    }

        //}
        [ProtectTD]
        public ActionResult Guipheduyet(int tuyendungid)
        {
            if (Session["UsrID"] != null)
            {
                if ((bool)Session["Pay"] == true)
                {
                    var uID = Session["UsrID"];
                    int UsId = int.Parse(uID.ToString());
                    var UserWeb = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == UsId && kh.UserRoles_NVLoaitaikhoan == 4);
                    //thêm mới CTXuatDiem
                    var CTXuat = dbc.Pay_CTXuatDiems.Where(kh => kh.Idsp == tuyendungid.ToString() && kh.DNID == UserWeb.UserChild_id);
                    if (CTXuat.Count() == 0)
                    {
                        var dsgiasp = dbc.Pay_GiaSPs.Find(1);
                        Pay_CTXuatDiem model = new Pay_CTXuatDiem();
                        model.Id = Guid.NewGuid();
                        model.DNID = UserWeb.UserChild_id;
                        model.Idsp = tuyendungid.ToString();
                        model.ID_giaSP = 1;
                        model.Ngay = DateTime.Now;
                        dbc.Pay_CTXuatDiems.Add(model);
                        var kq = dbc.SaveChanges();
                        if (kq > 0)
                        {
                            //update tồn cuối
                            var xuatdiem = dbc.Pay_CTXuatDiems.Where(kh => kh.DNID == UserWeb.UserChild_id).ToList();
                            double tongxd = 0;
                            for (int i = 0; i < xuatdiem.Count; i++)
                            {//cộng tất cả điểm xuất
                                var giasp = dbc.Pay_GiaSPs.Find(xuatdiem[i].ID_giaSP);
                                tongxd = tongxd + giasp.Gia;
                            }
                            var diemton = dbc.Pay_TonCuois.Find(UserWeb.UserChild_id);
                            if (diemton != null)
                            {//update Pay_toncuoi
                                diemton.TongXuat = tongxd;
                                diemton.SoDiemTon = diemton.TongNhap - tongxd;
                                diemton.Ghichuxuat = UserWeb.UserChild_id + "_" + tuyendungid;
                                diemton.ngay = DateTime.Now;
                                dbc.Entry(diemton).State = EntityState.Modified;
                                var kup = dbc.SaveChanges();
                                if (kup > 0)
                                {
                                    var dsTongdiem = dbc.Pay_TonCuois.FirstOrDefault(kh => kh.DNID == UserWeb.UserChild_id);
                                    ViewBag.Tongdiem = (dsTongdiem != null) ? dsTongdiem.SoDiemTon : 0;
                                    //update kickhoat==False//13/06 khong can
                                    var tuyendung = dbc.DoanhNghiep_TuyenDung.FirstOrDefault(kh => kh.TuyenDung_ID == tuyendungid);
                                    if (tuyendung != null)
                                    {
                                        var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[DoanhNghiep_TuyenDung] set TinhTrangHoSo=2 where TuyenDung_ID=@Id",
                                            new SqlParameter("@Id", tuyendungid));
                                        if (update > 0)
                                        {
                                            Session["ThongBao_DN_TD"] = "Gửi phê duyệt thành công hồ sơ :" + tuyendungid + " , trừ " + dsgiasp.Gia + " điểm. Kết quả sẻ có trong 24h.";
                                        }
                                        else
                                        {
                                            Session["ThongBao_DN_TD"] = "Có Lỗi gửi phê duyệt hồ sơ :" + tuyendungid;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Session["ThongBao_DN_TD"] = "Pay_toncuoi chưa có điểm nhập vào của DN:" + UserWeb.UserChild_id;
                            }
                        }
                        else
                        {
                            Session["ThongBao_DN_TD"] = "Có Lỗi thêm bảng Pay_CTXuatDiem :" + tuyendungid;
                        }
                    }
                    else
                    {
                        Session["ThongBao_DN_TD"] = "Hồ sơ này đã mua rồi :" + tuyendungid;
                    }
                }
                else
                {
                    var tuyendung = dbc.DoanhNghiep_TuyenDung.FirstOrDefault(kh => kh.TuyenDung_ID == tuyendungid);
                    if (tuyendung != null)
                    {
                        var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[DoanhNghiep_TuyenDung] set TinhTrangHoSo=2 where TuyenDung_ID=@Id",
                            new SqlParameter("@Id", tuyendungid));
                        if (update > 0)
                        {
                            Session["ThongBao_DN_TD"] = "Gửi phê duyệt thành công hồ sơ :" + tuyendungid;
                        }
                        else
                        {
                            Session["ThongBao_DN_TD"] = "Có Lỗi gửi phê duyệt hồ sơ :" + tuyendungid;
                        }
                    }
                }
            }
            return Json("oooook");
        }
        public ActionResult _CandidateAccount()
        {
            ViewBag.HocVan_ID = new SelectList(dbc.DM_HocVan.ToList().OrderBy(kh => kh.HocVan_ID), "HocVan_ID", "HocVan_Ten");
            ViewBag.TrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList().OrderBy(kh => kh.TrinhDoChuyenMon_ID), "TrinhDoChuyenMon_ID", "TenChuyenMon");
            ViewBag.Nganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList().OrderBy(nh => nh.NganhLaoDong_ID), "NganhLaoDong_ID", "TenNganhLaoDong");

            ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");

            //ViewBag.NghiepVu_ID = new SelectList(dbc.DM_NghiepVu.ToList().OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            //ViewBag.NghiepVu_ID_2 = new SelectList(dbc.DM_NghiepVu.ToList().OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            //ViewBag.NghiepVu_ID_3 = new SelectList(dbc.DM_NghiepVu.ToList().OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NoiCap_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == 0).OrderBy(nh => nh.TenDiaChi), "Id", "TenDiaChi", TT_Tinh);
            ViewBag.TamTru_Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.Id == TT_Tinh && nh.KichHoat == true), "Id", "TenDiaChi");
            ViewBag.TamTru_Huyen_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh && nh.KichHoat == true), "Id", "TenDiaChi");

            return PartialView("_CandidateAccount");
        }
        [HttpPost]
        public ActionResult _CandidateAccount(KhachHang KhHa, int TrinhDo_ID, int Nganh_ID,
             int Nghe_ID, int NghiepVu1, int NghiepVu11, int NghiepVu2, int NghiepVu22, int NghiepVu3)
        {
            ViewBag.HocVan_ID = new SelectList(dbc.DM_HocVan.ToList().OrderBy(kh => kh.HocVan_ID), "HocVan_ID", "HocVan_Ten");
            ViewBag.TrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList().OrderBy(kh => kh.TrinhDoChuyenMon_ID), "TrinhDoChuyenMon_ID", "TenChuyenMon");
            ViewBag.Nganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList().OrderBy(nh => nh.NganhLaoDong_ID), "NganhLaoDong_ID", "TenNganhLaoDong");
            ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            //ViewBag.NghiepVu_ID = new SelectList(dbc.DM_NghiepVu.ToList().OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            //ViewBag.NghiepVu_ID_2 = new SelectList(dbc.DM_NghiepVu.ToList().OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            //ViewBag.NghiepVu_ID_3 = new SelectList(dbc.DM_NghiepVu.ToList().OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu");
            ViewBag.NoiCap_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == 0).OrderBy(nh => nh.TenDiaChi), "Id", "TenDiaChi", TT_Tinh);
            ViewBag.TamTru_Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.Id == TT_Tinh && nh.KichHoat == true), "Id", "TenDiaChi");
            ViewBag.TamTru_Huyen_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh && nh.KichHoat == true), "Id", "TenDiaChi");
            if (Session["User_re"] != null)
            {
                try
                {
                    var user_register = new string[4];
                    user_register = Session["User_re"] as string[];

                    TaiKhoanInfo tk = new TaiKhoanInfo();

                    //ghi bảng KhachHang
                    KhachHang model = new KhachHang();
                    model = KhHa;
                    model.KichHoat = true;
                    model.NguoiTao = 0;
                    model.NguoiCapNhat = 0;
                    model.NgayTao = DateTime.Now;
                    model.NgayCapNhat = DateTime.Now;
                    model.SoLanCapNhat = 0;
                    model.HoTen = KhHa.HoTen;
                    model.DienThoai = KhHa.DienThoai;
                    model.Email = user_register[3].ToString();
                    var file = Request.Files["Hinh"];
                    if (file.ContentLength > 0)
                    {
                        var ten = file.FileName;
                        var ext = ten.Substring(ten.LastIndexOf('.'));
                        ten = Guid.NewGuid() + ext;

                        if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                        {
                            ten = Guid.NewGuid() + ext;
                            model.Hinh = "AnhDaiDien/" + ten;
                            //Thu nho hinh anh qua to
                            WebImage img = new WebImage(file.InputStream);
                            if (img.Width > 300)
                                img.Resize(300, 350);
                            img.Save(XString.maplocal + "Avatar_UV\\" + ten);
                        }
                        else { model.Hinh = "AnhDaiDien/AvataNam.jpg"; }
                    }
                    else if (KhHa.GioiTinh == 1)
                    {
                        model.Hinh = "AnhDaiDien/AvataNam.jpg";
                    }
                    else model.Hinh = "AnhDaiDien/AvataNu.jpg";

                    dbc.KhachHangs.Add(model);
                    int kt = dbc.SaveChanges();


                    //ghi bảng User
                    string PasswordSalt = Convert.ToBase64String(tk.GenerateSalt());
                    string CirpherPass = tk.EnCryptDotNetNukePassword(user_register[1].ToString(), "", PasswordSalt);
                    var insertUserkhachhang = new DAO.NhatKy_Admin_DAO().InsertUserWeb(
                        user_register[0].ToString(), user_register[3].ToString(), model.KH_ID, PasswordSalt, CirpherPass, 3);
                    //ghi bảng KhachHang_TrinhDo
                    var trinhdo = new DAO.Khachhang_trinhdo_nghiepvu_DAO().Xoa_InsertKhachHang_trinhdo(
                        TrinhDo_ID, Nganh_ID, Nghe_ID, model.KH_ID, 0);

                    //ghi bảng nghiệp vụ
                    var kqnv = new DAO.Khachhang_trinhdo_nghiepvu_DAO().Xoa_InsertKhachHang_NN_TH_NV(
                        NghiepVu1, NghiepVu11, NghiepVu2, NghiepVu22, NghiepVu3, model.KH_ID, 0);
                }
                catch (Exception ex)
                {
                    Session["Thongbaodangky"] = ex.Message + ".Có Lỗi, xem lại đường truyền internet, mời bạn đăng ký lại !!!";
                    Session.Remove("User_re");
                    return PartialView("_Login");
                }

            }
            else
            {
                Session["Thongbaodangky"] = "Thông báo, đã hết thời gian chờ đăng ký, mời bạn đăng ký lại !!!";
                return PartialView("_Login");

            }
            Session.Remove("User_re");
            Session["Thongbaodangky"] = "Đăng Ký thành công, mời bạn đăng nhập lại.";
            return PartialView("_Login");
        }

        // XỬ LÝ ĐỊA CHỈ Ở PHẦN DOANH NGHIỆP
        public ActionResult Huyen_ID(int Tinh_ID)
        {
            var Huyen_ID = dbc.DM_DiaChi.Where(kh => kh.ParentId == Tinh_ID && kh.KichHoat == true)
                            .Select(kh => new { Id = kh.Id, TenDiaChi = kh.TenDiaChi });
            return Json(Huyen_ID, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Xa_ID(int Huyen_ID)
        {
            var Xa_ID = dbc.DM_DiaChi.Where(kh => kh.ParentId == Huyen_ID && kh.KichHoat == true)
                            .Select(kh => new { Id = kh.Id, TenDiaChi = kh.TenDiaChi });
            return Json(Xa_ID, JsonRequestBehavior.AllowGet);
        }
        // XỬ LÝ LOAD NGHỀ THEO NGÀNH - DOANH NGHIỆP
        public ActionResult NgheKD_ID(int NganhKD_ID)
        {
            var NgheKD_ID = dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == NganhKD_ID)
                           .Select(kh => new { NgheKD_ID = kh.NgheKD_ID, TenNgheKD = kh.TenNgheKD });
            return Json(NgheKD_ID, JsonRequestBehavior.AllowGet);
        }
        // XỬ LÝ LOAD NGHỀ THEO NGÀNH - KHÁCH HÀNG
        public ActionResult Nghe_ID(int Nganh_ID)
        {
            var Nghe_ID = dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == Nganh_ID)
                           .Select(kh => new { NgheLaoDong_ID = kh.NgheLaoDong_ID, TenNgheLaoDong = kh.TenNgheLaoDong });
            return Json(Nghe_ID, JsonRequestBehavior.AllowGet);
        }
        // XỬ LÝ ĐỊA CHỈ TẠM TRÚ -  KHÁCH HÀNG
        public ActionResult TamTru_Huyen_ID(int TamTru_Tinh_ID)
        {
            var TamTru_Huyen_ID = dbc.DM_DiaChi.Where(kh => kh.ParentId == TamTru_Tinh_ID && kh.KichHoat == true)
                            .Select(kh => new { Id = kh.Id, TenDiaChi = kh.TenDiaChi });


            return Json(TamTru_Huyen_ID, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TamTru_Xa_ID(int TamTru_Huyen_ID)
        {
            var TamTru_Xa_ID = dbc.DM_DiaChi.Where(kh => kh.ParentId == TamTru_Huyen_ID && kh.KichHoat == true)
                            .Select(kh => new { Id = kh.Id, TenDiaChi = kh.TenDiaChi });

            return Json(TamTru_Xa_ID, JsonRequestBehavior.AllowGet);
        }
        [ProtectNTV]
        public ActionResult _MainCandidate()
        {
            return View();
        }
        public ActionResult _CandidateCategory()
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var KH_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                ViewBag.TotalHSDaLuu = dbc.Database.SqlQuery<PHU_HSTD_Luu>("select * from [VLDB].[dbo].[PHU_HSTD_Luu] where UserId =" + uID
                                                + " order by NgayTao desc").Count();
                ViewBag.TotalHSungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByNTV(dbc, KH_ID.UserChild_id).Count();
            }

            return PartialView();
        }
        public ActionResult _CandidateDoc()
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var KH_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                if (KH_ID != null)
                {
                    var NTVId = dbc.Database.SqlQuery<int>("select KH_ID from [VLDB].[dbo].[KhachHang] where KH_ID=" + KH_ID.UserChild_id);

                    ViewBag.DSHSbyNTV = dbc.Database.SqlQuery<KhachHang_TimViecLam>("select * from [VLDB].[dbo].[KhachHang_TimViecLam] where KH_ID=" + NTVId.First());
                    ViewBag.DSknCV = dbc.Database.SqlQuery<KhachHang_KinhNghiem_LamViec_2022>("select * from [VLDB].[dbo].[KhachHang_KinhNghiem_LamViec_2022] where KH_ID=" + NTVId.First());

                    return PartialView();
                }
                else
                {
                    //tro lai trang truoc do 27/06/2020
                    var requestUri = Session["requestUri"] as string;
                    if (requestUri != null)
                    {
                        return Redirect(requestUri);
                    }
                    else { return RedirectToAction("Index", "Home"); }
                }
            }
            return Json("No", JsonRequestBehavior.AllowGet);

        }
        public ActionResult _CandidateInfo()
        {
            if (Session["UsrID"] != null)
            {
                var uID = Session["UsrID"];
                var user = dbc.UserWebs.Find(uID);
                ViewBag.NTV = dbc.Database.SqlQuery<KhachHang>("select * from [VLDB].[dbo].[KhachHang] where KH_ID=" + user.UserChild_id);
                return PartialView();
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }
        
        [ProtectNTV]
        public ActionResult EditCanDoc(int id)
        {
            int a = id;
            Session["requestUri"] = "/Account/EditCanDoc/" + id;
            var model = dbc.KhachHang_TimViecLam.Find(a);
            ViewBag.ThoiGianLamViecMongMuon = new SelectList(dbc.DM_ThoiGianLamViec.ToList(), "ThoiGianLamViec_ID", "TenThoiGianLamViec", model.ThoiGianLamViecMongMuon);
            ViewBag.ChucDanhMongMuon = dbc.DM_ChucDanh.ToList();

            ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong", model.NganhMongMuon_ID);
            
            ViewBag.NgheMongMuon_ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == model.NganhMongMuon_ID), "NgheLaoDong_ID", "TenNgheLaoDong", model.NgheMongMuon_ID);
            if (model.NganhNghe34_2022 != null && model.NganhNghe34_2022 != "")
            {
                string[] NganhNghe34 = new string[2];
                NganhNghe34 = XString.Cutstringcomcoma(model.NganhNghe34_2022);
                if (NganhNghe34[0] != "")
                {
                    int mm2 = int.Parse(NganhNghe34[0]);
                    var NgheMongMuon_2ID = dbc.DM_NgheLaoDong.FirstOrDefault(kh => kh.NgheLaoDong_ID == mm2);
                    ViewBag.NgheMongMuon_22ID = NgheMongMuon_2ID == null ? 0 : mm2;
                    ViewBag.Tengnhemm22 = NgheMongMuon_2ID == null ? "Khong chon" : NgheMongMuon_2ID.TenNgheLaoDong;
                    //ViewBag.NgheMongMuon_2ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == model.NganhMongMuon_ID), "NgheLaoDong_ID", "TenNgheLaoDong", int.Parse(NganhNghe34[0]));
                }
                else
                {
                    ViewBag.NgheMongMuon_22ID = 0;
                    ViewBag.Tengnhemm22 = "Khong Chon";
                }
                if (NganhNghe34.Count() > 1)
                {
                    if (NganhNghe34[1] != "")
                    {
                        int mm3 = int.Parse(NganhNghe34[1]);

                        var NgheMongMuon_3ID = dbc.DM_NgheLaoDong.FirstOrDefault(kh => kh.NgheLaoDong_ID == mm3);
                        ViewBag.NgheMongMuon_33ID = NgheMongMuon_3ID == null ? 0 : mm3;
                        ViewBag.Tengnhemm33 = NgheMongMuon_3ID == null ? "Khong chon" : NgheMongMuon_3ID.TenNgheLaoDong;
                        //ViewBag.NgheMongMuon_3ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == model.NganhMongMuon_ID), "NgheLaoDong_ID", "TenNgheLaoDong", int.Parse(NganhNghe34[1]));
                    }
                    else
                    {
                        ViewBag.NgheMongMuon_33ID = 0;
                        ViewBag.Tengnhemm33 = "Khong Chon";
                    }
                }
                else
                {
                    ViewBag.NgheMongMuon_33ID = 0;
                    ViewBag.Tengnhemm33 = "Khong Chon";
                }

            }
            else
            {
                ViewBag.NgheMongMuon_22ID = 0;
                ViewBag.Tengnhemm22 = "Khong Chon";
                ViewBag.NgheMongMuon_33ID = 0;
                ViewBag.Tengnhemm33 = "Khong Chon";
            }
            //ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhKinhDoanh, "Nganh_ID", "TenNganhKD", model.NganhMongMuon_ID);
            //ViewBag.NgheMongMuon_ID = new SelectList(dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == model.NganhMongMuon_ID), "NgheKD_ID", "TenNgheKD", model.NgheMongMuon_ID);
            ViewBag.NoiLamViecMongMuon_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh), "Id", "TenDiaChi", model.NoiLamViecMongMuon_ID);


            return View("_CandidateEdit", model);
        }
        [ProtectNTV]
        [ValidateInput(false)]
        public ActionResult UpdateCanFile(KhachHang_TimViecLam model, string TenHoSo_kh, int dicongtac, int ThoiGianHoSoTonTai
            , int NgheMongMuon_2ID, int NgheMongMuon_3ID)
        {

            if (ModelState.IsValid)
            {
                model.NgayCapNhat = DateTime.Now;
                model.CoTheDiCongTac = dicongtac == 0 ? false : true;
                if (ThoiGianHoSoTonTai > 0)
                {
                    model.NgayHoSoHetHan = model.NgayCapNhat.Value.AddMonths(ThoiGianHoSoTonTai);
                }
                model.TenHoSo = TenHoSo_kh;
                model.NganhNghe34_2022 = NgheMongMuon_2ID.ToString() + "," + NgheMongMuon_3ID.ToString();
                //add file mới
                var file = Request.Files["CVdinhkem"];
                if (file.ContentLength > 0)
                {
                    //Xoas hinh cu
                    var tenhinhcu = XString.Cutstring(model.KhaNangNoiTroi);
                    bool xoahinh = XString.Xoahinhcu("Document", tenhinhcu);
                    //chep hinh moi
                    var ten = file.FileName;
                    var ext = ten.Substring(ten.LastIndexOf('.'));
                    ten = Guid.NewGuid() + ext;
                    model.KhaNangNoiTroi = ten;
                    file.SaveAs(XString.maplocal + "Document\\" + ten);

                    model.TyLeHoSoHoanThanh = 0;
                }

                dbc.Entry(model).State = System.Data.Entity.EntityState.Modified;

                dbc.SaveChanges();
                Session["ThongBao_KH_TimViec"] = "Update thành công hồ sơ: " + model.TenHoSo;
                return View("_MainCandidate");
            }
            else
            {
                ViewBag.ThoiGianLamViecMongMuon = new SelectList(dbc.DM_ThoiGianLamViec.ToList(), "ThoiGianLamViec_ID", "TenThoiGianLamViec", model.ThoiGianLamViecMongMuon);
                ViewBag.ChucDanhMongMuon = dbc.DM_ChucDanh.ToList();
                ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong", model.NganhMongMuon_ID);

                //ViewBag.NganhMongMuon_ID = new SelectList(dbc.DM_NganhKinhDoanh, "Nganh_ID", "TenNganhKD", model.NganhMongMuon_ID);
                //ViewBag.NgheMongMuon_ID = new SelectList(dbc.DM_NgheKinhDoanh.Where(kh => kh.Nhom_NganhKD_ID == model.NganhMongMuon_ID), "NgheKD_ID", "TenNgheKD", model.NgheMongMuon_ID);
                ViewBag.NgheMongMuon_ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == model.NganhMongMuon_ID), "NgheLaoDong_ID", "TenNgheLaoDong", model.NgheMongMuon_ID);
                ViewBag.NoiLamViecMongMuon_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh).ToList(), "Id", "TenDiaChi", model.NoiLamViecMongMuon_ID);
                var model1 = dbc.KhachHang_TimViecLam.Find(model.TimViec_ID);
                ModelState.AddModelError("", "Có Lỗi !!! ");
                return View("_CandidateEdit", model1);
            }
        }
        [ProtectNTV]
        public ActionResult _EditCandidateAccount()
        {
            //load trang NTV
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var user = dbc.UserWebs.SingleOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                var model = dbc.KhachHangs.Where(kh => kh.KH_ID == user.UserChild_id).Single();

                var modal_kh_nghiepvu3 = dbc.KhachHang_NghiepVu.Where(kh => kh.KH_ID == model.KH_ID)
                    .OrderBy(kh => kh.KHNghiepVu)
                    .Take(1)
                    .ToList();
                var modal_kh_nghiepvu22 = dbc.KhachHang_TimViec_NgoaiNgu_2022s.Where(kh => kh.KH_ID == model.KH_ID)
                    .OrderBy(kh => kh.KhachHang_TimViec_NgoaiNgu_ID)
                    .Skip(1)
                    .Take(1)
                    .ToList();
                var modal_kh_nghiepvu2 = dbc.KhachHang_TimViec_NgoaiNgu_2022s.Where(kh => kh.KH_ID == model.KH_ID)
                    .OrderBy(kh => kh.KhachHang_TimViec_NgoaiNgu_ID)
                    .Take(1)
                    .ToList();
                var modal_kh_nghiepvu11 = dbc.KhachHang_TimViec_TinHocs.Where(kh => kh.KH_ID == model.KH_ID)
                    .OrderBy(kh => kh.NghiepVu_TinHoc_ID)
                    .Skip(1)
                    .Take(1)
                    .ToList();
                var modal_kh_nghiepvu1 = dbc.KhachHang_TimViec_TinHocs.Where(kh => kh.KH_ID == model.KH_ID)
                    .OrderBy(kh => kh.NghiepVu_TinHoc_ID)
                    .Take(1)
                    .ToList();



                if (modal_kh_nghiepvu3.Count != 0)
                {
                    ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_kh_nghiepvu3.First().NghiepVu_ID);
                }
                else { ViewBag.NghiepVu3 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "3").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }

                if (modal_kh_nghiepvu2.Count != 0)
                {
                    ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_kh_nghiepvu2.First().NghiepVu_NgoaiNgu_ID);
                }
                else { ViewBag.NghiepVu2 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }

                if (modal_kh_nghiepvu22.Count != 0)
                {
                    ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_kh_nghiepvu22.First().NghiepVu_NgoaiNgu_ID);
                }
                else { ViewBag.NghiepVu22 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "2").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }

                if (modal_kh_nghiepvu1.Count != 0)
                {
                    ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_kh_nghiepvu1.First().NghiepVu_TinHoc_ID);
                }
                else { ViewBag.NghiepVu1 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }
                if (modal_kh_nghiepvu11.Count != 0)
                {
                    ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu", modal_kh_nghiepvu11.First().NghiepVu_TinHoc_ID);
                }
                else { ViewBag.NghiepVu11 = new SelectList(dbc.DM_NghiepVu.Where(kh => kh.GhiChu == "0" || kh.GhiChu == "1").OrderBy(kh => kh.NghiepVu_ID), "NghiepVu_ID", "TenNghiepVu"); }

                ViewBag.HocVan_ID = dbc.DM_HocVan.ToList().OrderBy(kh => kh.HocVan_ID);
                ViewBag.NoiCap_ID = dbc.DM_DiaChi.ToList().Where(nh => nh.ParentId == 0);

                ViewBag.TamTru_Tinh_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.Id == TT_Tinh), "Id", "TenDiaChi");
                ViewBag.TamTru_Huyen_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == TT_Tinh), "Id", "TenDiaChi", model.TamTru_Huyen_ID);
                ViewBag.TamTru_Xa_ID = new SelectList(dbc.DM_DiaChi.Where(nh => nh.ParentId == model.TamTru_Huyen_ID), "Id", "TenDiaChi", model.TamTru_Xa_ID);

                var model2 = dbc.KhachHang_TrinhDo.Where(p => p.KH_ID == user.UserChild_id).SingleOrDefault();
                if (model2 != null)
                {
                    ViewBag.Nganh_ID = model2;
                    int NganhHienTai = model2.Nganh_ID;
                    int TrinhDoHienTai = model2.TrinhDo_ID;

                    ViewBag.KHTrinhDo = model2.KHTrinhDo;
                    ViewBag.Nganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong", NganhHienTai);
                    ViewBag.Nghe_ID = new SelectList(dbc.DM_NgheLaoDong.Where(kh => kh.NhomNganhLaoDong == NganhHienTai), "NgheLaoDong_ID", "TenNgheLaoDong", model2.Nghe_ID);
                    ViewBag.TrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList(), "TrinhDoChuyenMon_ID", "TenChuyenMon", TrinhDoHienTai);
                    return View("_EditCandidateAccount", model);
                }
                else
                {
                    ViewBag.Nganh_ID = new SelectList(dbc.DM_NganhLaoDong.ToList(), "NganhLaoDong_ID", "TenNganhLaoDong");
                    ViewBag.Nghe_ID = new SelectList(dbc.DM_NgheLaoDong.ToList(), "NgheLaoDong_ID", "TenNgheLaoDong");
                    ViewBag.TrinhDo_ID = new SelectList(dbc.DM_TrinhDoChuyenMon.ToList(), "TrinhDoChuyenMon_ID", "TenChuyenMon");
                    return View("_EditCandidateAccount", model);
                }
            }
            else return PartialView("_Login");

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _EditCandidateAccount(KhachHang kh_td, string HoTen_kh, string DienThoai_kh, string Email_kh,
            int KH_ID, int TrinhDo_ID, int Nganh_ID, int Nghe_ID, int NghiepVu1, int NghiepVu11, int NghiepVu2
            , int NghiepVu22, int NghiepVu3)
        {
            //int KH_ID, string HoTen ,DateTime NgaySinh,int GioiTinh ,string DienThoai, 
            //string CMND,int NoiCap_ID, DateTime NgayCap, int TamTru_Tinh_ID, int TamTru_Huyen_ID, int TamTru_Xa_ID,int TamTru_DiaChi
            KhachHang model = new KhachHang();
            model = kh_td;
            var file = Request.Files["HinhDaiDien"];
            if (file.ContentLength > 0)
            {
                //Xoas hinh cu
                var tenhinhcu = XString.Cutstring(model.Hinh);
                bool xoahinh = XString.Xoahinhcu("Avatar_UV", tenhinhcu);
                //chep hinh moi
                var ten = file.FileName;
                var ext = ten.Substring(ten.LastIndexOf('.'));
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                {
                    ten = Guid.NewGuid() + ext;
                    model.Hinh = "AnhDaiDien/" + ten;
                    //Thu nho hinh anh qua to
                    WebImage img = new WebImage(file.InputStream);
                    if (img.Width > 300)
                        img.Resize(300, 350);
                    img.Save(XString.maplocal + "Avatar_UV\\" + ten);
                }
            }
            model.HoTen = HoTen_kh;
            model.DienThoai = DienThoai_kh;
            model.Email = Email_kh;
            model.NgayCapNhat = DateTime.Now;
            model.NguoiCapNhat = kh_td.KH_ID;
            model.HocVan_ID = kh_td.HocVan_ID;

            dbc.Entry(model).State = System.Data.Entity.EntityState.Modified;
            dbc.SaveChanges();

            var UserW = dbc.UserWebs.Find(int.Parse(Session["UsrID"].ToString()));
            if (UserW.EmailConnection != Email_kh)
            {
                UserW.EmailConnection = Email_kh;
                dbc.Entry(UserW).State = System.Data.Entity.EntityState.Modified;
                dbc.SaveChanges();
            }
            //ghi bảng KhachHang_TrinhDo
            var trinhdo = new DAO.Khachhang_trinhdo_nghiepvu_DAO().Xoa_InsertKhachHang_trinhdo(
                TrinhDo_ID, Nganh_ID, Nghe_ID, model.KH_ID, UserW.UserID);

            //ghi bảng nghiệp vụ
            var kqnv = new DAO.Khachhang_trinhdo_nghiepvu_DAO().Xoa_InsertKhachHang_NN_TH_NV(
                NghiepVu1, NghiepVu11, NghiepVu2, NghiepVu22, NghiepVu3, model.KH_ID, UserW.UserID);
            Session["ThongBao_KH_TimViec"] = "Update thành công hồ sơ bản thân bạn.";
            return PartialView("_MainCandidate");
        }
        [ProtectNTV]
        public ActionResult DeleteCanDoc(int Id)
        {
            var model = dbc.KhachHang_TimViecLam.Find(Id);
            if (model != null)
            {
                try
                {
                    dbc.KhachHang_TimViecLam.Remove(model);
                    dbc.SaveChanges();
                    Session["ThongBao_KH_TimViec"] = "Delete thành công hồ sơ: " + model.TenHoSo;

                    return View("_MainCandidate");
                }
                catch
                {
                    ModelState.AddModelError("", "Xóa Lỗi !!!");
                    return View("_MainCandidate");
                }
            }
            return View("_MainCandidate");

        }
        [ProtectNTV]
        public ActionResult DeleteCVNTV(int Id)
        {
            var model = dbc.KhachHang_KinhNghiem_LamViec_2022s.Find(Id);
            if (model != null)
            {
                try
                {
                    dbc.KhachHang_KinhNghiem_LamViec_2022s.Remove(model);
                    dbc.SaveChanges();
                    Session["ThongBao_KH_CV"] = "Delete thành công KN làm việc tại cty: " + model.TenCongTy;
                    return View("_MainCandidate");
                }
                catch
                {
                    Session["ThongBao_KH_CV"] = "Có Lỗi Xóa CV!!!";
                    return View("_MainCandidate");
                }
            }
            return View("_MainCandidate");

        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        public ActionResult thaydoitrangthai_kh(int tuyendungid)
        {
            var timviec = dbc.KhachHang_TimViecLam.FirstOrDefault(kh => kh.TimViec_ID == tuyendungid);
            Boolean hienthi = timviec.HienThiTrenWeb == true ? false : true;
            var update = dbc.Database.ExecuteSqlCommand("update VLDB.dbo.[KhachHang_TimViecLam] set HienThiTrenWeb=@HienThiTrenWeb where TimViec_ID=@Id",
                new SqlParameter("@HienThiTrenWeb", hienthi),
                new SqlParameter("@Id", tuyendungid));
            return Json("oooook");
        }
        public ActionResult _CandidateManagement()
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var KH_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);

                ViewBag.VLPhuHop = new DN_HoSoTuyenDung_Dao().GetmapTDbyTieudeTVL(KH_ID.UserChild_id, (bool)Session["Pay"]);

                ViewBag.HSDaLuu = dbc.Database.SqlQuery<PHU_HSTD_Luu>("select * from [VLDB].[dbo].[PHU_HSTD_Luu] where UserId =" + uID
                                            + " order by NgayTao desc");
                ViewBag.HSungtuyen = DAO.DN_UngTuyen_Dao.GetListUngTuyenByNTV(dbc, KH_ID.UserChild_id);
                return PartialView();
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }
        [ProtectUser]
        public ActionResult Logoff()
        {
            if (Session.Count > 0)
            {
                if (Session["quyen"] != null || Session["quyen"].ToString() != "")
                {
                    Session.Remove("User");
                    Session.Remove("quyen");
                    Session.Remove("UsrID");
                    Session.Remove("emailFace");
                    Session.Remove("Maunen");
                    //Session.Remove("SanMoi");
                    Session.Remove("Thongbaodangky");
                    Session.Remove("User_re");
                    Session.Remove("ThongBaoMau28");
                    Session.Remove("ThongBaoNew");
                    //REMOVE SESSION NHỰT - PHẦN TÌM VIỆC/ TUYỂN DỤNG NHANH
                    Session.Remove("NTV_ID");
                    Session.Remove("TenNTV");
                    Session.Remove("NgaySinhNTV");
                    Session.Remove("DiaChiNTV");
                    Session.Remove("DienThoaiNTV");
                    Session.Remove("DN_ID");
                    Session.Remove("TenDN");
                    Session.Remove("DiaChiDN");
                    Session.Remove("DienThoaiDN");
                    /////////////
                    Session.Remove("ThongBao_DN_TD");
                    Session.Remove("ThongBao_KH_TimViec");
                }
                else
                {
                    var requestUri = Session["requestUri"] as string;
                    if (requestUri != null)
                    {
                        return Redirect(requestUri);
                    }
                    else { return RedirectToAction("Index", "Home"); }
                }
            }
            HttpContext.Application["UserVisitor"] = (int)HttpContext.Application["UserVisitor"] - 1;//trừ số lượng đã logout
            return RedirectToAction("Index", "Home");
        }
        //login Facebook *************************************************
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult loginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"

            });
            return Redirect(loginUrl.AbsoluteUri);
            //return Redirect(loginUrl.AbsolutePath);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],

                redirect_uri = RedirectUri.AbsoluteUri,
                code = code

            });
            var accessToken = result.access_token;
            Session["accessToken"] = accessToken;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=link,first_name,middle_name,last_name,id,email");
                string email = me.email;
                string username = me.id;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                string first_name = me.first_name;
                string id = me.id;
                //FormsAuthentication.SetAuthCookie(email, false);

                // kiem tra tồn tại user face không phân biệt hoa thường.
                TaiKhoanInfo tk = new TaiKhoanInfo();
                Session["emailFace"] = first_name + " " + middlename + " " + lastname;
                Session["User"] = username;
                var user = dbc.UserWebs.Where(kh => kh.UserName.ToLower() == username.ToLower()).ToList();
                if (user.Count() == 0)
                {
                    //lưu thông tin user vào session
                    string[] user_regiter = new string[4];
                    user_regiter[0] = username;
                    user_regiter[1] = "123456789";
                    user_regiter[2] = email;
                    user_regiter[3] = email;
                    Session["User_re"] = user_regiter;
                    //Tạo User
                    return Redirect("_RegisterEX");
                }
                else if (user.Count() == 1)
                {
                    var userFa = dbc.UserWebs.Where(kh => kh.UserName == username).Single();
                    string[] user_log = new string[4];
                    user_log[0] = username;
                    user_log[1] = "123456789";
                    user_log[2] = email;
                    user_log[3] = email;
                    //gán session
                    //var uID = dbc.Database.SqlQuery<int>("select [UserID] from [VLVN].[dbo].[Users] where [Username]='" + username + "'");
                    var uID = userFa.UserID;
                    Session["UsrID"] = uID;
                    //kiểm tra quyền
                    if (userFa.UserRoles_NVLoaitaikhoan == 3)
                    {
                        Session["quyen"] = "NTV";
                    }
                    else
                    { Session["quyen"] = "TD"; }
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi trùng UserName !!!");
                    return PartialView("_Register");
                }
            }
            //var cookie2 = Request.Cookies[Session["User"].ToString()];
            //if (cookie2 != null)
            //{
            //    Session["Maunen"] = cookie2.Values["Mau"];
            //}
            //mơi 14/08/2019 *******************************************************
            FormsAuthentication.RedirectFromLoginPage(Session["UsrID"].ToString(), true);
            //tro lai trang truoc do
            var requestUri = Session["requestUri"] as string;
            if (requestUri != null)
            {
                return Redirect(requestUri);
            }
            else { return RedirectToAction("Index", "Home"); }
        }
        public ActionResult _RegisterEX()
        {
            //lấy ra email face
            if (Session["User_re"] != null)
            {
                var user_regiter = new string[3];
                user_regiter = Session["User_re"] as string[];
                ViewBag.Email = user_regiter[3].ToString();
            }
            else
            {
                return View("_Register");
            }
            return View("_RegisterEX");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _RegisterEX(string captcha, string optradio)
        {
            if (Session["User_re"] != null)
            {
                if (captcha != null)
                {
                    //kiểm tra captcha trước
                    string getcaptcha = Session["captchar"].ToString();
                    if (captcha == getcaptcha)
                    {
                        if (optradio == "ntv")
                        {
                            return Redirect("_CandidateAccount");
                        }
                        else return Redirect("_JobAccount");

                    }
                    else ViewData["captcha"] = "Điền chính xác dòng mã này!!";
                }
            }
            else
            {
                return PartialView("_Register");
            }
            return PartialView("_RegisterEX");
        }
        //login Facebook *************************************************
        [ProtectTD]
        public ActionResult DN_tuchoiungvien(int KH_ID = 0, int tuyendung_ID = 0, int tuchoi = 2)
        {
            string uttuchoi = "NO";
            if (tuchoi == 1)
            {
                uttuchoi = "Từ chối";
            }
            var item = dbc.DoanhNghiep_UngTuyens.FirstOrDefault(kh => kh.TuyenDung_ID == tuyendung_ID && kh.KH_ID == KH_ID);
            if (item != null)
            {
                item.NgayUpdate = DateTime.Now;
                item.NTV_LyDoHuy = "NO";
                item.DN_TuChoi = uttuchoi;
                item.DN_Daxem = true;
                var kq = DAO.DN_UngTuyen_Dao.Update_DoanhNghiep_UngTuyen(dbc, item);
                if (kq)
                {
                    return Json("OK");
                }
                else return Json("Error");
            }
            return Json("Error");
        }
        public ActionResult ungvien_tuchoiDN(int KH_ID = 0, int tuyendung_ID = 0, int TrangThaiUngTuyenNTV = 0)
        {
            var item = dbc.DoanhNghiep_UngTuyens.FirstOrDefault(kh => kh.TuyenDung_ID == tuyendung_ID && kh.KH_ID == KH_ID);
            if (item != null)
            {
                item.NgayUpdate = DateTime.Now;

                if (TrangThaiUngTuyenNTV == 0)
                {
                    item.smsNTVtoDN = "Tôi muốn ngưng ứng tuyển.";
                    item.NTV_LyDoHuy = "Hủy";
                    item.NTV_TrangThaiUngTuyen = false;
                }
                else
                {
                    item.smsNTVtoDN = "Tôi muốn ứng tuyển lại vị trí này.";
                    item.NTV_LyDoHuy = "NO";
                    item.NTV_TrangThaiUngTuyen = true;
                }
                var kq = DAO.DN_UngTuyen_Dao.Update_DoanhNghiep_UngTuyen(dbc, item);
                if (kq)
                {
                    return Json("OK");
                }
                else return Json("Error");
            }

            return Json("Error");
        }
        [ProtectNTV]
        public ActionResult ThemKinhNghiem()
        {

            return PartialView("_ThemKinhNghiem");
        }
        [ProtectNTV]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemKinhNghiem(String TenCongTy, String ChucVu, DateTime TuThangCV, DateTime DenThangCV, String CongViecChinh, String DaLamViecNuocNgoai)
        {
            if (Session["UsrID"] != null)
            {
                int uID = int.Parse(Session["UsrID"].ToString());
                var KH_ID = dbc.UserWebs.FirstOrDefault(kh => kh.UserID == uID && kh.UserRoles_NVLoaitaikhoan == 3);
                KhachHang_KinhNghiem_LamViec_2022 model = new KhachHang_KinhNghiem_LamViec_2022();
                model.KH_ID = KH_ID.UserChild_id;
                model.TenCongTy = TenCongTy;
                model.ChucVu = ChucVu;
                string th = TuThangCV.Month + "/" + TuThangCV.Year;
                model.TuThang = th;
                string dt = DenThangCV.Month + "/" + DenThangCV.Year;
                model.DenThang = dt;
                model.CongViecChinh = CongViecChinh;
                model.DaLamViecNuocNgoai = DaLamViecNuocNgoai;
                model.NgayTao = DateTime.Now;
                model.NguoiTao = uID;
                dbc.KhachHang_KinhNghiem_LamViec_2022s.Add(model);
                int kt = dbc.SaveChanges();
                if (kt != 0)
                {
                    Session["ThongBao_KH_CV"] = "Thêm thành công kn bản thân.";
                }
                return RedirectToAction("_MainCandidate", "Account");
            }


            return PartialView("_Login");
        }
        public ActionResult _RegisterEmail()
        {
            Session.Remove("RanDomOTP");
            
            return View("_RegisterEmail");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _RegisterEmail(string Email, string Password, string PasswordSec, string captcha, string optradio)
        {
            string otp = GetRanDomOTP();
            if (Email != "")
            {
                var user = dbc.UserWebs.Where(p => p.UserName.ToLower() == Email.ToLower()).ToList();
                if (Password != PasswordSec)
                {
                    ViewData["Emailxt"] = "Xác nhận mật khẩu không thành công.";
                }
                else
                {
                    if (user.Count == 0)
                    {
                        if (captcha != null)
                        {
                            try
                            {
                                //kiểm tra captcha trước
                                string getcaptcha = Session["captchar"].ToString();
                                if (captcha == getcaptcha)
                                {
                                    //lưu thông tin user vào session
                                    string[] user_regiter = new string[4];
                                    user_regiter[0] = Email;
                                    user_regiter[1] = Password;
                                    user_regiter[2] = optradio;
                                    user_regiter[3] = Email;
                                    Session["User_re"] = user_regiter;
                                    //Session["Emailname"] = user_regiter[0].ToString();
                                    //Gửi OTP đến email khách hàng
                                    var kq = Mailer.Send(Email, "Gửi OTP", "Mã xác nhận bạn là: " + Session["RanDomOTP"].ToString());
                                    if (kq)
                                    {
                                        return RedirectToAction("_RegisterOTP");
                                    }
                                    else
                                    {
                                        Session["ThongbaodangkyEmail"] = "Thông báo, Có Lỗi khi gửi OTP đến email !!!";
                                        return View("_Register");
                                    }


                                }
                                else ViewData["captcha"] = "Điền chính xác dòng mã này!!";
                            }
                            catch
                            {
                                Session["ThongbaodangkyEmail"] = "Thông báo, đã hết thời gian chờ đăng ký, mời bạn đăng ký lại !!!";
                                return View("_Register");
                            }
                        }
                    }
                    else
                    {
                        ViewData["Emailxt"] = "Email này đã được sử dụng, chọn gmail khác.";
                    }
                }
                
            }
            else
            {
                ViewData["Emailxt"] = "Điền chính xác email để lấy mã.";
            }

            return View("_RegisterEmail");
        }
        public ActionResult _RegisterOTP()
        {
            if (Session["User_re"] != null)
            {
                var user_regiter = new string[3];
                user_regiter = Session["User_re"] as string[];
                ViewBag.Emailname = user_regiter[0].ToString();
            }
            else
            {
                Session["ThongbaodangkyEmail"] = "Thông báo, đã hết thời gian chờ đăng ký, mời bạn đăng ký lại !!!";
                return View("_Register");
            }
            return View("_RegisterOTP");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _RegisterOTP(string maOTP)
        {
            if(Session["RanDomOTP"] != null)
            {
                string getOTP = Session["RanDomOTP"].ToString();
                if (maOTP == getOTP)
                {
                    if (Session["User_re"] != null)
                    {
                        var user_regiter = new string[3];
                        user_regiter = Session["User_re"] as string[];

                        string optradio = user_regiter[2].ToString();
                        if (optradio == "ntv")
                        {
                            return Redirect("_CandidateAccount");
                        }
                        else return Redirect("_JobAccount");
                    }
                    else
                    {
                        Session["ThongbaodangkyEmail"] = "Thông báo, đã hết thời gian chờ đăng ký, mời bạn đăng ký lại !!!";
                        return View("_Register");
                    }

                }
                else
                {
                    Session["ThongbaodangkyEmail"] = "Thông báo, Bạn đã nhập sai OTP, mời bạn đăng ký lại !!!";
                    return View("_Register");
                }
            }
            else
            {
                return View("_Register");
            }
            
        }
        public string GetRanDomOTP()
        {
            //get Random text
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679012345679";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            Session["RanDomOTP"] = randomText.ToString();

            string text = randomText.ToString();
            return text;
        }
    }
}