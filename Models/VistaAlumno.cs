using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIAlumnos2026.Models
{
    public class VistaAlumno
    {
        public int AlumnoId { get; set; }
        public string? NombreCompleto { get; set; }     
        public int Dni {get; set; }  
        public Sexo? Sexo {get; set;}   
        public string? Domicilio {get;set;}
        public int NotaIEv1 { get; set; }  //NOTA 0 EQUIVALE A AUSENTE
        public int NotaIEv2 { get; set; }  
        public int NotaIEv3 { get; set; }  
        public int NotaIEv4 { get; set; }  
        public int NotaRec1 { get; set; }  
        public int NotaRec2 { get; set; }
        public int NotaIEFI { get; set; }
        public int NotaRIEFI { get; set; }      
    }
}