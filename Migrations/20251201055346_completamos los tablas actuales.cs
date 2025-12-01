using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogWithMyPrincess.Migrations
{
    /// <inheritdoc />
    public partial class completamoslostablasactuales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_IdCommentParent",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_IdPost",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Comments",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reaccion",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_IdCommentParent",
                table: "Comments",
                column: "IdCommentParent",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_IdPost",
                table: "Comments",
                column: "IdPost",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_IdCommentParent",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_IdPost",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Reaccion",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Comments",
                newName: "Image");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_IdCommentParent",
                table: "Comments",
                column: "IdCommentParent",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_IdPost",
                table: "Comments",
                column: "IdPost",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
