using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnmChipsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Prestador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prestadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nombres = table.Column<string>(type: "varchar(80)", nullable: false),
                    PrimerAp = table.Column<string>(type: "varchar(80)", nullable: false),
                    SegundoAp = table.Column<string>(type: "varchar(80)", nullable: true),
                    HoraEntradaBase = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraSalidaBase = table.Column<TimeOnly>(type: "time", nullable: false),
                    Email = table.Column<string>(type: "varchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestadores", x => x.Id);
                    table.UniqueConstraint("AK_Prestadores_Email", x => x.Email);
                    table.UniqueConstraint("AK_Prestadores_Codigo", x => x.Codigo);
                });
            migrationBuilder.CreateIndex(
                    name: "IX_Prestadores_Codigo",
                    table: "Prestadores",
                    column: "Codigo",
                    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestadores");
        }
    }
}
