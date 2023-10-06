using System;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Data.Entities;
using Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DB
{
    public class DEVDbContext : IdentityDbContext<ApplicationUser>
    {
        //Constructor calling the DbContext class constructor

        public DEVDbContext(DbContextOptions<DEVDbContext> options) : base(options)
        {
            //Database.SetInitializer(new DEVDBInitializer());
            //when "Enable-Migrations" enabled, initializer replaced by Migrations/Configuation.cs
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DEVDbInitializer(modelBuilder).Seed();
        }

        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}