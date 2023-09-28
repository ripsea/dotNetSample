using Data.DB;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Services.Models.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly IRepositoryWrapper _repo;

        public UserServiceRepository(IRepositoryWrapper repo)
        {
            this._repo = repo;
        }

        public UserRefreshToken AddUserRefreshTokens(TokenDto user)
        {
            UserRefreshToken ttt = new UserRefreshToken() { RefreshToken="abc"};
            _repo.UserRefreshToken.Create(ttt);
            //_repo.Create(ttt);
            return ttt;
        }

        public void DeleteUserRefreshTokens(
            string username, 
            string refreshToken)
        {
            /*
            var item = _repo.UserRefreshToken
                .FindAll()
                .FirstOrDefault(x => 
                    x.User.Name == username &&
                    x.RefreshToken == refreshToken);
            if (item != null)
            {
                _repo.UserRefreshToken.Delete(item);
            }
            */
        }

        public UserRefreshToken GetSavedRefreshTokens(
            string username, 
            string refreshToken)
        {
            /*
            return _repo.UserRefreshToken
                .FindAll()
                .FirstOrDefault(
                    x => x.User.Name == username && 
                    x.RefreshToken == refreshToken && x.IsActive == true);
            */
            return null;
        }

        public void SaveCommit()
        {
            _repo.Save();
            //_repo.SaveChanges();
        }

        public async Task<bool> IsValidUserAsync(UserDto user)
        {
            /*
            var repoUser =  _repo.User
                .FindByCondition(x => x.Name == user.Name && 
                x.Password == user.Password);

            if (repoUser!=null) { return  true; }
            */
            return false;
        }
    }
}
