namespace GestionVentas.Application.Services.Auth.Models
{
    public class RegisterRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
