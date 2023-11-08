

namespace Services.Models.Repositories
{
    public interface IJWTManagerRepository
    {
        TokenDto GenerateToken(string userName);
        TokenDto GenerateRefreshToken(string userName);
    }
}
