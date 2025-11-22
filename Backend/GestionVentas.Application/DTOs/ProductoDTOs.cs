using System.ComponentModel.DataAnnotations;

namespace GestionVentas.Application.DTOs
{
    public class ProductoCreateDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Range(1, 999999)]
        public decimal Precio { get; set; }

        [Range(0, 999999)]
        public int Stock { get; set; }
    }

    public class ProductoUpdateDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Range(1, 999999)]
        public decimal Precio { get; set; }

        [Range(0, 999999)]
        public int Stock { get; set; }
    }

    public class ProductoResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
