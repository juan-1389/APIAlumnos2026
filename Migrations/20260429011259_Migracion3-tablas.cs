using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAlumnos2026.Migrations
{
    /// <inheritdoc />
    public partial class Migracion3tablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotaAlumno",
                table: "NotaAlumno");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "NotaAlumno");

            migrationBuilder.RenameTable(
                name: "NotaAlumno",
                newName: "NotaAlumnos");

            migrationBuilder.RenameColumn(
                name: "Dni",
                table: "NotaAlumnos",
                newName: "AsignaturaId");

            migrationBuilder.AddColumn<int>(
                name: "AlumnoId",
                table: "NotaAlumnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaNota",
                table: "NotaAlumnos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotaAlumnos",
                table: "NotaAlumnos",
                column: "NotaAlumnoId");

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    AlumnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.AlumnoId);
                });

            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.AsignaturaId);
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    DocenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.DocenteId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotaAlumnos_AlumnoId",
                table: "NotaAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaAlumnos_AsignaturaId",
                table: "NotaAlumnos",
                column: "AsignaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaAlumnos_Alumnos_AlumnoId",
                table: "NotaAlumnos",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "AlumnoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotaAlumnos_Asignaturas_AsignaturaId",
                table: "NotaAlumnos",
                column: "AsignaturaId",
                principalTable: "Asignaturas",
                principalColumn: "AsignaturaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaAlumnos_Alumnos_AlumnoId",
                table: "NotaAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaAlumnos_Asignaturas_AsignaturaId",
                table: "NotaAlumnos");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Asignaturas");

            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotaAlumnos",
                table: "NotaAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_NotaAlumnos_AlumnoId",
                table: "NotaAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_NotaAlumnos_AsignaturaId",
                table: "NotaAlumnos");

            migrationBuilder.DropColumn(
                name: "AlumnoId",
                table: "NotaAlumnos");

            migrationBuilder.DropColumn(
                name: "FechaNota",
                table: "NotaAlumnos");

            migrationBuilder.RenameTable(
                name: "NotaAlumnos",
                newName: "NotaAlumno");

            migrationBuilder.RenameColumn(
                name: "AsignaturaId",
                table: "NotaAlumno",
                newName: "Dni");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "NotaAlumno",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotaAlumno",
                table: "NotaAlumno",
                column: "NotaAlumnoId");
        }
    }
}
