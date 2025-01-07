using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Focy.Restaurant.Migrations
{
    /// <inheritdoc />
    public partial class Add_ImagUri_Into_Menu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "Menus",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ImgUri",
                table: "Menus",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUri",
                table: "Menus");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "Menus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
