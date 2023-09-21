using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repo.Migrations
{
    /// <inheritdoc />
    public partial class dbinitials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JwtUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    UserName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    IssuedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtUserRefreshTokens", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JwtUser");

            migrationBuilder.DropTable(
                name: "JwtUserRefreshTokens");
        }
    }
}
