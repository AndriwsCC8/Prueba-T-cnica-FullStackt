using GestionVentas.Application.Interfaces;
using GestionVentas.Application.Models;
using GestionVentas.Domain.Entities;
using GestionVentas.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionVentas.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<UsuarioIdentity> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new UsuarioIdentity
            {
                UserName = request.Email,
                Email = request.Email,
                Nombre = request.Nombre
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Usuario registrado correctamente"
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return new AuthResponse { IsSuccess = false, Message = "Usuario no encontrado" };

            var valid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!valid)
                return new AuthResponse { IsSuccess = false, Message = "Credenciales inválidas" };

            var token = await GenerateTokenAsync(user);

            return new AuthResponse
            {
                IsSuccess = true,
                Token = token,
                Message = "Login exitoso"
            };
        }

        private async Task<string> GenerateTokenAsync(UsuarioIdentity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("nombre", user.Nombre)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

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
