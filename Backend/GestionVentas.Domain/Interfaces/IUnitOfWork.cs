using GestionVentas.Domain.Entities;

namespace GestionVentas.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Producto> Productos { get; }
    IGenericRepository<Cliente> Clientes { get; }
    // Nota: Las ventas son más complejas, se manejarán en el Servicio.

    Task<int> CompleteAsync(); // Método para guardar los cambios
}