using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebViecLammoi.Models
{
    public partial class KhachHang_KinhNghiem_LamViec_2022
    {
        [Key]
        public int KhachHang_KinhNghiem_LamViec_ID { get; set; }
        public int KH_ID {  get; set; }
        [StringLength(2000)]
        public string TenCongTy {  get; set; }
        [StringLength(2000)]
        public string ChucVu {  get; set; }
        [StringLength(8)]
        public string TuThang {  get; set; }
        [StringLength(8)]
        public string DenThang { get; set; }
        public string CongViecChinh { get; set; }
        public string DaLamViecNuocNgoai { get; set; }
        public DateTime NgayTao {  get; set; }
        public int NguoiTao {  get; set; }
    }
}