// GestionVentas.Application/DTOs/LoginResponse.cs
namespace GestionVentas.Application.DTOs
{
    public class LoginResponse
    {
        public required string Token { get; set; }
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}