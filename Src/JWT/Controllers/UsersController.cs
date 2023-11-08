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
using Microsoft.IdentityModel.Tokens;
using Serilog;

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
        public async Task<IActionResult> Authenticate(LoginRequest login)
        {
            var auth = await userServiceRepository.GetUserAsync(login.Name);
            if (auth == null) { return Unauthorized("Incorrect username or password!"); }

            var result = await userServiceRepository.LoginAsync(login.Name, login.Password);
            if (!result)
            {
                return Unauthorized("Incorrect username or password!");
            }

            var token = jWTManager.GenerateJwtToken(login.Name);
            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            auth = await userServiceRepository.AddRefreshToken(login.Name);
            auth.AccessToken = token;
            _logger.LogInformation($"this is {login.Name}'s token {auth.AccessToken}");
            return Ok(auth);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
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
        public async Task<IActionResult> RefreshToken(TokenDto requestToken)
        {
            var userName = User.Identity.Name??null;

            var userToken = await userServiceRepository.GetRefreshToken(userName);          

            if ((userToken.RefreshToken!= requestToken.RefreshToken)
                ||(userToken.RefreshTokenExpiryTime < DateTime.Now)
                ||(userToken.RefreshToken == null)
                ||(userToken.RefreshTokenIsUsed==true)
                ||(userToken.RefreshTokenIsRevorked == true))
            {
                return Unauthorized("Invalid attempt!");
            }

            var user = await userServiceRepository.AddRefreshToken(userName);
            user.AccessToken = jWTManager.GenerateJwtToken(userName);

            if (user == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            return Ok(user);
        }

    }
}
