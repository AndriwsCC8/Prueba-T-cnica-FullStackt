using GestionVentas.Application.DTOs;
using GestionVentas.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentas.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] VentaDTO dto)
        {
            try
            {
                var venta = await _ventaService.CrearVentaAsync(dto);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVentas()
        {
            var ventas = await _ventaService.ObtenerVentasAsync();
            return Ok(ventas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaService.ObtenerVentaPorIdAsync(id);

            if (venta == null)
                return NotFound($"No existe una venta con ID {id}");

            return Ok(venta);
        }
    }
}
