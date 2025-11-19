namespace GestionVentas.Application.DTOs;

// DTO para crear o actualizar un cliente
public class ClienteDto
{
    // El ID no se incluye en la creación, pero sí puede venir en la actualización
    public int? Id { get; set; } 
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}