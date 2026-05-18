using System.ComponentModel.DataAnnotations;

namespace APIAlumnos2026.ClasesVistas;

public class ResultadoAlumnos 
{

    public decimal Promedio { get; set; }

    public int NotaMasAlta { get; set; }

    public string? AlumnoNotaMasAlta { get; set; }

    public int NotaMasBaja { get; set; }

    public string? AlumnoNotaMasBaja { get; set; }

     public int CantidadAprobados { get; set; }

     public int CantidadDesaprobados { get; set; }

     public string? EstadoDeGrupo { get; set; }

}

      

   
    
