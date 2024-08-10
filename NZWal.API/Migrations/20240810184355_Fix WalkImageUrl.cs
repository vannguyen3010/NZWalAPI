using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWal.API.Migrations
{
    /// <inheritdoc />
    public partial class FixWalkImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImgeUrl",
                table: "Walks",
                newName: "WalkImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImageUrl",
                table: "Walks",
                newName: "WalkImgeUrl");
        }
    }
}
