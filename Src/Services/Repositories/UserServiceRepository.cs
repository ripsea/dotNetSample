using AutoMapper.Internal.Mappers;
using Data.DB;
using Data.Entities;
using Data.Entities.Auth;
using Data.Repositories;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
        //Logging in a .Net Core Library
        //https://xfischer.github.io/logging-dotnet-core/
        private readonly ILogger<UserServiceRepository> _logger;

        public UserServiceRepository(IRepositoryWrapper repo,
            IJWTManagerRepository jwtManagerRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<UserServiceRepository> logger)
        {
            this._repo = repo;
            this._jwtManagerRepository = jwtManagerRepository;
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task<TokenDto> GetRefreshTokenData(string refreshToken)
        {
            //retrieve the saved refresh token from database            
            var appUser = _userManager.Users
                    .Where(x => x.RefreshToken == refreshToken)
                    .FirstOrDefault();
            
            return ObjectMapper.Mapper.Map<TokenDto>(appUser);
        }

        public async Task<bool> IsRefreshTokenValid(
            string username,
            string refreshToken)
        {
            var userToken = await GetRefreshTokenData(refreshToken);

            if ((userToken==null)
                || (userToken.RefreshToken != refreshToken)
                || (userToken.RefreshTokenExpiryTime < DateTime.Now)
                || (userToken.RefreshToken == null)
                || (userToken.RefreshTokenIsUsed == true)
                || (userToken.RefreshTokenIsRevorked == true)
                || (!userToken.UserName.Equals(username)))
            {
                return false;
            }
            return true;
        }

        public async Task<TokenDto> CreateUserToken(string name)
        {
            await RevokeRefreshToken(name);
            ApplicationUser appUser = await _userManager.FindByNameAsync(name);
            var jwtRefreshToken = _jwtManagerRepository.GenerateJwtRefreshToken(
                out DateTime RefreshTokenExpiryTime);
            appUser.RefreshToken = jwtRefreshToken;
            appUser.RefreshTokenExpiryTime = RefreshTokenExpiryTime;          
            await _userManager.UpdateAsync(appUser);

            var token = _jwtManagerRepository.GenerateJwtAccessToken(name);
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            TokenDto tokenDto = ObjectMapper.Mapper.Map<TokenDto>(appUser);
            tokenDto.AccessToken = token;
            return tokenDto;
        }

        public async Task RevokeRefreshToken(
            string username)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(username);
            DtoFactory.SetRefreshTokenDefault(appUser);
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

        public async Task<TokenDto> GetUserAsync(string name)
        {
            _logger.LogInformation($"{name}");
            ApplicationUser appUser = await _userManager.FindByNameAsync(name);
            return ObjectMapper.Mapper.Map<TokenDto>(appUser);
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
