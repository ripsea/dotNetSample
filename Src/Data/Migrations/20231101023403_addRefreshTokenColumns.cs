using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addRefreshTokenColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b8b1b5e2-89a9-46ff-860b-752571e6c297"));

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenAddedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "RefreshTokenIsRevorked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RefreshTokenIsUsed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenJwtId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "Password", "RowVersion" },
                values: new object[] { new Guid("a9188722-4fc9-4efb-8e17-385af69a6c5f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "iris", "iris", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a9188722-4fc9-4efb-8e17-385af69a6c5f"));

            migrationBuilder.DropColumn(
                name: "RefreshTokenAddedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenIsRevorked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenIsUsed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenJwtId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "Password", "RowVersion" },
                values: new object[] { new Guid("b8b1b5e2-89a9-46ff-860b-752571e6c297"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "iris", "iris", null });
        }
    }
}
