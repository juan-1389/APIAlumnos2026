using System.ComponentModel.DataAnnotations;

namespace APIAlumnos2026.Models;


public class NotaAlumno
{   
[Key]

public int NotaAlumnoId { get; set; }   

public DateOnly FechaNota { get; set; }

public int Nota { get; set; } 
public int AlumnoId { get; set; }

public int AsignaturaId { get; set; } 

public Alumno? Alumno { get; set; }
public Asignatura? Asignatura { get; set; }

}