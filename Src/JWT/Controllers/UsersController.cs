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
        [SwaggerResponse(HttpStatusCode.OK, "成功", typeof(TokenRequest))]
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

            var token = jWTManager.GenerateToken(login.Name);
            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            auth = await userServiceRepository.AddRefreshToken(login.Name);
            auth.AccessToken = token.Access_Token;
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
        public async Task<IActionResult> RefreshToken(TokenRequest token)
        {

            var principal =
                jWTManager
                .GetPrincipalFromExpiredToken(token.Access_Token);

            var username = principal.Identity?.Name;

            var checkToken = await userServiceRepository.GetRefreshToken(username);
            
            if ((checkToken.Refresh_Token!= token.Refresh_Token)
                ||(checkToken.RefreshTokenExpiryTime < DateTime.Now))
            {
                return Unauthorized("Invalid attempt!");
            }

            var user = await userServiceRepository.AddRefreshToken(username);
            user.AccessToken = checkToken.Access_Token;

            if (user == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            return Ok(user);
        }

        //https://mp.weixin.qq.com/s/fWVR-y9C5vqmGZ_aOApq5A
        /*
        private async Task<AuthResult> VerifyAndGenerateToken(
            TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                // 此验证功能将确保 Token 满足验证参数，并且它是一个真正的 token 而不仅仅是随机字符串
                var tokenInVerification = 
                    jwtTokenHandler.ValidateToken(
                        tokenRequest.Access_Token, 
                        _tokenValidationParams, 
                        out var validatedToken);

                // Validation 2 - Validate encryption alg
                // 检查 token 是否有有效的安全算法
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                // 验证原 token 的过期时间，得到 unix 时间戳
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Token has not yet expired"
                }
                    };
                }

                // validation 4 - validate existence of the token
                // 验证 refresh token 是否存在，是否是保存在数据库的 refresh token
                var storedRefreshToken = 
                    await _apiDbContext.RefreshTokens
                    .FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedRefreshToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Refresh Token does not exist"
                }
                    };
                }

                // Validation 5 - 检查存储的 RefreshToken 是否已过期
                // Check the date of the saved refresh token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "Refresh Token has expired, user needs to re-login" },
                        Success = false
                    };
                }

                // Validation 6 - validate if used
                // 验证 refresh token 是否已使用
                if (storedRefreshToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Refresh Token has been used"
                }
                    };
                }

                // Validation 7 - validate if revoked
                // 检查 refresh token 是否被撤销
                if (storedRefreshToken.IsRevorked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Refresh Token has been revoked"
                }
                    };
                }

                // Validation 8 - validate the id
                // 这里获得原 JWT token Id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                // 根据数据库中保存的 Id 验证收到的 token 的 Id
                if (storedRefreshToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "The token doesn't mateched the saved token"
                }
                    };
                }



                // update current token 
                // 将该 refresh token 设置为已使用
                storedRefreshToken.IsUsed = true;
                _apiDbContext.RefreshTokens.Update(storedRefreshToken);
                await _apiDbContext.SaveChangesAsync();

                // 生成一个新的 token
                var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
                return await GenerateJwtToken(dbUser);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Token has expired please re-login"
                }
                    };
                }
                else
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                {
                    "Something went wrong."
                }
                    };
                }
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }
        */
    }
}
