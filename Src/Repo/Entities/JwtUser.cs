using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.EF
{
    [Table("JwtUser")]
    public class JwtUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("Name", TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("Password", TypeName = "nvarchar")]
        [StringLength(50)]
        public string Password { get; set; }
        [Column("Roles", TypeName = "nvarchar")]
        [StringLength(500)]
        public string Roles { get; set; }
        [Column("EmailID", TypeName = "nvarchar")]
        [StringLength(100)]
        public string EmailID { get; set; }

        //chain methods
        public JwtUser SetUserName(string UserName)
        {
            this.Name = UserName;
            return this;
        }

        public JwtUser SetUserPassword(string UserPassword)
        {
            this.Password = UserPassword;
            return this;
        }

        public JwtUser SetUserRoles(string userRoles)
        {
            this.Roles = userRoles;
            return this;
        }

        public JwtUser SetUserEmailID(string UserEmailID)
        {
            this.EmailID = UserEmailID;
            return this;
        }


    }

}