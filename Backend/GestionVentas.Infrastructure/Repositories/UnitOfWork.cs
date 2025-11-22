using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using GestionVentas.Infrastructure.Data;

namespace GestionVentas.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        // Repositorios genéricos
#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IGenericRepository<Producto>? Productos { get; private set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IGenericRepository<Cliente>? Clientes { get; private set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IGenericRepository<Venta>? Ventas { get; private set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public IGenericRepository<DetalleVenta>? DetallesVenta { get; private set; }
#pragma warning restore CS8766 // La nulabilidad de los tipos de referencia del tipo de valor devuelto no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).

        // Inicializar repos genéricos
        public void InitializeRepositories()
        {
            Productos = new GenericRepository<Producto>(_context);
            Clientes = new GenericRepository<Cliente>(_context);
            Ventas = new GenericRepository<Venta>(_context);
            DetallesVenta = new GenericRepository<DetalleVenta>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
