using Services.Models.Common;

namespace Services.Models
{
    public class TokenRequest:BaseModel
    {
        public string Access_Token { get; set; }
        public string? Refresh_Token { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
