using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.EF
{
    public class JWT_UserMaster
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRoles { get; set; }
        public string UserEmailID { get; set; }

        public JWT_UserMaster(int UserID_, string UserName_, string UserPassword_, string UserRoles_, string UserEmailID_)
        {
            this.UserID = UserID_;
            this.UserName = UserName_;
            this.UserPassword = UserPassword_;
            this.UserRoles = UserRoles_;
            this.UserEmailID = UserEmailID_;
        }
    }
}