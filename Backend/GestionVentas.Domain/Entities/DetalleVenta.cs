public class DetalleVenta
{
    public int Id { get; set; }

    public int VentaId { get; set; }
    public Venta? Venta { get; set; }   // navegación opcional

    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }  // navegación opcional

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Importe => Cantidad * PrecioUnitario;
}
