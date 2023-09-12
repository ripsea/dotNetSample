using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Server.EF
{
    public class DEVContext : DbContext
    {
        //Constructor calling the DbContext class constructor
        public DEVContext() : base("name=DEV")
        {
            //Database.SetInitializer(new DEVDBInitializer());
            //when "Enable-Migrations" enabled, initializer replaced by Migrations/Configuation.cs
        }
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   

        }
        */
        //Adding Domain Classes as DbSet
        public DbSet<ClientMaster> ClientMasters { get; set; }
        public DbSet<UserMaster> UserMasters { get; set; }

    }
}