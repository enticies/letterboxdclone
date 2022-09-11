using Microsoft.EntityFrameworkCore.Migrations;

namespace lbdbackend.Data.Migrations
{
    public partial class AddedCommentCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Reviews");
        }
    }
}
