namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KhachHang_TimViecLam_FileDinhKem
    {
        [Key]
        public int HSTV_FileDinhKemID { get; set; }

        public int? HSTV_ID { get; set; }

        [StringLength(100)]
        public string TenFile { get; set; }

        public string DuongDan { get; set; }

        public bool? ChoPhepTaiVe { get; set; }

        public bool? ChoPhepHienThi { get; set; }

        [StringLength(50)]
        public string NguoiUpload { get; set; }

        public DateTime? NgayUpload { get; set; }
    }
}
