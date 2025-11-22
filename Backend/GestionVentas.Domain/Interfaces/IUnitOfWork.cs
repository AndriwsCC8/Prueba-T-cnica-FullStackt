using GestionVentas.Domain.Entities;

namespace GestionVentas.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Producto> Productos { get; }
        IGenericRepository<Cliente> Clientes { get; }
        IGenericRepository<Venta> Ventas { get; }
        IGenericRepository<DetalleVenta> DetallesVenta { get; }

        void InitializeRepositories();

        Task<int> CompleteAsync();
    }
}
