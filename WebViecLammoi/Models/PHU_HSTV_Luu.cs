namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PHU_HSTV_Luu
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int HSTV_Id { get; set; }

        public int NTV_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }

        [Required]
        public string TenHoSo { get; set; }
    }
}
