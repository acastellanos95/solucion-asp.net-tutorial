using Microsoft.EntityFrameworkCore;
using Dominio;

namespace Persistencia
{
    public class CursosOnlineContext : DbContext
    {
        public CursosOnlineContext(DbContextOptions options) : base(options)
        {}
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new {ci.InstructorId, ci.CursoId});
        }

        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoInstructor> CursoInstructors { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Precio> Precios { get; set; }
    }
}