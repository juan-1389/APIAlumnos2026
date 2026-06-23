using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAlumnos2026.Migrations
{
    /// <inheritdoc />
    public partial class MirgacionHistorialAlumnoNuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistorialNotaAlumnos",
                table: "HistorialNotaAlumnos");

            migrationBuilder.RenameTable(
                name: "HistorialNotaAlumnos",
                newName: "HistorialNotaAlumno");

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "VistaNotaAlumnos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistorialNotaAlumno",
                table: "HistorialNotaAlumno",
                column: "HistorialNotaAlumnoId");

            migrationBuilder.CreateTable(
                name: "HistorialAlumno",
                columns: table => new
                {
                    HistorialAlumnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    CampoModificado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorAnterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorNuevo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAlumno", x => x.HistorialAlumnoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialAlumno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistorialNotaAlumno",
                table: "HistorialNotaAlumno");

            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "VistaNotaAlumnos");

            migrationBuilder.RenameTable(
                name: "HistorialNotaAlumno",
                newName: "HistorialNotaAlumnos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistorialNotaAlumnos",
                table: "HistorialNotaAlumnos",
                column: "HistorialNotaAlumnoId");
        }
    }
}
