using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAlumnos2026.Models;

namespace APIAlumnos2026.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotaAlumnoscontroller : ControllerBase
    {
        private readonly Context _context;

        public NotaAlumnoscontroller(Context context)
        {
            _context = context;
        }

       [HttpGet]
public async Task<ActionResult<IEnumerable<VistaNotaAlumno>>> GetNotaAlumnos()

{
    var vistaNotasAlumnos = await _context.NotaAlumnos
        .Include(a => a.Asignatura)
        .Include(a => a.Alumno)
        .OrderBy(n => n.Alumno!.NombreCompleto)
        .Select(n => new VistaNotaAlumno
        {
            NotaAlumnoId = n.NotaAlumnoId,
            AlumnoId = n.AlumnoId,
            NombreCompleto = n.Alumno != null ? n.Alumno.NombreCompleto : "",
            AsignaturaID = n.AsignaturaId,
            AsignaturaNombre = n.Asignatura != null ? n.Asignatura.Descripcion : "",
            FechaString = n.FechaNota.ToString("dd/MM/yyyy"),
            FechaStringInput = n.FechaNota.ToString("yyyy-MM-dd"),
            Dni = n.Alumno != null ? n.Alumno.Dni : 0,
            Nota = n.Nota
        })
        .ToListAsync();

    return vistaNotasAlumnos;
}
    
        // GET: api/NotaAlumnoscontroller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaAlumno>> GetNotaAlumno(int id)
        {
            var notaAlumno = await _context.NotaAlumnos.FindAsync(id);

            if (notaAlumno == null)
            {
                return NotFound();
            }

            return notaAlumno;
        }

        // PUT: api/NotaAlumnoscontroller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
[HttpPut("{id}")]
public async Task<IActionResult> PutNotaAlumno(int id, NotaAlumno notaAlumno)
{
    if (id != notaAlumno.NotaAlumnoId)
    {
        return BadRequest();
    }

    // Buscar nota original
    var notaOriginal = await _context.NotaAlumnos
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.NotaAlumnoId == id);

    if (notaOriginal == null)
    {
        return NotFound();
    }

    // Guardar historial si cambió la nota
    if (notaOriginal.Nota != notaAlumno.Nota)
    {
        HistorialNotaAlumno historial = new HistorialNotaAlumno()
        {
            NotaAlumnoId = notaAlumno.NotaAlumnoId,
            CampoModificado = "Nota",
            ValorAnterior = notaOriginal.Nota.ToString(),
            ValorNuevo = notaAlumno.Nota.ToString(),
            FechaCambio = DateTime.Now
        };

        _context.HistorialNotaAlumno.Add(historial);
    }

    // Actualizar nota
    _context.Entry(notaAlumno).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!NotaAlumnoExists(id))
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

        // POST: api/NotaAlumnoscontroller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotaAlumno>> PostNotaAlumno(NotaAlumno notaAlumno)
        {
            _context.NotaAlumnos.Add(notaAlumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotaAlumno", new { id = notaAlumno.NotaAlumnoId }, notaAlumno);
        }

        // DELETE: api/NotaAlumnoscontroller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotaAlumno(int id)
        {
            var notaAlumno = await _context.NotaAlumnos.FindAsync(id);
            if (notaAlumno == null)
            {
                return NotFound();
            }

            _context.NotaAlumnos.Remove(notaAlumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotaAlumnoExists(int id)
        {
            return _context.NotaAlumnos.Any(e => e.NotaAlumnoId == id);
        }
    }
}
