using Data.Entities;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(Users users);

        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);

        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        int SaveCommit();
    }
}
