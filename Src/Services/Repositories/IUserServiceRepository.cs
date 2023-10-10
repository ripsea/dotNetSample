using Data.Entities;
using Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {

        Task<TokenDto> GetRefreshToken(string username);
        Task<User> AddRefreshToken(string name);

        Task RevokeRefreshToken(string username);

        void SaveCommit();

        Task<bool> LoginAsync(string name, string password);
        Task<User> GetUserAsync(string username);
        Task<IdentityResult> CreateUserAsync(
            string username,
            string email,
            string password);
    }
}
