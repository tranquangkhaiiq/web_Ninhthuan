namespace WebViecLammoi.Models.Model_Cty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pay_Sys
    {
        public int Id { get; set; }

        public bool Pay { get; set; }

        [StringLength(200)]
        public string GiaiThich { get; set; }
        public DateTime Ngay { get; set; }
    }
}
