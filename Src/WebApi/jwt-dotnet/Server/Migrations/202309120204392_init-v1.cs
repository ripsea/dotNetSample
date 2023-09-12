namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JWT_ClientMaster",
                c => new
                    {
                        ClientKeyId = c.Int(nullable: false, identity: true),
                        ClientID = c.String(maxLength: 500),
                        ClientSecret = c.String(maxLength: 500),
                        ClientName = c.String(maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ClientKeyId);
            
            CreateTable(
                "dbo.JWT_UserMaster",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        UserPassword = c.String(maxLength: 50),
                        UserRoles = c.String(maxLength: 500),
                        UserEmailID = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JWT_UserMaster");
            DropTable("dbo.JWT_ClientMaster");
        }
    }
}
