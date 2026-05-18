using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAlumnos2026.Models;
using Microsoft.IdentityModel.Tokens;
using APIAlumnos2026.ClasesVistas;

namespace APIAlumnos2026.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private readonly Context _context;

        public ResultadosController(Context context)
        {
            _context = context;
        }

        // GET: api/Resultados
        [HttpGet]
        public async Task<ActionResult<ResultadoAlumnos>> GetResultadoInicial()
        {
            ResultadoAlumnos resultado = new ResultadoAlumnos();

            // Obtengo Promedio de Notas

            var sumaNotas = _context.NotaAlumnos.Sum(n => n.Nota);
            var cantidadAlumnos = _context.NotaAlumnos.Count();

            resultado.Promedio = decimal.Round(sumaNotas / cantidadAlumnos, 2);

            // Obtengo Nota Mas Alta

            var notaMasAlta = _context.NotaAlumnos.Max(n => n.Nota);

            resultado.NotaMasAlta = notaMasAlta;

            // Obtengo Nota Mas Baja

            resultado.NotaMasBaja = _context.NotaAlumnos.Min(n => n.Nota);

// FALTA
            // Obtener Alumno nota mas alta
            // Obtener Alumno nota mas baja
            var alumnosNotaMasAlta = _context.NotaAlumnos
                                    .Where(n => n.Nota == resultado.NotaMasAlta)
                                    // .Select(n => n.NombreAlumno)
                                    .ToList();
            
            // for (int i = 0; i < alumnosNotaMasAlta.Count(); i++)
            // {
            //     var NombreAlumno = alumnosNotaMasAlta[i];
            // }

            foreach (var alumnoNotaMasAlta in alumnosNotaMasAlta)
            {
                resultado.AlumnoNotaMasAlta += alumnoNotaMasAlta.AlumnoId + " ; ";
            }
            
            var alumnosNotaMasBaja = _context.NotaAlumnos
                                    .Where(n => n.Nota == resultado.NotaMasBaja)
                                    .Select(n => n.AlumnoId)
                                    .ToList();
            
            foreach (var alumnoNotaMasBaja in alumnosNotaMasBaja)
            {
                resultado.AlumnoNotaMasBaja += alumnoNotaMasBaja + " ; ";
            }
            
            // Obtengo Cantidad Aprobados

            resultado.CantidadAprobados = _context.NotaAlumnos.Where(n => n.Nota >= 6).Count();

            // Obtengo Cantidad Desaprobados
            
            resultado.CantidadDesaprobados = _context.NotaAlumnos.Where(n => n.Nota < 6).Count();

            // Resolvemos Grupo

            if(resultado.Promedio >= 6)
            {
                resultado.EstadoDeGrupo = "Grupo Aprobado";
            }
            else 
            {
                resultado.EstadoDeGrupo = "Grupo en Riesgo";
            }

            var alumnoMax = _context.NotaAlumnos
            .Include (a => a.Alumno)
            .OrderByDescending(a => a.Nota)
            .FirstOrDefault();

            var alumnoMin = _context.NotaAlumnos
            .Include (a => a.Alumno)
            .OrderBy(a => a.Nota)
            .FirstOrDefault();

            resultado.AlumnoNotaMasAlta = alumnoMax?.Alumno?.NombreCompleto ??"Sin nombre";
            resultado.AlumnoNotaMasBaja = alumnoMin?.Alumno?.NombreCompleto ??"Sin nombre";

            return resultado;
        }
    }
}
