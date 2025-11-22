namespace GestionVentas.Application.DTOs;

public class VentaResponse
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public int ClienteId { get; set; }
    public List<DetalleVentaResponse> Detalles { get; set; } = new();
}

public class DetalleVentaResponse
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
