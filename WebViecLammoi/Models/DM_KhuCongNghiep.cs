namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DM_KhuCongNghiep
    {
        public DM_KhuCongNghiep() {
            this.DoanhNghieps = new HashSet<DoanhNghiep>();
        }
        [Key]
        public int KhuCongNghiep_ID { get; set; }

        [Required]
        [StringLength(2000)]
        public string TenKhuCongNghiep { get; set; }

        public DateTime NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public bool? KichHoat { get; set; }
        public virtual ICollection<DoanhNghiep> DoanhNghieps { get; set; }
    }
}
