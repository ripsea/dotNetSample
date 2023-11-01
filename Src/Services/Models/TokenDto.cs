using Services.Models.Common;

namespace Services.Models
{
    public class TokenDto:BaseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
