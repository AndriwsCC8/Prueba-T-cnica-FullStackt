using GestionVentas.Application.DTOs;
using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;

namespace GestionVentas.Application.Services
{
    public class VentaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VentaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Venta> CrearVentaAsync(VentaDTO dto)
        {
            decimal total = 0;
            List<DetalleVenta> detalles = new();

            // Procesar detalles
            foreach (var item in dto.Detalles)
            {
                var producto = await _unitOfWork.Productos.GetByIdAsync(item.ProductoId);
                if (producto == null)
                    throw new Exception($"Producto {item.ProductoId} no existe.");

                if (producto.Stock < item.Cantidad)
                    throw new Exception($"Stock insuficiente para {producto.Nombre}.");

                var detalle = new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio
                };

                total += detalle.Importe;

                detalles.Add(detalle);

                // Descontar stock
                producto.Stock -= item.Cantidad;
                _unitOfWork.Productos.Update(producto);
            }

            var venta = new Venta
            {
                ClienteId = dto.ClienteId,
                Fecha = DateTime.Now,
                Total = total,
                Detalles = detalles
            };

            await _unitOfWork.Ventas.AddAsync(venta);
            await _unitOfWork.CompleteAsync();

            return venta;
        }

        public async Task<IEnumerable<Venta>> ObtenerVentasAsync()
        {
            return await _unitOfWork.Ventas.GetAllAsync();
        }

        public async Task<Venta?> ObtenerVentaPorIdAsync(int id)
        {
            return await _unitOfWork.Ventas.GetByIdAsync(id);
        }
    }
}
