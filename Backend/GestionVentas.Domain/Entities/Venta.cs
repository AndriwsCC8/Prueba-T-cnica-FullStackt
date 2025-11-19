namespace GestionVentas.Domain.Entities;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }

    // Relación con Cliente (Una venta pertenece a un cliente)
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    // Relación con Detalles (Una venta tiene muchos productos)
    public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}