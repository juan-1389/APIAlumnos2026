using System.ComponentModel.DataAnnotations;

namespace APIAlumnos2026.Models;


public class HistorialNotaAlumno
{   
[Key]

public int HistorialNotaAlumnoId { get; set; }   

public int NotaAlumnoId { get; set; } 
public string? CampoModificado { get; set; }

public string? ValorAnterior { get; set; } 

public string? ValorNuevo { get; set; } 


public DateTime FechaCambio { get; set; }

}