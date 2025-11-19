using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GestionVentas.Domain.Entities;
using GestionVentas.Infrastructure.Identity;

namespace GestionVentas.Infrastructure.Context
{
    public class ApplicationDbContext :
        IdentityDbContext<UsuarioIdentity, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Producto> Productos { get; set; } = default!;
        public DbSet<Venta> Ventas { get; set; } = default!;
        public DbSet<DetalleVenta> DetalleVentas { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<UsuarioIdentity>()
                .ToTable("Usuarios");
        }
    }
}
