using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Repositories;
using Services.Models;
using Data.DB;
using JWT.Models;
using Data.Migrations;
using AutoMapper;
namespace Jwt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerRepository jWTManager;
        private readonly IUserServiceRepository userServiceRepository;
        private readonly IMapper mapper;

        public UsersController(
            IJWTManagerRepository jWTManager, 
            IUserServiceRepository userServiceRepository,
            IMapper mapper)
        {
            this.jWTManager = jWTManager;
            this.userServiceRepository = userServiceRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "iris iris"
        };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(UserViewModel user)
        {
            var userDto = this.mapper.Map<UserDto>(user);

            var validUser = 
                await userServiceRepository.IsValidUserAsync(userDto);

            if (!validUser)
            {
                return Unauthorized("Incorrect username or password!");
            }

            var token = jWTManager.GenerateToken(user.Name);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            userServiceRepository.AddUserRefreshTokens(token);
            userServiceRepository.SaveCommit();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("addToken")]
        public IActionResult AddToken(TokenDto token)
        {
            userServiceRepository.AddUserRefreshTokens(token);
            userServiceRepository.SaveCommit();
            return Ok(token);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refreshToken")]
        public IActionResult Refresh(TokenDto token)
        {
            var principal = 
                jWTManager
                .GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = 
                userServiceRepository
                .GetSavedRefreshTokens(username, token.Refresh_Token);

            if (savedRefreshToken.RefreshToken != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = jWTManager.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }


            userServiceRepository.DeleteUserRefreshTokens(username, token.Refresh_Token);
            userServiceRepository.AddUserRefreshTokens(newJwtToken);
            userServiceRepository.SaveCommit();

            return Ok(newJwtToken);
        }
    }
}
