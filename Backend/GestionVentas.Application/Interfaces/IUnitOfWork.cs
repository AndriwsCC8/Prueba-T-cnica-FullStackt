using GestionVentas.Domain.Interfaces;

namespace GestionVentas.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IClienteRepository Clientes { get; }

        Task<int> SaveChangesAsync();
    }
}
