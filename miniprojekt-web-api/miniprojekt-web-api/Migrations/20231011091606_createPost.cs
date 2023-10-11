using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojekt_web_api.Migrations
{
    /// <inheritdoc />
    public partial class createPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Post",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Header",
                table: "Post");
        }
    }
}
