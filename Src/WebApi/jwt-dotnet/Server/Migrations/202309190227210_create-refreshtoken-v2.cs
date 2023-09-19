namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createrefreshtokenv2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JWT_RefreshToken",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 500),
                        ClientID = c.String(maxLength: 500, unicode: false),
                        IssuedTime = c.DateTime(nullable: false),
                        ExpiredTime = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JWT_RefreshToken");
        }
    }
}
