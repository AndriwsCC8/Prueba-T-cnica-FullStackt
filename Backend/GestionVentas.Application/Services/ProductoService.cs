using GestionVentas.Application.DTOs;
using GestionVentas.Application.Interfaces;
using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;

namespace GestionVentas.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductoResponseDTO>> ObtenerTodosAsync()
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();

            return productos.Select(p => new ProductoResponseDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock
            });
        }

        public async Task<ProductoResponseDTO?> ObtenerPorIdAsync(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
                return null;

            return new ProductoResponseDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }

        public async Task<ProductoResponseDTO> CrearAsync(ProductoCreateDTO dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock
            };

            await _unitOfWork.Productos.AddAsync(producto);
            await _unitOfWork.CompleteAsync();

            return new ProductoResponseDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }

        public async Task<bool> ActualizarAsync(int id, ProductoUpdateDTO dto)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
                return false;

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;

            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
                return false;

            _unitOfWork.Productos.Remove(producto);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
