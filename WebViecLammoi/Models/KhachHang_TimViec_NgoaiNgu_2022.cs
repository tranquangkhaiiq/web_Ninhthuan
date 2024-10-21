using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebViecLammoi.Models
{
    public partial class KhachHang_TimViec_NgoaiNgu_2022
    {
        [Key]
        public int KhachHang_TimViec_NgoaiNgu_ID { get; set; }
        public int KH_ID {  get; set; }
        public int NghiepVu_NgoaiNgu_ID { get; set; }
        [StringLength(500)]
        public string ChungChiNgoaiNgu {  get; set; }
        [StringLength(500)]
        public string KhaNangSuDung {  get; set; }
        [StringLength(500)]
        public string GhiChu {  get; set; }
        public DateTime NgayTao {  get; set; }
        public int NguoiTao {  get; set; }
        [ForeignKey("NghiepVu_NgoaiNgu_ID")]
        public virtual DM_NghiepVu DM_NghiepVus { get; set; }

    }
}