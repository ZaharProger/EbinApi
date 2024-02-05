using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbinApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fullname",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "Users",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "middle_name",
                table: "Users",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Users",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "middle_name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "fullname",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
