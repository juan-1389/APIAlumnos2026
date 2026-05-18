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
    public class Alumnoscontroller : ControllerBase
    {
        private readonly Context _context;

        public Alumnoscontroller(Context context)
        {
            _context = context;
        }

        // GET: api/Alumnoscontroller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetNotaAlumno()
        {
            return await _context.Alumnos.ToListAsync();
        }

        // GET: api/Alumnoscontroller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var Alumno = await _context.Alumnos.FindAsync(id);

            if (Alumno == null)
            {
                return NotFound();
            }

            return Alumno;
        }

        // PUT: api/Alumnoscontroller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno Alumno)
        {
            if (id != Alumno.AlumnoId)
            {
                return BadRequest();
            }

            _context.Entry(Alumno).State = EntityState.Modified;

            try
            {
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

        // POST: api/Alumnoscontroller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostNotaAlumno(Alumno Alumno)
        {
            _context.Alumnos.Add(Alumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotaAlumno", new { id = Alumno.AlumnoId }, Alumno);
        }

        // DELETE: api/Alumnoscontroller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var Alumno = await _context.Alumnos.FindAsync(id);
            if (Alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(Alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.AlumnoId == id);
        }
    }
}
