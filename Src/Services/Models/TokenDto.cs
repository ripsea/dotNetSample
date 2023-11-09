using Services.Models.Common;

namespace Services.Models
{
    public class TokenDto:BaseModel
    {
        public string UserName { get; set; }
        public bool? RefreshTokenIsUsed { get; set; } // 如果已经使用过它，我们不想使用相同的 refresh token 生成新的 JWT token
        public bool? RefreshTokenIsRevorked { get; set; } // 是否出于安全原因已将其撤销
        public DateTime RefreshTokenAddedDate { get; set; }
        public string? RefreshTokenJwtId { get; set; } // 產生RefreshToekn時儲存的User.Id
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
