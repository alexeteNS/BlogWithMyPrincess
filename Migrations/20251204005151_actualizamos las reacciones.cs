using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogWithMyPrincess.Migrations
{
    /// <inheritdoc />
    public partial class actualizamoslasreacciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reaccion",
                table: "Comments",
                newName: "likes");

            migrationBuilder.AddColumn<int>(
                name: "dislikes",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dislikes",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "likes",
                table: "Comments",
                newName: "Reaccion");
        }
    }
}
