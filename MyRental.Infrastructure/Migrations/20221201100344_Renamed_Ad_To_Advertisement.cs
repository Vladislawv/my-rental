using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRental.Infrastructure.Migrations
{
    public partial class Renamed_Ad_To_Advertisement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_UserId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Ads_AdId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ads",
                table: "Ads");

            migrationBuilder.RenameTable(
                name: "Ads",
                newName: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "AdId",
                table: "Medias",
                newName: "AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_AdId",
                table: "Medias",
                newName: "IX_Medias_AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_UserId",
                table: "Advertisements",
                newName: "IX_Advertisements_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3afc091e-28cd-4f07-a93b-568f50c55ac5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4c6770d1-a485-4128-8af8-7605f96630ae");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Advertisements_AdvertisementId",
                table: "Medias",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Advertisements_AdvertisementId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "Ads");

            migrationBuilder.RenameColumn(
                name: "AdvertisementId",
                table: "Medias",
                newName: "AdId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_AdvertisementId",
                table: "Medias",
                newName: "IX_Medias_AdId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_UserId",
                table: "Ads",
                newName: "IX_Ads_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ads",
                table: "Ads",
                column: "Id");

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
                column: "ConcurrencyStamp",
                value: "a618cc45-f2ea-4e68-8cf7-c4b280bb11ce");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_UserId",
                table: "Ads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Ads_AdId",
                table: "Medias",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
