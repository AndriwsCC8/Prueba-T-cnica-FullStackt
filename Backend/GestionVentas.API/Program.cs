using GestionVentas.Infrastructure.Data;
using GestionVentas.Infrastructure.Repositories;
using GestionVentas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GestionVentas.Application.Services;
using GestionVentas.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// DbContext correcto (ESTE SÍ EXISTE)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ClienteService>();



builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "GestionVentas API", 
        Version = "v1" 
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy =>
        {
            policy.AllowAnyOrigin()       // Este es el Cors, para permitir el frontend fusionarse con el backend 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowVue"); //CORS permitido


app.Run();
