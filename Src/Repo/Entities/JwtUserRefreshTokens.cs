using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.EF
{
    [Table("JwtUserRefreshTokens")]
    public class JwtUserRefreshTokens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column("UserName", TypeName = "nvarchar")]
        [StringLength(500)]
        public string UserName { get; set; }
        [Column("RefreshToken", TypeName = "varchar")]
        [StringLength(500)]
        public string RefreshToken { get; set; }
        [Column("IssuedTime", TypeName = "datetime")]
        public DateTime IssuedTime { get; set; }
        [Column("ExpiredTime", TypeName = "datetime")]
        public DateTime ExpiredTime { get; set; }

        public JwtUserRefreshTokens(string userName, string refreshToken, 
            DateTime issuedTime, DateTime expiredTime)
        {
            UserName = userName;
            RefreshToken = refreshToken;
            IssuedTime = issuedTime;
            ExpiredTime = expiredTime;
        }
    }
}