using Data.Entities;
using Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {

        Task<TokenRequest> GetRefreshToken(string username);
        Task<AuthResult> AddRefreshToken(string name);

        Task RevokeRefreshToken(string username);

        void SaveCommit();

        Task<bool> LoginAsync(string name, string password);
        Task<AuthResult> GetUserAsync(string username);
        Task<IdentityResult> CreateUserAsync(
            string username,
            string email,
            string password);
    }
}
