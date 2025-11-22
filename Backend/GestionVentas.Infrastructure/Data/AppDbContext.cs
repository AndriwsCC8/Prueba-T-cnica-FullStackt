using GestionVentas.Domain.Entities;
using GestionVentas.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GestionVentas.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UsuarioIdentity, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tablas de negocio
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // NECESARIO para Identity

            // Precision decimal
            builder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            builder.Entity<DetalleVenta>()
                .Property(d => d.PrecioUnitario)
                .HasPrecision(18, 2);

            builder.Entity<Venta>()
                .Property(v => v.Total)
                .HasPrecision(18, 2);

            // Relaciones
            builder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
