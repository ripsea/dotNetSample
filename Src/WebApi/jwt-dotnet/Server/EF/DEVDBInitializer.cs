using Server.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI.WebControls;

namespace Server
{
    public class DEVDBInitializer: CreateDatabaseIfNotExists<DEVContext>
    {

        protected override void Seed(DEVContext context)
        {
            //Seed: A method that should be overridden to actually add data to the context for seeding.
            //The default implementation does nothing.
            //base.Seed(context);

            JWT_UserMaster jWT_UserMaster = new JWT_UserMaster();

            jWT_UserMaster
                .SetUserName("iris")
                .SetUserEmailID("iris@uxb2b.com")
                .SetUserPassword("iris");

            context.UserMasters.Add(jWT_UserMaster);

            context.SaveChanges();
        }
    }
}