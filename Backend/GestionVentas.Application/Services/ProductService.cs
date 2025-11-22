using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using GestionVentas.Application.DTOs.Product;

namespace GestionVentas.Application.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Producto>> GetAll()
    {
        return _unitOfWork.Productos.GetAllAsync();
    }

    public Task<Producto?> GetById(int id)
    {
        return _unitOfWork.Productos.GetByIdAsync(id);
    }

    public async Task<Producto> Create(ProductCreateDTO dto)
    {
        var product = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Stock = dto.Stock
        };

        await _unitOfWork.Productos.AddAsync(product);
        await _unitOfWork.CompleteAsync();

        return product;
    }

    public async Task<bool> Update(int id, ProductUpdateDTO dto)
    {
        var product = await _unitOfWork.Productos.GetByIdAsync(id);
        if (product == null) return false;

        product.Nombre = dto.Nombre;
        product.Descripcion = dto.Descripcion;
        product.Precio = dto.Precio;
        product.Stock = dto.Stock;

        _unitOfWork.Productos.Update(product);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _unitOfWork.Productos.GetByIdAsync(id);
        if (product == null) return false;

        _unitOfWork.Productos.Remove(product);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
