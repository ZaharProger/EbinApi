using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbinApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCodeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Auth_codes",
                type: "text",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "code",
                table: "Auth_codes",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
