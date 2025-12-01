using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogWithMyPrincess.Migrations
{
    /// <inheritdoc />
    public partial class Actualizamoslosocmentarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_userId",
                table: "Posts",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_userId",
                table: "Posts",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_userId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_userId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comments");
        }
    }
}
