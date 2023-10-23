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
using Jwt.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        private readonly ILogger<UsersController> _logger;
        public UsersController(
            IJWTManagerRepository jWTManager, 
            IUserServiceRepository userServiceRepository,
            IMapper mapper,
            ILogger<UsersController> logger)
        {
            this.jWTManager = jWTManager;
            this.userServiceRepository = userServiceRepository;
            this.mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        /// <param name="login">使用者登入資料</param>
        /// <returns>A newly TokenDto</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticate
        ///     {
        ///        "UserName": "iris",
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
        public async Task<IActionResult> Authenticate(LoginViewModel login)
        {
            var user = await userServiceRepository.GetUserAsync(login.Name);
            if (user == null) { return Unauthorized("Incorrect username or password!"); }

            var result = await userServiceRepository.LoginAsync(login.Name, login.Password);
            if (!result)
            {
                return Unauthorized("Incorrect username or password!");
            }

            var token = jWTManager.GenerateToken(login.Name);
            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            user = await userServiceRepository.AddRefreshToken(login.Name);
            user.AccessToken = token.Access_Token;
            _logger.LogInformation($"this is {login.Name}'s token {user.AccessToken}");
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = 
                await userServiceRepository.GetUserAsync(model.Username);

            if (user!=null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", 
                        Message = "User already exists!" });

            var result = await userServiceRepository.CreateUserAsync(
                username: model.Username,
                password: model.Password,
                email: model.Email
                );

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { 
                        Status = "Error", 
                        Message = "User creation failed! Please check user details and try again." });

            return Ok(
                new Response { 
                    Status = "Success", 
                    Message = "User created successfully!" });
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> Refresh(TokenDto token)
        {

            var principal =
                jWTManager
                .GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            var checkToken = userServiceRepository.GetRefreshToken(username);
            
            if ((checkToken.Result.Refresh_Token!= token.Refresh_Token)
                ||(checkToken.Result.RefreshTokenExpiryTime < DateTime.Now))
            {
                return Unauthorized("Invalid attempt!");
            }

            var user = userServiceRepository.AddRefreshToken(username);
            if (user == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            return Ok(user);
        }
    }
}
