using AutoMapper.Internal.Mappers;
using Data.DB;
using Data.Entities;
using Data.Entities.Auth;
using Data.Repositories;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Services.AutoMapper;
using System;

namespace Services.Models.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly IRepositoryWrapper _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServiceRepository(IRepositoryWrapper repo,
            UserManager<ApplicationUser> userManager)
        {
            this._repo = repo;
            this._userManager = userManager;
        }

        public UserRefreshToken AddUserRefreshTokens(TokenDto user)
        {
            UserRefreshToken userRefreshToken = 
                ObjectMapper.Mapper.Map<UserRefreshToken>(user);
            _repo.UserRefreshToken.Create(userRefreshToken);
            return userRefreshToken;
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
        }

        public async Task<bool> IsValidUserAsync(UserDto user)
        {
            var repoUser =
                _repo.User
                .FindByCondition(
                    x => x.Name == user.Name &&
                    x.Password == user.Password)
                ;

            if (repoUser.Any()) { return true; }
            return false;
        }

        public async Task<bool> IsUserNameExistedAsync(string username)
        {
            var repoUser =
                await _userManager.FindByNameAsync(username);

            if (repoUser==null) { return false; }
            return true;
        }

        public async Task<IdentityResult> CreateUserAsync(
            string username, 
            string email,
            string password)
        {
            ApplicationUser user = new()
            {
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = username
            };

            return await _userManager.CreateAsync(user, password);
        }


    }
}
