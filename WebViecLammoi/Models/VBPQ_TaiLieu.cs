using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebViecLammoi.Models
{
    public partial class VBPQ_TaiLieu
    {
        public int Id { get; set; }

        [StringLength(400)]
        public string Files { get; set; }

        public int LoaiTaiLieuId { get; set; }

        public int DonViId { get; set; }

        [StringLength(400)]
        public string SoHieu { get; set; }

        [StringLength(400)]
        public string SoVanban { get; set; }

        [StringLength(400)]
        public string NguoiKy { get; set; }

        public DateTime? NgayKy { get; set; }

        public DateTime? NgayBanHanh { get; set; }

        public string TrichYeu { get; set; }

        public DateTime NgayTao { get; set; }

        public int NguoiNhap { get; set; }

        public DateTime NgayNhap { get; set; }

        public bool IsActive { get; set; }

        public int OrderNumber { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public int PortalId { get; set; }

        public int ModuleId { get; set; }

        [StringLength(50)]
        public string nguonchich { get; set; }

        [StringLength(50)]
        public string kyhieukho { get; set; }

        public Guid msrepl_tran_version { get; set; }

        [ForeignKey("DonViId")]
        public virtual VBPQ_DonVi VBPQ_DonVis { get; set; }
        [ForeignKey("LoaiTaiLieuId")]
        public virtual VBPQ_LoaiTaiLieu VBPQ_LoaiTaiLieus { get; set; }
    }
}