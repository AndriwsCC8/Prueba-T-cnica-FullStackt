using GestionVentas.Application.DTOs;

namespace GestionVentas.Application.Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteResponseDto>> GetAllAsync();
    Task<ClienteResponseDto?> GetByIdAsync(int id);
    Task<ClienteResponseDto> AddAsync(ClienteDto clienteDto);
    Task<bool> UpdateAsync(int id, ClienteDto clienteDto);
    Task<bool> DeleteAsync(int id);
}