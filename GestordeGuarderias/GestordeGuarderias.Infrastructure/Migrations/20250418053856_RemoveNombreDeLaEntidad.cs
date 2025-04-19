using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestordeGuarderias.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNombreDeLaEntidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadesNinos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActividadesNinos",
                columns: table => new
                {
                    NinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActividadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadesNinos", x => new { x.NinoId, x.ActividadId });
                    table.ForeignKey(
                        name: "FK_ActividadesNinos_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActividadesNinos_Ninos_NinoId",
                        column: x => x.NinoId,
                        principalTable: "Ninos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesNinos_ActividadId",
                table: "ActividadesNinos",
                column: "ActividadId");
        }
    }
}
