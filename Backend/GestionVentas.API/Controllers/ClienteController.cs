using GestionVentas.Application.Services;
using GestionVentas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentas.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _service.ObtenerClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.ObtenerClientePorIdAsync(id);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            var nuevo = await _service.CrearClienteAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cliente cliente)
        {
            cliente.Id = id;
            var ok = await _service.ActualizarClienteAsync(cliente);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.EliminarClienteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
