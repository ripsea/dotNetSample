using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Models.Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly JwtConfigOptions _jwtConfigOptions;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly IUserServiceRepository userServiceRepository;

        public JWTManagerRepository(JwtConfigOptions jwtConfigOptions, 
            TokenValidationParameters tokenValidationParams,
            IUserServiceRepository userServiceRepository)
        {
            this._jwtConfigOptions = jwtConfigOptions;
            _tokenValidationParams = tokenValidationParams;
            this.userServiceRepository = userServiceRepository; 
        }
        public TokenDto GenerateToken(string userName)
        {
            return GenerateJWTTokens(userName);
        }

        public TokenDto GenerateRefreshToken(string username)
        {
            return GenerateJWTTokens(username);
        }

        public TokenDto GenerateJWTTokens(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtConfigOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName)
                        //Claims.Add(new Claim(ClaimTypes.Role, userRole));
                    }),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = 
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), 
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_jwtConfigOptions.RefreshTokenValidityInDays,
                out int refreshTokenValidityInDays);

            return new TokenDto { 
                AccessToken = tokenHandler.WriteToken(token), 
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays)
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //https://mp.weixin.qq.com/s/fWVR-y9C5vqmGZ_aOApq5A
        //https://www.cnblogs.com/ittranslator/p/asp-net-core-5-rest-api-authentication-with-jwt-step-by-step.html
        private async Task<TokenResultDto> VerifyToken(
            TokenDto tokenRequest)
        {
            TokenResultDto tokenResultDto = DtoFactory.TokenResultDto();
            List<string> errors = new List<string>();
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                // Program.AddJwtBearer()參數
                var principal =
                    jwtTokenHandler.ValidateToken(
                        tokenRequest.AccessToken,
                        _tokenValidationParams,
                        out var validatedToken);

                // Validation 2 - Validate encryption alg
                // 是否有有效的安全算法
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256, 
                        StringComparison.InvariantCultureIgnoreCase);

                    if (result==false)
                    {
                        tokenResultDto.Success = false;
                        errors.Add("JwtSecurityToken Alg Check Failed.");
                        tokenResultDto.Errors = errors;
                        return tokenResultDto;
                    }
                }

                // Validation 3 - validate expiry date
                // token過期
                var utcExpiryDate = long.Parse(
                    principal.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    tokenResultDto.Success = false;
                    errors.Add("Token has not yet expired.");
                    tokenResultDto.Errors = errors;
                    return tokenResultDto;
                }

                // validation 4 - validate existence of the token
                // Check refresh token
                var storedRefreshToken = await userServiceRepository.GetRefreshToken(principal.Identity.Name);
                if (storedRefreshToken == null)
                {                    
                    tokenResultDto.Success = false;
                    errors.Add("Refresh Token does not exist.");
                    tokenResultDto.Errors = errors;
                    return tokenResultDto;
                }

                // Validation 5 - 检查存储的 RefreshToken 是否已过期
                // Check the date of the saved refresh token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.RefreshTokenExpiryTime)
                {
                    tokenResultDto.Success = false;
                    errors.Add("Refresh Token has expired, user needs to re-login");
                    tokenResultDto.Errors = errors;
                    return tokenResultDto;
                }

                // Validation 6 - validate if used
                // 验证 refresh token 是否已使用
                if (storedRefreshToken.RefreshTokenIsUsed)
                {
                    tokenResultDto.Success = false;
                    errors.Add("Refresh Token has been used");
                    tokenResultDto.Errors = errors;
                    return tokenResultDto;
                }

                // Validation 7 - validate if revoked
                // 检查 refresh token 是否被撤销
                if (storedRefreshToken.RefreshTokenIsRevorked)
                {
                    tokenResultDto.Success = false;
                    errors.Add("Refresh Token has been revoked");
                    tokenResultDto.Errors = errors;
                    return tokenResultDto;
                }

                // Validation 8 - validate the user id
                // 比對產生JWT token 時儲存的user.Id
                var jti = principal.Claims.FirstOrDefault(
                    x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedRefreshToken.RefreshTokenJwtId != jti)
                {
                    tokenResultDto.Success = false;
                    errors.Add("The token doesn't mateched the saved token");
                    tokenResultDto.Errors = errors;
                    return tokenResultDto;
                }
                return tokenResultDto;
                /*
                // update current token 
                // 将该 refresh token 设置为已使用
                storedRefreshToken.IsUsed = true;
                _apiDbContext.RefreshTokens.Update(storedRefreshToken);
                await _apiDbContext.SaveChangesAsync();

                // 生成一个新的 token
                var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
                return await GenerateJwtToken(dbUser);
                */

            }
            catch (Exception ex)
            {
                return null;
                /*
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
                */
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_jwtConfigOptions.Key);

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = 
                tokenHandler.ValidateToken(
                    token,
                    _tokenValidationParams, 
                    out SecurityToken securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null 
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256, 
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }

    }
}
