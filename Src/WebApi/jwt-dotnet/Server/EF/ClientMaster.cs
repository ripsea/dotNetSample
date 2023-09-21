﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server.EF
{
    [Table("JWT_ClientMaster")]
    public class ClientMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientKeyId { get; set; }
        [Column("ClientID", TypeName = "nvarchar")]
        [StringLength(500)]
        public string ClientID { get; set; }
        [Column("ClientSecret", TypeName = "nvarchar")]
        [StringLength(500)]
        public string ClientSecret { get; set; }
        [Column("ClientName", TypeName = "nvarchar")]
        [StringLength(100)]
        public string ClientName { get; set; }
        [Column("Active", TypeName = "bit")]
        public bool Active { get; set; }
        [Column("RefreshTokenLifeTime", TypeName = "int")]
        public int RefreshTokenLifeTime { get; set; }
        [Column("AllowedOrigin", TypeName = "nvarchar")]
        [StringLength(500)]
        public string AllowedOrigin { get; set; }

        public ClientMaster(int ClientKeyId_, string ClientID_, string ClientSecret_, string ClientName_, bool Active_, int RefreshTokenLifeTime_, string AllowedOrigin_)
        {
            this.ClientKeyId = ClientKeyId_;
            this.ClientID = ClientID_;
            this.ClientSecret = ClientSecret_;
            this.ClientName = ClientName_;
            this.Active = Active_;
            this.RefreshTokenLifeTime = RefreshTokenLifeTime_;
            this.AllowedOrigin = AllowedOrigin_;
        }
    }
}