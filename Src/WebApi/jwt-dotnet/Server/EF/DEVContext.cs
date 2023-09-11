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
            Database.SetInitializer(new DEVDBInitializer());
        }

        //Adding Domain Classes as DbSet
        public DbSet<JWT_ClientMaster> ClientMasters { get; set; }
        public DbSet<JWT_UserMaster> UserMasters { get; set; }

    }
}