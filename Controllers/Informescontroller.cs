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
    [Route("api/[controller]")]
    [ApiController]
    public class InformesController : ControllerBase
    {
        private readonly Context _context;

        public InformesController(Context context)
        {
            _context = context;
        }

        [HttpPost("promedioalumnos")]
        public async Task<ActionResult<IEnumerable<VistaPromedioAlumno>>> PostPromedioAlumno(FiltroNotaAlumno filtro)
        {
            //INICIAMOS UN LISTADO VACIO DE TIPO VISTAPROMEDIOALUMNO PARA MOSTRAR EN PANTALLA
            List<VistaPromedioAlumno> alumnosMostrar = new List<VistaPromedioAlumno>();

            //BUSCAMOS TODOS LOS ALUMNOS DE LA BASE DE DATOS
            var alumnos = await _context.Alumnos.ToListAsync();

            if (filtro.AlumnoId > 0)
            {
                //QUIERE DECIR QUE FILTRA POR ALGUNA ASIGNATURA EN PARTICULAR
                alumnos = alumnos.Where(a => a.AlumnoId == filtro.AlumnoId).ToList();
            }

            //POR CADA ALUMNO LO RECORREMOS PARA BUSCAR SUS NOTAS 
            foreach (var alumno in alumnos)
            {
                //POR CADA ALUMNO BUSCO LAS NOTAS CORRESPONDIENTES
                var notasAlumno = await _context.NotaAlumnos.Where(a => a.AlumnoId == alumno.AlumnoId).ToListAsync();
                if (filtro.AsignaturaId > 0)
                {
                    //QUIERE DECIR QUE FILTRA POR ALGUNA ASIGNATURA EN PARTICULAR
                    notasAlumno = notasAlumno.Where(a => a.AsignaturaId == filtro.AsignaturaId).ToList();
                }

                DateTime fechaDesde = new DateTime();
                bool fechaDesdeValida = DateTime.TryParse(filtro.FechaDesde, out fechaDesde);

                DateTime fechaHasta = new DateTime();
                bool fechaHastaValida = DateTime.TryParse(filtro.FechaHasta, out fechaHasta);

                if (fechaDesdeValida && fechaHastaValida)
                {
                    fechaHasta = fechaHasta.AddHours(23);
                    fechaHasta = fechaHasta.AddMinutes(59);
                    fechaHasta = fechaHasta.AddSeconds(59);
                    notasAlumno = notasAlumno.Where(t => t.FechaNota >= fechaDesde && t.FechaNota <= fechaHasta).ToList();
                }

                //PREGUNTAMOS SI ESE ALUMNO TIENE AL MENOS UNA NOTA PARA PODER MOSTRARLO
                if (notasAlumno.Count > 0)
                {
                    var alumnoMostrar = new VistaPromedioAlumno
                    {
                        NombreCompleto = alumno.NombreCompleto,
                        Dni = alumno.Dni,
                        Promedio = decimal.Round(Convert.ToDecimal(notasAlumno.Sum(n => n.Nota)) / notasAlumno.Count(), 2)
                    };
                    alumnosMostrar.Add(alumnoMostrar);
                }
            }

            alumnosMostrar = alumnosMostrar.OrderBy(a => a.NombreCompleto).ToList();

            return alumnosMostrar.ToList();
        }


        [HttpPost("promedioasignaturas")]
        public async Task<ActionResult<IEnumerable<VistaPromedioAsignatura>>> PostPromedioAsignatura(FiltroNotaAlumno filtro)
        {
            //INICIAMOS UN LISTADO VACIO DE TIPO VISTAPROMEDIOASIGNATURA PARA MOSTRAR EN PANTALLA
            List<VistaPromedioAsignatura> asignaturasMostrar = new List<VistaPromedioAsignatura>();

            //BUSCAMOS TODAS LAS ASIGNATURAS DE LA BASE DE DATOS
            var asignaturas = await _context.Asignaturas.ToListAsync();

            if (filtro.AsignaturaId > 0)
            {
                //QUIERE DECIR QUE FILTRA POR ALGUNA ASIGNATURA EN PARTICULAR
                asignaturas = asignaturas.Where(a => a.AsignaturaId == filtro.AsignaturaId).ToList();
            }

            //POR CADA ASIGNATURA LO RECORREMOS PARA BUSCAR SUS NOTAS 
            foreach (var asignatura in asignaturas)
            {
                //POR CADA ASIGNATURA BUSCO LAS NOTAS CORRESPONDIENTES
                var notasAsignatura = await _context.NotaAlumnos.Where(a => a.AsignaturaId == asignatura.AsignaturaId).ToListAsync();

                if (filtro.AlumnoId > 0)
                {
                    //QUIERE DECIR QUE FILTRA POR ALGUN ALUMNO EN PARTICULAR
                    notasAsignatura = notasAsignatura.Where(a => a.AlumnoId == filtro.AlumnoId).ToList();
                }

                DateTime fechaDesde = new DateTime();
                bool fechaDesdeValida = DateTime.TryParse(filtro.FechaDesde, out fechaDesde);

                DateTime fechaHasta = new DateTime();
                bool fechaHastaValida = DateTime.TryParse(filtro.FechaHasta, out fechaHasta);

                if (fechaDesdeValida && fechaHastaValida)
                {
                    fechaHasta = fechaHasta.AddHours(23);
                    fechaHasta = fechaHasta.AddMinutes(59);
                    fechaHasta = fechaHasta.AddSeconds(59);
                    notasAsignatura = notasAsignatura.Where(t => t.FechaNota >= fechaDesde && t.FechaNota <= fechaHasta).ToList();
                }

                //PREGUNTAMOS SI ESA ASIGNATURA TIENE AL MENOS UNA NOTA PARA PODER MOSTRARLO
                if (notasAsignatura.Count > 0)
                {
                    var asignaturaMostrar = new VistaPromedioAsignatura
                    {
                        AsignaturaId = asignatura.AsignaturaId,
                        AsignaturaNombre = asignatura.Descripcion,
                        Promedio = decimal.Round(Convert.ToDecimal(notasAsignatura.Sum(n => n.Nota)) / notasAsignatura.Count(), 2)
                    };
                    asignaturasMostrar.Add(asignaturaMostrar);
                }
            }

            asignaturasMostrar = asignaturasMostrar.OrderBy(a => a.AsignaturaNombre).ToList();

            return asignaturasMostrar.ToList();
        }



        [HttpGet("HistorialNotas/{id}")]
        public async Task<ActionResult<IEnumerable<VistaHistorialNotaAlumno>>> GetHistorial(int id)
        {
            //INICIAMOS UN LISTADO VACIO DE TIPO VISTAPROMEDIOALUMNO PARA MOSTRAR EN PANTALLA
            List<VistaHistorialNotaAlumno> asignaturasMostrar = new List<VistaHistorialNotaAlumno>();

            //BUSCAMOS TODOS LOS ALUMNOS DE LA BASE DE DATOS
            var historiales = await _context.HistorialNotaAlumno.Where(a => a.NotaAlumnoId == id).ToListAsync();


            historiales = historiales.OrderByDescending(a => a.FechaCambio).ToList();

            //POR CADA ALUMNO LO RECORREMOS PARA BUSCAR SUS NOTAS 
            foreach (var historial in historiales)
            {

                var alumnoMostrar = new VistaHistorialNotaAlumno
                {
                    FechaCambioString = historial.FechaCambio.ToString("dd/MM/yyyy HH:mm"),
                    CampoModificado = historial.CampoModificado,
                    ValorAnterior = historial.ValorAnterior,
                    ValorNuevo = historial.ValorNuevo
                };
                asignaturasMostrar.Add(alumnoMostrar);

            }

            return asignaturasMostrar.ToList();
        }


        [HttpGet("HistorialAlumno/{id}")]
        public async Task<ActionResult<IEnumerable<VistaHistorialAlumno>>> GetHistorialAlumno(int id)
        {
            //INICIAMOS UN LISTADO VACIO DE TIPO VISTAPROMEDIOALUMNO PARA MOSTRAR EN PANTALLA
            List<VistaHistorialAlumno> datosMostrar = new List<VistaHistorialAlumno>();

            //BUSCAMOS TODOS LOS ALUMNOS DE LA BASE DE DATOS
            var historiales = await _context.HistorialAlumno.Where(a => a.AlumnoId == id).ToListAsync();


            historiales = historiales.OrderByDescending(a => a.FechaCambio).ToList();

            //POR CADA ALUMNO LO RECORREMOS PARA BUSCAR SUS NOTAS 
            foreach (var historial in historiales)
            {

                var alumnoMostrar = new VistaHistorialAlumno
                {
                    FechaCambioString = historial.FechaCambio.ToString("dd/MM/yyyy HH:mm"),
                    CampoModificado = historial.CampoModificado,
                    ValorAnterior = historial.ValorAnterior,
                    ValorNuevo = historial.ValorNuevo
                };
                datosMostrar.Add(alumnoMostrar);

            }

            return datosMostrar.ToList();
        }

         [HttpGet("HistorialDocente/{id}")]
        public async Task<ActionResult<IEnumerable<VistaHistorialDocente>>> GetHistorialDocente(int id)
        {
            //INICIAMOS UN LISTADO VACIO DE TIPO VISTAPROMEDIOALUMNO PARA MOSTRAR EN PANTALLA
            List<VistaHistorialDocente> datosMostrar = new List<VistaHistorialDocente>();

            //BUSCAMOS TODOS LOS ALUMNOS DE LA BASE DE DATOS
            var historiales = await _context.HistorialDocente.Where(a => a.DocenteId == id).ToListAsync();


            historiales = historiales.OrderByDescending(a => a.FechaCambio).ToList();

            //POR CADA ALUMNO LO RECORREMOS PARA BUSCAR SUS NOTAS 
            foreach (var historial in historiales)
            {

                var alumnoMostrar = new VistaHistorialDocente
                {
                    FechaCambioString = historial.FechaCambio.ToString("dd/MM/yyyy HH:mm"),
                    CampoModificado = historial.CampoModificado,
                    ValorAnterior = historial.ValorAnterior,
                    ValorNuevo = historial.ValorNuevo
                };
                datosMostrar.Add(alumnoMostrar);

            }

            return datosMostrar.ToList();
        }


        // PUT: api/NotaAlumnos/5
        [HttpPut("HistorialNotas/{id}")]
        public async Task<IActionResult> PutNotaAlumno(int id, NotaAlumno notaAlumno)
        {
            if (id != notaAlumno.NotaAlumnoId)
            {
                return BadRequest();
            }

            try
            {
                var notaOriginal = await _context.NotaAlumnos
                    .Where(n => n.NotaAlumnoId == id)
                    .SingleOrDefaultAsync();

                if (notaOriginal == null)
                {
                    return NotFound();
                }

                // CAMBIO DE ALUMNO
                if (notaOriginal.AlumnoId != notaAlumno.AlumnoId)
                {
                    var historial = new HistorialNotaAlumno
                    {
                        NotaAlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "ALUMNO",
                        ValorAnterior = notaOriginal.AlumnoId.ToString(),
                        ValorNuevo = notaAlumno.AlumnoId.ToString()
                    };

                    _context.HistorialNotaAlumno.Add(historial);
                }

                // CAMBIO DE ASIGNATURA
                if (notaOriginal.AsignaturaId != notaAlumno.AsignaturaId)
                {
                    var historial = new HistorialNotaAlumno
                    {
                        NotaAlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "ASIGNATURA",
                        ValorAnterior = notaOriginal.AsignaturaId.ToString(),
                        ValorNuevo = notaAlumno.AsignaturaId.ToString()
                    };

                    _context.HistorialNotaAlumno.Add(historial);
                }

                // CAMBIO DE NOTA
                if (notaOriginal.Nota != notaAlumno.Nota)
                {
                    var historial = new HistorialNotaAlumno
                    {
                        NotaAlumnoId = id,
                        FechaCambio = DateTime.Now,
                        CampoModificado = "NOTA",
                        ValorAnterior = notaOriginal.Nota.ToString(),
                        ValorNuevo = notaAlumno.Nota.ToString()
                    };

                    _context.HistorialNotaAlumno.Add(historial);
                }

                // ACTUALIZAMOS DATOS
                notaOriginal.AlumnoId = notaAlumno.AlumnoId;
                notaOriginal.AsignaturaId = notaAlumno.AsignaturaId;
                notaOriginal.Nota = notaAlumno.Nota;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar");
            }

            return NoContent();
        }


    }

}