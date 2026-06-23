using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIAlumnos2026.Models
{
      public class FiltroNotaAlumno
    {
        public string? FechaDesde { get; set; }
        public string? FechaHasta { get; set; }
        public int? AsignaturaId { get; set; }
        public int? AlumnoId { get; set; }
    }
}