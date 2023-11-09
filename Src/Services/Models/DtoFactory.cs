using Data.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public static class DtoFactory
    {
        public static ApplicationUser SetRefreshTokenDefault(
            ApplicationUser application)
        {
            application.RefreshToken = null;
            application.RefreshTokenExpiryTime = null;
            application.RefreshTokenIsRevorked = false;
            application.RefreshTokenIsUsed = false;
            return application;
        }
    }
}
