using GestionVentas.Application.DTOs;
using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;

namespace GestionVentas.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteService(IUnitOfWork unitOfWork)
    {
        // Inyección de Dependencia del UnitOfWork (SOLID)
        _unitOfWork = unitOfWork;
    }

    // LISTAR TODOS (READ)
    public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync()
    {
        var clientes = await _unitOfWork.Clientes.GetAllAsync();
        
        // Mapeo de Entidad -> DTO (Para desacoplar)
        return clientes.Select(c => new ClienteResponseDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono
        });
    }

    // OBTENER POR ID (READ)
    public async Task<ClienteResponseDto?> GetByIdAsync(int id)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
        
        if (cliente == null) return null;

        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        };
    }

    // CREAR (CREATE)
    public async Task<ClienteResponseDto> AddAsync(ClienteDto clienteDto)
    {
        // Lógica de Negocio: Podríamos validar que el Email sea único aquí

        var cliente = new Cliente
        {
            Nombre = clienteDto.Nombre!,
            Email = clienteDto.Email!,
            Telefono = clienteDto.Telefono!
        };

        await _unitOfWork.Clientes.AddAsync(cliente);
        await _unitOfWork.CompleteAsync(); // Guardar cambios en la BD

        // Mapeo de vuelta para la respuesta
        return new ClienteResponseDto
        {
            Id = cliente.Id, // El ID se genera después de CompleteAsync()
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        };
    }

    // ACTUALIZAR (UPDATE)
    public async Task<bool> UpdateAsync(int id, ClienteDto clienteDto)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (cliente == null) return false;

        // Actualizar propiedades
        cliente.Nombre = clienteDto.Nombre!;
        cliente.Email = clienteDto.Email!;
        cliente.Telefono = clienteDto.Telefono!;

        _unitOfWork.Clientes.Update(cliente);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    // ELIMINAR (DELETE)
    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (cliente == null) return false;

        _unitOfWork.Clientes.Remove(cliente);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}