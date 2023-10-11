using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojekt_web_api.Migrations
{
    /// <inheritdoc />
    public partial class llleChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Votes",
                table: "Post",
                newName: "Upvotes");

            migrationBuilder.RenameColumn(
                name: "Votes",
                table: "Comments",
                newName: "Upvotes");

            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                table: "Post",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Upvotes",
                table: "Post",
                newName: "Votes");

            migrationBuilder.RenameColumn(
                name: "Upvotes",
                table: "Comments",
                newName: "Votes");
        }
    }
}
