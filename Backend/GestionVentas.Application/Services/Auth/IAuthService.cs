using GestionVentas.Application.Services.Auth.Models;

namespace GestionVentas.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
    }
}
