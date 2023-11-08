namespace Services.Configurations
{   
    public class JwtConfigOptions
    {
        public const string JwtConfig = "JWT";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double TokenValidityInMinutes { get; set; }
        public string RefreshTokenValidityInDays { get; set; }

    }
}
