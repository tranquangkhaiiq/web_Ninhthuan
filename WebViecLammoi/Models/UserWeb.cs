namespace WebViecLammoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserWeb")]
    public partial class UserWeb
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordSalt { get; set; }

        public int UserChild_id { get; set; }

        public int UserRoles_NVLoaitaikhoan { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        [Required]
        [StringLength(256)]
        public string EmailConnection { get; set; }

        public bool IsLocked { get; set; }

        public DateTime LastLockedChangedDate { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Note { get; set; }
    }
}
