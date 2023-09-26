﻿using Data.Entities;

namespace Services.Models.Repositories
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(User users);

        UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);

        UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        void SaveCommit();
    }
}
