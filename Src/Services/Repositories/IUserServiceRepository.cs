using Data.Entities;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(UserDto users);

        UserRefreshToken AddUserRefreshTokens(TokenDto user);

        UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        void SaveCommit();
    }
}
