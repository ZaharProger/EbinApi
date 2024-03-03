using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbinApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMinAndroidIosTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MinIos",
                table: "Apps",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MinAndroid",
                table: "Apps",
                type: "text",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "MinIos",
                table: "Apps",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "MinAndroid",
                table: "Apps",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
