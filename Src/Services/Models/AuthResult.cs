using Services.Models.Common;

namespace Services.Models
{
    public class AuthResult:BaseModel
    {
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
