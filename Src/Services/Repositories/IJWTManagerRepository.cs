using System.Security.Claims;

namespace Services.Models.Repositories
{
    public interface IJWTManagerRepository
    {
        TokenDto GenerateToken(string userName);
        TokenDto GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
