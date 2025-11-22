namespace GestionVentas.Application.DTOs
{
    public class VentaDTO
    {
        public int ClienteId { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; } = new();
    }

    public class DetalleVentaDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
