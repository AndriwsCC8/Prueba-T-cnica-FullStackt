using Microsoft.AspNetCore.Mvc;
using GestionVentas.Application.DTOs;
using GestionVentas.Application.Services;

namespace GestionVentas.API.Controllers;

[Route("api/[controller]")]
[ApiController]
// Nota: La autenticación [Authorize] se agregará después de implementar JWT.
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    // Inyección de Dependencia del Servicio
    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // GET: api/Clientes
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> Get()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(clientes);
    }

    // GET: api/Clientes/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteResponseDto>> Get(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    // POST: api/Clientes
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteResponseDto>> Post([FromBody] ClienteDto clienteDto)
    {
        // Si el modelo recibido no es válido (ej. campos requeridos)
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var nuevoCliente = await _clienteService.AddAsync(clienteDto);
        
        return CreatedAtAction(nameof(Get), new { id = nuevoCliente.Id }, nuevoCliente);
    }

    // PUT: api/Clientes/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] ClienteDto clienteDto)
    {
        if (id != clienteDto.Id) return BadRequest();
        
        var success = await _clienteService.UpdateAsync(id, clienteDto);
        
        if (!success) return NotFound();
        
        return NoContent(); // Indica éxito sin devolver contenido
    }

    // DELETE: api/Clientes/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _clienteService.DeleteAsync(id);
        
        if (!success) return NotFound();
        
        return NoContent();
    }
}