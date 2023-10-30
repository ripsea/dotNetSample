using System.Security.Claims;

namespace Services.Models.Repositories
{
    public interface IJWTManagerRepository
    {
        TokenRequest GenerateToken(string userName);
        TokenRequest GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
