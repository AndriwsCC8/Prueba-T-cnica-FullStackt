// Backend/GestionVentas.Application/DTOs/RegisterRequest.cs
using System.ComponentModel.DataAnnotations;

namespace GestionVentas.Application.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public required string Email { get; set; }
        
        [Required]
        public required string Password { get; set; }
        
        [Required]
        public required string Nombre { get; set; }
    }
}