using System.ComponentModel.DataAnnotations;

namespace APIAlumnos2026.Models;


public class Docente
{   
[Key]

public int DocenteId { get; set; }   

public string? NombreCompleto { get; set; } 

public int Dni { get; set; } 

public Sexo Sexo { get;set; }

}

public enum Sexo

{
    Masculino    = 1, 
    Femenino     = 2,
    Otros        = 3, 
  
}
