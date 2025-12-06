using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogWithMyPrincess.Migrations
{
    /// <inheritdoc />
    public partial class actualizamostabladepost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reaccion",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "dislikes",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "Posts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dislikes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "likes",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "Reaccion",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
