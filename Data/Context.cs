using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIAlumnos2026.Models;
using Microsoft.Identity.Client;

public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)

            {}
    
            public DbSet<APIAlumnos2026.Models.Alumno> Alumnos {get ; set; } = default!;

             public DbSet<APIAlumnos2026.Models.Asignatura> Asignaturas {get ; set; } = default!;

             public DbSet<APIAlumnos2026.Models.Docente> Docentes {get ; set; } = default!;
        
             public DbSet<APIAlumnos2026.Models.NotaAlumno> NotaAlumnos { get; set; } = default!;

             public DbSet<APIAlumnos2026.Models.HistorialNotaAlumno> HistorialNotaAlumno { get; set; } = default!;

             public DbSet<APIAlumnos2026.Models.VistaNotaAlumno> VistaNotaAlumnos { get; set; } = default!;

              public DbSet<APIAlumnos2026.Models.HistorialAlumno> HistorialAlumno { get; set; } = default!;

             public DbSet<APIAlumnos2026.Models.HistorialDocente> HistorialDocente { get; set; } = default!;
    }
