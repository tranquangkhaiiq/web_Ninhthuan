using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebViecLammoi.Models
{
    public partial class DM_TinhTrangPheDuyetHoSo
    {
        public DM_TinhTrangPheDuyetHoSo()
        {
            this.DoanhNghiep_TuyenDungs = new HashSet<DoanhNghiep_TuyenDung>();
            this.KhachHang_TimViecLams = new HashSet<KhachHang_TimViecLam>();
        }
        [Key]
        public int TinhTrangHoSo_ID { get; set; }

        [StringLength(50)]
        public string TinhTrangHoSo_Ten { get; set; }

        public bool? TinhTrangHoSo_KichHoat { get; set; }

        public DateTime? TinhTrangHoSo_NgayTao { get; set; }

        [StringLength(50)]
        public string TinhTrangHoSo_NguoiTao { get; set; }

        public virtual ICollection<DoanhNghiep_TuyenDung> DoanhNghiep_TuyenDungs { get; set; }
        public virtual ICollection<KhachHang_TimViecLam> KhachHang_TimViecLams { get; set; }
    }
}