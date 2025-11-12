using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIEnercheck.Migrations
{
    /// <inheritdoc />
    public partial class UserReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserReq",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserReq",
                table: "AspNetUsers");
        }
    }
}
