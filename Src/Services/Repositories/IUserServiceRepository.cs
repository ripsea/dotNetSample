using Data.Entities;
using Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {
        Task<bool> IsRefreshTokenValid(string username,string refreshToken);
        Task<TokenDto> GetRefreshTokenData(string username);
        Task<TokenDto> CreateUserToken(string name);

        Task RevokeRefreshToken(string username);

        void SaveCommit();

        Task<bool> LoginAsync(string name, string password);
        Task<TokenDto> GetUserAsync(string username);
        Task<IdentityResult> CreateUserAsync(
            string username,
            string email,
            string password);
    }
}
