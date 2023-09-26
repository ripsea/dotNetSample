using Data.DB;
using Data.Entities;
using Data.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Services.Models.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly RepositoryWrapper _repo;

        public UserServiceRepository(RepositoryWrapper repo)
        {
            this._repo = repo;
        }

        public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
        {
            _repo.UserRefreshToken.Create(user);
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _repo.UserRefreshToken
                .FindAll()
                .FirstOrDefault(x => 
                    x.User.Name == username &&
                    x.RefreshToken == refreshToken);
            if (item != null)
            {
                _repo.UserRefreshToken.Delete(item);
            }
        }

        public UserRefreshToken GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _repo.UserRefreshToken
                .FindAll()
                .FirstOrDefault(
                    x => x.User.Name == username && 
                    x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public void SaveCommit()
        {
            _repo.Save();
        }

        public async Task<bool> IsValidUserAsync(User user)
        {
            var repoUser =  _repo.User
                .FindByCondition(x => x.Name == user.Name && 
                x.Password == user.Password);

            if (repoUser!=null) { return  true; }
            return false;
        }
    }
}
