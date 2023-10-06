using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {

        Task<bool> IsValidUserAsync(UserDto users);

        UserRefreshToken AddUserRefreshTokens(TokenDto user);

        UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        void SaveCommit();

        Task<bool> IsUserNameExistedAsync(string username);
        Task<IdentityResult> CreateUserAsync(
            string username,
            string email,
            string password);
    }
}
