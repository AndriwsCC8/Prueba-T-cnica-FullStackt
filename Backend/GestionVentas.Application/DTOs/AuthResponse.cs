// GestionVentas.Application/DTOs/AuthResponse.cs

namespace GestionVentas.Application.DTOs
{
    public class AuthResponse
    {
        public string? Token { get; set; } // <--- ¡CRÍTICO: Añadir el signo de interrogación!
        public bool IsSuccess { get; set; } 
        public string? Message { get; set; }
    }
}