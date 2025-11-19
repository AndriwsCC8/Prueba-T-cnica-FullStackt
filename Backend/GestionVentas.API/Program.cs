using GestionVentas.Infrastructure.Context;
using GestionVentas.Domain.Entities; 
using GestionVentas.Application.Interfaces; 
using GestionVentas.Application.Services.Auth; // Usado para referenciar a AuthService
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using GestionVentas.Application.Services.Auth.Models; //conectando con models para que tenga acceso

var builder = WebApplication.CreateBuilder(args);

// 1. Registro de DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. ASP.NET Identity (manejo de usuarios) 
// Usamos Usuario y IdentityRole<int> para coincidir con tu DbContext.
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => 
{
    // Configuraciones de políticas de contraseña (puedes ajustarlas)
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Registro del servicio de autenticacion
// La ambigüedad se resuelve por el 'using GestionVentas.Application.Services.Auth;'
builder.Services.AddScoped<
    GestionVentas.Application.Interfaces.IAuthService   // Implementacion
>();

builder.Services.AddControllers();

// 4. Swagger con soporte para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestionVentas API", Version = "v1" });

    // Boton "Authorize" en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese un token JWT válido",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// 5. Autenticacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });
    
// 6. Middleware
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Los middlewares de autenticacion y autorizacion son obligatorios
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();