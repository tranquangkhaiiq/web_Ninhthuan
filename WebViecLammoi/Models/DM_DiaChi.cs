namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_DiaChi
    {
        public DM_DiaChi() {
            this.KhachHangs = new HashSet<KhachHang>();
            this.DoanhNghieps = new HashSet<DoanhNghiep>();
            this.KhachHang_TimViecLams = new HashSet<KhachHang_TimViecLam>();
        }
        public int Id { get; set; }

        [StringLength(2000)]
        public string TenDiaChi { get; set; }

        public string MoTa { get; set; }

        public int? ParentId { get; set; }

        public int? MaQuocGia { get; set; }

        public int? Level { get; set; }

        public DateTime? NgayTao { get; set; }

        public bool? KichHoat { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        public virtual ICollection<DoanhNghiep> DoanhNghieps { get; set; }
        public virtual ICollection<KhachHang_TimViecLam> KhachHang_TimViecLams { get; set; }
    }
}
