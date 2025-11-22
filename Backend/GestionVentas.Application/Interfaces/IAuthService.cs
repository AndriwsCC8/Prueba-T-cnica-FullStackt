using GestionVentas.Application.Models;
using System.Threading.Tasks;

namespace GestionVentas.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
