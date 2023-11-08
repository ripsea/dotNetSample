

namespace Services.Models.Repositories
{
    public interface IJWTManagerRepository
    {
        string GenerateJwtToken(string userName);
        string GenerateJwtRefreshToken(out DateTime expiredDateTime);
    }
}
