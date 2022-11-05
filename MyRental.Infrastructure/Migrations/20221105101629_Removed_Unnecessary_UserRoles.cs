using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRental.Infrastructure.Migrations
{
    public partial class Removed_Unnecessary_UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d5faa3df-8aa8-4c9e-9919-be8f475c4057");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a618cc45-f2ea-4e68-8cf7-c4b280bb11ce", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3b6a7f3b-aabb-4769-9a5a-29c1e8b81213");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "416a6c50-9456-4e45-8f1f-e93e9b281e07", "Realtor", "Realtor" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Type" },
                values: new object[,]
                {
                    { 3, "4f60c970-1ba2-4768-9768-0c021d286228", "Landlord", "Landlord", 3 },
                    { 4, "78bca0fc-8c4c-4e59-80e2-9462a401a6b7", "Tenant", "Tenant", 4 }
                });
        }
    }
}
