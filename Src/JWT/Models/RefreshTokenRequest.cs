﻿namespace Jwt.Models
{
    public class RefreshTokenRequest
    {
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
    }
}
