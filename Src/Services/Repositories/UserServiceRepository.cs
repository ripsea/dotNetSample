using Data.DB;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Services.Models.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DEVDbContext _db;

        public UserServiceRepository(UserManager<IdentityUser> userManager, DEVDbContext db)
        {
            this._userManager = userManager;
            this._db = db;
        }

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _db.UserRefreshTokens.Add(user);
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _db.UserRefreshTokens.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshTokens.Remove(item);
            }
        }

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _db.UserRefreshTokens.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return _db.SaveChanges();
        }

        public async Task<bool> IsValidUserAsync(Users users)
        {
            var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.Name);
            var result = await _userManager.CheckPasswordAsync(u, users.Password);
            return result;

        }
    }
}
