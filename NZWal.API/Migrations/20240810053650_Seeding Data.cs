using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWal.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegionImageUrl",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("026a6f1e-3d0d-4b74-8c32-1f2bac5db8d6"), "Easy" },
                    { new Guid("779de2da-f578-4573-8bde-14175cf7b17e"), "Hard" },
                    { new Guid("a1f90e05-4abd-4761-9b66-4bd48d55bd4d"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("597e2399-58d2-46a8-8e58-5e7d9bada3fa"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("b7afc251-4a50-4bde-a000-7e0b4a8bf1f0"), "NTL", "Northland", null },
                    { new Guid("bdebbae5-d2dc-4ce5-bc22-235b208aa162"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("e3df00fd-62bd-482a-a94a-db3308eb7d0f"), "BOP", "Bay Of Plenty", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("026a6f1e-3d0d-4b74-8c32-1f2bac5db8d6"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("779de2da-f578-4573-8bde-14175cf7b17e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a1f90e05-4abd-4761-9b66-4bd48d55bd4d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("597e2399-58d2-46a8-8e58-5e7d9bada3fa"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b7afc251-4a50-4bde-a000-7e0b4a8bf1f0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bdebbae5-d2dc-4ce5-bc22-235b208aa162"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e3df00fd-62bd-482a-a94a-db3308eb7d0f"));

            migrationBuilder.AlterColumn<string>(
                name: "RegionImageUrl",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
