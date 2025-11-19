using GestionVentas.Domain.Interfaces;

namespace GestionVentas.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IClienteRepository Clientes { get; }

        Task<int> SaveChangesAsync();
    }
}
