using GestionVentas.Application.Services.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionVentas.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            // Inyección de dependencias: UserManager para Identity y IConfiguration para JWT
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var user = new IdentityUser
            {
                // codigo de creación de usuario 
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            return "Usuario registrado correctamente";
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return "Usuario no encontrado";

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return "Credenciales incorrectas";

            return GenerateToken(user);
        }

        private string GenerateToken(IdentityUser user)
        {
            // Generación de la clave de seguridad JWT usando la configuración
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            ); 

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                // Configura el Token con Issuer, Audience, Claims y la expiración de 2 horas
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
