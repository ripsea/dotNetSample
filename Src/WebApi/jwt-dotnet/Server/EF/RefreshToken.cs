using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.EF
{
    [Table("JWT_RefreshToken")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("UserName", TypeName = "nvarchar")]
        [StringLength(500)]
        public string UserName { get; set; }
        [Column("ClientID", TypeName = "varchar")]
        [StringLength(500)]
        public string ClientID { get; set; }
        [Column("IssuedTime", TypeName = "datetime")]
        public DateTime IssuedTime { get; set; }
        [Column("ExpiredTime", TypeName = "datetime")]
        public DateTime ExpiredTime { get; set; }
        [Column("ProtectedTicket", TypeName = "nvarchar")]
        [StringLength(500)]
        public string ProtectedTicket { get; set; }

        public RefreshToken(string userName, string clientID, DateTime issuedTime, DateTime expiredTime, string protectedTicket)
        {
            UserName = userName;
            ClientID = clientID;
            IssuedTime = issuedTime;
            ExpiredTime = expiredTime;
            ProtectedTicket = protectedTicket;
        }
    }
}