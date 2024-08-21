using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnmChipsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFotoPrestador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Prestadores",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Prestadores");
        }
    }
}
