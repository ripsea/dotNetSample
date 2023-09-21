using System;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Models;
using Server.EF;

namespace Repo.DB
{
    public class DEVDbContext : DbContext
    {
        //Constructor calling the DbContext class constructor

        public DEVDbContext(DbContextOptions<DEVDbContext> options) : base(options)
        {
            //Database.SetInitializer(new DEVDBInitializer());
            //when "Enable-Migrations" enabled, initializer replaced by Migrations/Configuation.cs
        }

        //Adding Domain Classes as DbSet
        public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }

    }
}