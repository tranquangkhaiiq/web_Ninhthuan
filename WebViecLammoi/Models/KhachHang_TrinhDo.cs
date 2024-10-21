namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_TrinhDo
    {
        [Key]
        public int KHTrinhDo { get; set; }

        public int KH_ID { get; set; }

        public int TrinhDo_ID { get; set; }

        public int Nganh_ID { get; set; }

        public int Nghe_ID { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
        [ForeignKey("TrinhDo_ID")]
        public virtual DM_TrinhDoChuyenMon DM_TrinhDoChuyenMons { get; set; }
        [ForeignKey("Nganh_ID")]
        public virtual DM_NganhLaoDong DM_NganhLaoDongs { get; set; }
        [ForeignKey("Nghe_ID")]
        public virtual DM_NgheLaoDong DM_NgheLaoDongs { get; set; }
    }
}
