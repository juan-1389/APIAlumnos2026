using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIAlumnos2026.Models;


public class Alumno
{   
[Key]

public int AlumnoId { get; set; }   

public string? NombreCompleto { get; set; }
 
public int Dni { get; set; } 

public Sexo Sexo { get; set; }

public string? Domicilio { get; set; }
[JsonIgnore]
public ICollection<NotaAlumno>? Notas { get; set; }

}
