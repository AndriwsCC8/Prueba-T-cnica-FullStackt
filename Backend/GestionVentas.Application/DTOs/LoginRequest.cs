// GestionVentas.Application/DTOs/LoginRequest.cs
using System.ComponentModel.DataAnnotations;

namespace GestionVentas.Application.DTOs
{
    public class LoginRequest
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}