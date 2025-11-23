// GestionVentas.Application/Interfaces/IAuthService.cs
using GestionVentas.Application.DTOs;
using System.Threading.Tasks;

namespace GestionVentas.Application.Interfaces
{
    public interface IAuthService
    {
        // CORRECCIÓN: Cambiar LoginResponse por AuthResponse
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request); // Si existe
    }
}