using Microsoft.EntityFrameworkCore;
using MiApiLocal.Models;

namespace MiApiLocal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) {}

        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Si querés reglas extra, constraints o seed data, agregalas acá.
            base.OnModelCreating(modelBuilder);
        }
    }
}
