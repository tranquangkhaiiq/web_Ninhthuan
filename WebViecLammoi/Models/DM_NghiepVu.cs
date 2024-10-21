namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_NghiepVu
    {
        public DM_NghiepVu()
        {
            this.DoanhNghiep_TuyenDung_NghiepVus = new HashSet<DoanhNghiep_TuyenDung_NghiepVu>();
            this.KhachHang_NghiepVus = new HashSet<KhachHang_NghiepVu>();
            this.DoanhNghiep_TuyenDung_NgoaiNgus = new HashSet<DoanhNghiep_TuyenDung_NgoaiNgu>();
            this.DoanhNghiep_TuyenDung_TinHocs = new HashSet<DoanhNghiep_TuyenDung_TinHoc>();
            this.KhachHang_TimViec_TinHocs = new HashSet<KhachHang_TimViec_TinHoc>();
            this.KhachHang_TimViec_NgoaiNgu_2022s = new HashSet<KhachHang_TimViec_NgoaiNgu_2022>();
        }
        [Key]
        public int NghiepVu_ID { get; set; }

        [StringLength(500)]
        public string TenNghiepVu { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTao { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
        public virtual ICollection<DoanhNghiep_TuyenDung_NghiepVu> DoanhNghiep_TuyenDung_NghiepVus { get; set; }
        public virtual ICollection<KhachHang_NghiepVu> KhachHang_NghiepVus { get; set; }
        public virtual ICollection<DoanhNghiep_TuyenDung_NgoaiNgu>DoanhNghiep_TuyenDung_NgoaiNgus{ get; set; }
        public virtual ICollection<DoanhNghiep_TuyenDung_TinHoc>DoanhNghiep_TuyenDung_TinHocs { get; set; }
        public virtual ICollection<KhachHang_TimViec_TinHoc>KhachHang_TimViec_TinHocs { get; set; }
        public virtual ICollection<KhachHang_TimViec_NgoaiNgu_2022> KhachHang_TimViec_NgoaiNgu_2022s{ get; set; }
    }
}
