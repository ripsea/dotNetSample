using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Repositories;
using Services.Models;
using Data.DB;
using JWT.Models;
using Data.Migrations;
using AutoMapper;
using AutoMapper.Execution;
using System.Net;
using Swashbuckle.Swagger.Annotations;
using Swashbuckle.Examples;
using Jwt.Swagger;

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


        /// <summary>
        /// 使用者登入認證並取得token
        /// </summary>
        /// <param name="user">使用者登入資料</param>
        /// <returns>A newly TokenDto</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticate
        ///     {
        ///        "Name": "iris",
        ///        "Password": "isapassword"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        [SwaggerResponse(HttpStatusCode.OK, "成功", typeof(TokenDto))]
        //[SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        //[SwaggerRequestExample(typeof(UserViewModel), typeof(UserViewModelRequestExample))]
        //[SwaggerResponseExample(HttpStatusCode.OK, typeof(TokenDto), typeof(TokenDtoResponseExample))]
        [SwaggerOperation(Tags = new[] { "JWT", "使用者作業" })]
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
