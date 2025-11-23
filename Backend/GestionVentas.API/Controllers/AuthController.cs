// GestionVentas.API/Controllers/AuthController.cs (CORREGIDO)
using GestionVentas.Application.DTOs;
using GestionVentas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var result = await _authService.LoginAsync(model);

        // ¡CORRECCIÓN AQUÍ! Cambiar result.Success por result.IsSuccess
        if (result.IsSuccess) 
        {
            return Ok(result); 
        }
        
        // ¡CORRECCIÓN AQUÍ! Cambiar result.Success por result.IsSuccess
        return Unauthorized(result); 
    }
}