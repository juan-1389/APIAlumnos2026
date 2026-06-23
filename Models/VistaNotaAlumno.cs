using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIAlumnos2026.Models
{
    public class VistaNotaAlumno
    {
    
    [Key] 
    
        public int NotaAlumnoId { get; set; }
        public int AlumnoId { get; set; }
        public string? NombreCompleto { get; set; }
        public int AsignaturaID { get; set; }

        public string? AsignaturaNombre { get; set; }
        public string? FechaString {get;set;}
         public string? FechaStringInput {get;set;}
        public int Nota { get; set; }
        public int Dni { get; set; }

         public string? Domicilio {get;set;}
    }
    
    public class VistaPromedioAlumno
    {
        public int AlumnoId { get; set; }
        public string? NombreCompleto { get; set; }
        public decimal Promedio { get; set; }  
        public int Dni {get; set; }  
        // public string? SexoString {get; set;}   
        public string? Domicilio {get;set;}
    }


     public class VistaPromedioAsignatura
    {
        public int AsignaturaId { get; set; }
        public string? AsignaturaNombre { get; set; }
        public decimal Promedio { get; set; }  
    }
}