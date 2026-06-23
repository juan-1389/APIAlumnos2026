using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAlumnos2026.Migrations
{
    /// <inheritdoc />
    public partial class Mirgacion6CreacionTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VistaNotaAlumnos",
                columns: table => new
                {
                    NotaAlumnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsignaturaID = table.Column<int>(type: "int", nullable: false),
                    AsignaturaNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaStringInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    Dni = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VistaNotaAlumnos", x => x.NotaAlumnoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VistaNotaAlumnos");
        }
    }
}
