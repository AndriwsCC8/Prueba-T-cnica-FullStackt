namespace GestionVentas.Application.DTOs;

public class LoginRequest
{
    // El nombre de usuario o email con el que el usuario intenta iniciar sesión
    public string? UsernameOrEmail { get; set; }

    // La contraseña del usuario
    public string? Password { get; set; }
}