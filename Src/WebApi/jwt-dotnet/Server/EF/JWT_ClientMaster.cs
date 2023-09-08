using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.EF
{
    public class JWT_ClientMaster
    {
        public int ClientKeyId { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }

        public JWT_ClientMaster(int ClientKeyId_, string ClientID_, string ClientSecret_, string ClientName_, bool Active_, int RefreshTokenLifeTime_, string AllowedOrigin_)
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