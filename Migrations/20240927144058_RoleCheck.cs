using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RoleCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0596eb84-7236-4544-8ad7-2d47ec80897d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7236770-76bd-4a56-836d-ca01c1d8e435");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45c23046-718f-4de0-b3e3-24c15ee57930", null, "Admin", "ADMIN" },
                    { "d38e4e5a-b610-4760-9b91-5b6496bb7f37", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45c23046-718f-4de0-b3e3-24c15ee57930");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d38e4e5a-b610-4760-9b91-5b6496bb7f37");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0596eb84-7236-4544-8ad7-2d47ec80897d", null, "Admin", "ADMIN" },
                    { "f7236770-76bd-4a56-836d-ca01c1d8e435", null, "User", "USER" }
                });
        }
    }
}
