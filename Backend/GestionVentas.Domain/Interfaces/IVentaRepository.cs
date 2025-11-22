using GestionVentas.Domain.Entities;

namespace GestionVentas.Domain.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta?> GetVentaCompletaAsync(int id);
        Task<IEnumerable<Venta>> GetVentasCompletaAsync();
    }
}
