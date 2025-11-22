using GestionVentas.Application.DTOs;

namespace GestionVentas.Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoResponseDTO>> ObtenerTodosAsync();
        Task<ProductoResponseDTO?> ObtenerPorIdAsync(int id);
        Task<ProductoResponseDTO> CrearAsync(ProductoCreateDTO dto);
        Task<bool> ActualizarAsync(int id, ProductoUpdateDTO dto);
        Task<bool> EliminarAsync(int id);
    }
}
