using Microsoft.AspNetCore.Mvc;
using GestionVentas.Application.Interfaces; // <-- ESTE ES EL USING CRÍTICO QUE RESUELVE EL ERROR
using GestionVentas.Application.DTOs; // Para LoginRequest y RegisterRequest
using System.Threading.Tasks;

namespace GestionVentas.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // Inyección de dependencia de IAuthService
    public AuthController(IAuthService authService) 
    {
        _authService = authService;
    }

    public IAuthService AuthService => _authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await AuthService.LoginAsync(request: request);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Credenciales inválidas.");
        }

        return Ok(new { token });
    }
  
}