using GestionVentas.Application.Services.Auth.Models;

namespace GestionVentas.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
        Task<string?> LoginAsync(DTOs.LoginRequest request);
    }
}
