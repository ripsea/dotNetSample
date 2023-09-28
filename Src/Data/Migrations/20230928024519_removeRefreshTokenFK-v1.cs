using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class removeRefreshTokenFKv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("c00880fb-7652-4730-9d91-66decebc8a36"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "Password", "RowVersion" },
                values: new object[] { new Guid("d9da60ae-26a8-440f-bb70-8e006d061a23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "iris", "iris", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d9da60ae-26a8-440f-bb70-8e006d061a23"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "Password", "RowVersion" },
                values: new object[] { new Guid("c00880fb-7652-4730-9d91-66decebc8a36"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "iris", "iris", null });
        }
    }
}
