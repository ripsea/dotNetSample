using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo.Migrations
{
    /// <inheritdoc />
    public partial class dbinitialsv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JwtUser");

            migrationBuilder.DropTable(
                name: "JwtUserRefreshTokens");

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.CreateTable(
                name: "JwtUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JwtUserRefreshTokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiredTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IssuedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtUserRefreshTokens", x => x.ID);
                });
        }
    }
}
