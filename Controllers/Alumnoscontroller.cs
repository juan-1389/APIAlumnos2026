using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAlumnos2026.Models;

namespace ApiAlumnos2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly Context _context;

        public AlumnosController(Context context)
        {
            _context = context;
        }

        // GET: api/Alumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaAlumno>>> GetAlumnos()
        {
            List<VistaAlumno> VistaAlumnos = new List<VistaAlumno>();

            var alumnos = await _context.Alumnos.OrderBy(n => n.NombreCompleto).ToListAsync();

            foreach (var alumno in alumnos)
            {
                var mostraAlumno = new VistaAlumno
                {
                    AlumnoId = alumno.AlumnoId,
                    NombreCompleto = alumno.NombreCompleto,
                    Dni = alumno.Dni,
                    Domicilio = alumno.Domicilio,
                    Sexo = alumno.Sexo,
                };
                VistaAlumnos.Add(mostraAlumno);

            }

            return VistaAlumnos;
        }

        // GET: api/Alumnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }



        // PUT: api/Alumnos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumno)
        {
            if (id != alumno.AlumnoId)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(alumno.NombreCompleto))
            {
                alumno.NombreCompleto = alumno.NombreCompleto;
            }

            if (!string.IsNullOrEmpty(alumno.Domicilio))
            {
                alumno.Domicilio = alumno.Domicilio;
            }

            var alumnoExiste = await _context.Alumnos.Where(t => t.Dni == alumno.Dni && t.AlumnoId != alumno.AlumnoId).FirstOrDefaultAsync();

            if (alumnoExiste != null)
            {
                return Conflict(new { mensaje = "Ya existe un alumno con ese dni." });
            }

            try
            {

                var alumnoOriginal = _context.Alumnos.Where(n => n.AlumnoId == id).Single();

                //PREGUNTAR QUE CAMBIA
                if (alumnoOriginal.NombreCompleto != alumno.NombreCompleto)
                {
                    var editoAlumno = new HistorialAlumno
                    {
                        AlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "NOMBRE",
                        ValorAnterior = alumnoOriginal.NombreCompleto,
                        ValorNuevo = alumno.NombreCompleto,
                    };
                    _context.HistorialAlumno.Add(editoAlumno);
                }


                if (alumnoOriginal.Dni != alumno.Dni)
                {
                    var editoAlumno = new HistorialAlumno
                    {
                        AlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "DNI",
                        ValorAnterior = alumnoOriginal.Dni.ToString(),
                        ValorNuevo = alumno.Dni.ToString(),
                    };
                    _context.HistorialAlumno.Add(editoAlumno);
                }


                if (alumnoOriginal.Sexo != alumno.Sexo)
                {
                    var editoAlumno = new HistorialAlumno
                    {
                        AlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "SEXO",
                        ValorAnterior = alumnoOriginal.Sexo.ToString(),
                        ValorNuevo = alumno.Sexo.ToString(),
                    };
                    _context.HistorialAlumno.Add(editoAlumno);
                }


                if (alumnoOriginal.Domicilio != alumno.Domicilio)
                {
                    var editoAlumno = new HistorialAlumno
                    {
                        AlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "DOMICILIO",
                        ValorAnterior = alumnoOriginal.Domicilio,
                        ValorNuevo = alumno.Domicilio,
                    };
                    _context.HistorialAlumno.Add(editoAlumno);
                }

                alumnoOriginal.NombreCompleto = alumno.NombreCompleto;
                alumnoOriginal.Dni= alumno.Dni;
                alumnoOriginal.Sexo = alumno.Sexo;
                alumnoOriginal.Domicilio = alumno.Domicilio;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {

            if (!string.IsNullOrEmpty(alumno.NombreCompleto))
            {
                alumno.NombreCompleto = alumno.NombreCompleto;
            }

            if (!string.IsNullOrEmpty(alumno.Domicilio))
            {
                alumno.Domicilio = alumno.Domicilio;
            }
            var alumnoExiste = await _context.Alumnos.Where(t => t.Dni == alumno.Dni).FirstOrDefaultAsync();

            if (alumnoExiste != null)
            {
                return Conflict(new { mensaje = "Ya existe un alumno con ese dni." });
            }

            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlumno", new { id = alumno.AlumnoId }, alumno);
        }

        // DELETE: api/Alumnos/5 esta seccion del aplicativo no se usa el delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.AlumnoId == id);
        }
    }
}
