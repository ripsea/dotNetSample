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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

    //提供 Request/Response 的 example
    options.ExampleFilters();

    //在 class 所標記的 Summary 說明檔，編譯變成 xml 檔，可以讓 Swashbuckle.AspNetCore 套用
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//提供 Request/Response 的 example
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
//builder.Services.AddSwaggerExamplesFromAssemblyOf<UserViewModelRequestExample>();
#endregion

#region Identity

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

builder.Services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
    o => {

        o.IncludeErrorDetails = true;
        var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // on production make it true
                ValidateAudience = false, // on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };
        o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context => {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                },

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

builder.Services.AddControllers();

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

app.MapControllers();

app.Run();
public partial class Program { } // this part for nunit test
