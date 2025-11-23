using GestionVentas.Application.Interfaces;
using GestionVentas.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionVentas.Application.DTOs;
using System.Linq; 
using System.Collections.Generic; // Para List<Claim>

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
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                    Token = null 
                };

            return new AuthResponse { IsSuccess = true, Message = "Usuario registrado correctamente", Token = null }; 
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            // CORRECCIÓN: Si el usuario no existe, IsSuccess es FALSE
            if (user == null)
                return new AuthResponse { IsSuccess = false, Message = "Usuario no encontrado", Token = null }; 

            var valid = await _userManager.CheckPasswordAsync(user, request.Password);
            
            // CORRECCIÓN: Si la contraseña es incorrecta, IsSuccess es FALSE
            if (!valid)
                return new AuthResponse { IsSuccess = false, Message = "Contraseña incorrecta", Token = null };

            var token = await GenerateTokenAsync(user);

            return new AuthResponse
            {
                IsSuccess = true,
                Token = token,
                Message = "Login exitoso"
            };
        }

        private Task<string> GenerateTokenAsync(UsuarioIdentity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("nombre", user.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}