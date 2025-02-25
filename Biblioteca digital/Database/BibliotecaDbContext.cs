using Biblioteca_digital.Database.config;
using Biblioteca_digital.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_digital.Database
{
    public class BibliotecaDbContext : IdentityDbContext<Usuario>
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {

        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        // Configure all the models and constrigs as you need
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureAuthbasic();

            //builder.SetUpDomainContext();

            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
