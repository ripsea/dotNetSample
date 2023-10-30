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

        public JWTManagerRepository(JwtConfigOptions jwtConfigOptions)
        {
            this._jwtConfigOptions = jwtConfigOptions;
        }
        public TokenRequest GenerateToken(string userName)
        {
            return GenerateJWTTokens(userName);
        }

        public TokenRequest GenerateRefreshToken(string username)
        {
            return GenerateJWTTokens(username);
        }

        public TokenRequest GenerateJWTTokens(string userName)
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

            return new TokenRequest { 
                Access_Token = tokenHandler.WriteToken(token), 
                Refresh_Token = refreshToken,
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

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_jwtConfigOptions.Key);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = 
                tokenHandler.ValidateToken(
                    token, 
                    tokenValidationParameters, 
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
