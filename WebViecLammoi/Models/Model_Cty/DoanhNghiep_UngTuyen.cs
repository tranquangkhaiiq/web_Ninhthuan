namespace WebViecLammoi.Models.Model_Cty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoanhNghiep_UngTuyen
    {
        public Guid Id { get; set; }

        public int TuyenDung_ID { get; set; }

        public int KH_ID { get; set; }

        [StringLength(500)]
        public string smsNTVtoDN { get; set; }

        public bool? DN_Daxem { get; set; }

        public bool? DN_LayTTLienHe { get; set; }

        public DateTime? NgayUngTuyen { get; set; }

        public bool? NTV_TrangThaiUngTuyen { get; set; }

        public DateTime? NgayUpdate { get; set; }

        [StringLength(500)]
        public string NTV_LyDoHuy { get; set; }

        [StringLength(500)]
        public string File_CVUngTuyen { get; set; }

        [StringLength(20)]
        public string DN_TuChoi { get; set; }
    }
}
