using System.ComponentModel.DataAnnotations;

namespace APIAlumnos2026.Models;


public class Asignatura
{   
[Key]

public int AsignaturaId { get; set; }  
public string? Descripcion { get; set; } 
public bool Eliminado { get; set; }  

 public virtual ICollection<NotaAlumno>? NotasAlumnos {get; set; }

}