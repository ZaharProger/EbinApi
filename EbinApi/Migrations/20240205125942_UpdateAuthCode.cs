using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbinApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "Auth_codes",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "Auth_codes",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");
        }
    }
}
