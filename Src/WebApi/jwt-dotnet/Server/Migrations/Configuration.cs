namespace Server.Migrations
{
    using Server.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Server.EF.DEVContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Server.EF.DEVContext context)
        {
            UserMaster jWT_UserMaster = new UserMaster();

            jWT_UserMaster
                .SetUserName("iris")
                .SetUserEmailID("iris@uxb2b.com")
                .SetUserPassword("iris")
                .SetUserRoles("admin");

            context.UserMasters.Add(jWT_UserMaster);

            context.SaveChanges();
        }
    }
}
