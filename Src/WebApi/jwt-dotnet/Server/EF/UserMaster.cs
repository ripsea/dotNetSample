using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.EF
{
    [Table("JWT_UserMaster")]
    public class UserMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Column("UserName", TypeName = "nvarchar")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Column("UserPassword", TypeName = "nvarchar")]
        [StringLength(50)]
        public string UserPassword { get; set; }
        [Column("UserRoles", TypeName = "nvarchar")]
        [StringLength(500)]
        public string UserRoles { get; set; }
        [Column("UserEmailID", TypeName = "nvarchar")]
        [StringLength(100)]
        public string UserEmailID { get; set; }

        //chain methods
        public UserMaster SetUserName(string UserName)
        {
            this.UserName = UserName;
            return this;
        }

        public UserMaster SetUserPassword(string UserPassword)
        {
            this.UserPassword = UserPassword;
            return this;
        }

        public UserMaster SetUserRoles(string userRoles)
        {
            this.UserRoles = userRoles;
            return this;
        }

        public UserMaster SetUserEmailID(string UserEmailID)
        {
            this.UserEmailID = UserEmailID;
            return this;
        }

        /*
        public JWT_UserMaster(int UserID_, string UserName_, string UserPassword_, string UserRoles_, string UserEmailID_)
        {
            this.UserID = UserID_;
            this.UserName = UserName_;
            this.UserPassword = UserPassword_;
            this.UserRoles = UserRoles_;
            this.UserEmailID = UserEmailID_;
        }
        */

    }

}