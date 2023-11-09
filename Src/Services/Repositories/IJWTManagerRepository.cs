

namespace Services.Models.Repositories
{
    public interface IJWTManagerRepository
    {
        string GenerateJwtAccessToken(string userName);
        string GenerateJwtRefreshToken(out DateTime expiredDateTime);
    }
}
