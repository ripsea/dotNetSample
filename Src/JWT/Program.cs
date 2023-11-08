using Data.DB;
using Data.Entities.Auth;
using Data.Repositories;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Jwt;
using Jwt.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Models.Repositories;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Configuration;
using System.Reflection;
using System.Text;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Services.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
//    //.CreateLogger(); //for Two-stage initialization; replace by CreateBootstrapLogger()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web host");

    // Add services to the container.
    #region Services

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    #region Swagger

    builder.Services.AddSwaggerGen(options =>
    {
        #region version setting
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "JWT API",
            Description = "Iris ASP.NET Core Web API for managing JWT items",
            TermsOfService = new Uri("https://dotblogs.com.tw/irislai"),
            Contact = new OpenApiContact
            {
                Name = "Iris",
                Url = new Uri("https://dotblogs.com.tw/irislai")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }
        });

        options.SwaggerDoc("v2",
            new OpenApiInfo { Title = "Jwt API - V2", Version = "v2" });
        #endregion

        #region authorization
        // Authorization
        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization"
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
            });

        #endregion

        //���� Request/Response �� example
        options.ExampleFilters();

        //�b class �ҼаO�� Summary �����ɡA�sĶ�ܦ� xml �ɡA�i�H�� Swashbuckle.AspNetCore �M��
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    //���� Request/Response �� example
    builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    //builder.Services.AddSwaggerExamplesFromAssemblyOf<UserViewModelRequestExample>();
    #endregion

    #region services-Identity

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>
        (
        options => {
            options.Password.RequireUppercase = true; // on production add more secured options
            options.Password.RequireDigit = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<DEVDbContext>()
        .AddDefaultTokenProviders();

    #endregion

    #region services-Authentication
    var JwtConfig = 
        builder.Configuration.GetSection(JwtConfigOptions.JwtConfig)
        .Get<JwtConfigOptions>();

    builder.Services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
        o => {

            // �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
            o.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

            o.TokenValidationParameters = new TokenValidationParameters
            {
                // �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
                NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                // �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
                RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                // �@��ڭ̳��|���� Issuer
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.Issuer,

                // �q�`���ӻݭn���� Audience
                ValidateAudience = false,
                //ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

                // �@��ڭ̳��|���� Token �����Ĵ���
                ValidateLifetime = true,

                // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
                ValidateIssuerSigningKey = false,

                // "1234567890123456" ���ӱq IConfiguration ���o
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Key))
            };
            o.Events = new JwtBearerEvents
            {

                //JwtBearer debug
                //https://nestenius.se/2023/02/21/troubleshooting-jwtbearer-authentication-problems-in-asp-net-core/
                OnMessageReceived = msg =>
                {
                    msg.Request.Headers.TryGetValue("Authorization", out var BearerToken);
                    msg.Request.Headers.TryGetValue("Path", out var RequestPath);
                    if (!string.IsNullOrEmpty(BearerToken))
                    {
                        Console.WriteLine("Access token");
                        Console.WriteLine($"URL: {RequestPath}");
                        Console.WriteLine($"Token: {BearerToken}\r\n");
                    }
                    else
                    {
                        Console.WriteLine("Access token");
                        Console.WriteLine("URL: " + RequestPath);
                        Console.WriteLine("Token: No access token provided\r\n");
                    }
                    return Task.CompletedTask;
                }


            };
        });
    #endregion

    #region services-configurations
    builder.Services.Configure<JwtConfigOptions>(
        builder.Configuration.GetSection(JwtConfigOptions.JwtConfig));
    #endregion

    #region services-others
    builder.Services.AddScoped<IMapService, MapService>();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
    builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
    builder.Services.AddScoped<IUserServiceRepository, UserServiceRepository>();
    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
    builder.Services.AddDbContextPool<DEVDbContext>(
        options
            => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
    #endregion
    builder.Services.AddControllers();
    #endregion

    #region host
    builder.Host.UseSerilog();


   #endregion

    #region app
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSerilogRequestLogging(); 
    app.MapControllers();

    app.Run();

    #endregion

    return 0;

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
public partial class Program { } // this part for nunit test
