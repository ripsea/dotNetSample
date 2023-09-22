using System;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DB
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
        public virtual DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }

    }
}