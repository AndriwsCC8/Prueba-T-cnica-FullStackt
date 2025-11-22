using GestionVentas.Application.DTOs;
using GestionVentas.Domain.Entities;

namespace GestionVentas.Application.Interfaces
{
    public interface IVentaService
    {
        Task<Venta> CrearVentaAsync(VentaDTO dto);
        Task<IEnumerable<Venta>> ObtenerVentasAsync();
        Task<Venta?> ObtenerVentaPorIdAsync(int id);
    }
}
