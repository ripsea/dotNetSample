using System;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.EF;

namespace Repo.DB
{
    public class DEVContext : DbContext
    {
        //Constructor calling the DbContext class constructor

        public DEVContext(DbContextOptions<DEVContext> options) : base(options)
        {
            //Database.SetInitializer(new DEVDBInitializer());
            //when "Enable-Migrations" enabled, initializer replaced by Migrations/Configuation.cs
        }

        //Adding Domain Classes as DbSet
        public DbSet<JwtUser> JwtUsers { get; set; }
        public DbSet<JwtUserRefreshTokens> JwtUserRefreshTokens { get; set; }

    }
}