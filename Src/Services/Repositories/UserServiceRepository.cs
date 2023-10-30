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
using System.IdentityModel.Tokens.Jwt;
using System.Web;

namespace Services.Models.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IJWTManagerRepository _jwtManagerRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServiceRepository(IRepositoryWrapper repo,
            IJWTManagerRepository jwtManagerRepository,
            UserManager<ApplicationUser> userManager)
        {
            this._repo = repo;
            this._jwtManagerRepository = jwtManagerRepository;
            this._userManager = userManager;
        }

        public async Task<TokenRequest> GetRefreshToken(string username)
        {
            //retrieve the saved refresh token from database
            ApplicationUser appUser = await _userManager.FindByNameAsync(username);
            TokenRequest token = new TokenRequest()
            {
                Refresh_Token = appUser.RefreshToken,
                RefreshTokenExpiryTime = appUser.RefreshTokenExpiryTime
            };
            return token;
        }

        public async Task<AuthResult> AddRefreshToken(string name)
        {
            await RevokeRefreshToken(name);
            ApplicationUser appUser = await _userManager.FindByNameAsync(name);
            var tokenDto = _jwtManagerRepository.GenerateRefreshToken(name);
            appUser.RefreshToken = tokenDto.Refresh_Token;
            appUser.RefreshTokenExpiryTime = tokenDto.RefreshTokenExpiryTime;
            await _userManager.UpdateAsync(appUser);

            return ObjectMapper.Mapper.Map<AuthResult>(appUser);
        }

        public async Task RevokeRefreshToken(
            string username)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(username);
            appUser.RefreshToken = null;
            await _userManager.UpdateAsync(appUser);
        }


        public void SaveCommit()
        {
            _repo.Save();
        }

        public async Task<bool> LoginAsync(string name, string password)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(name);
            return await _userManager.CheckPasswordAsync(appUser, password);
        }

        public async Task<AuthResult> GetUserAsync(string name)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(name);
            return ObjectMapper.Mapper.Map<AuthResult>(appUser);
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
