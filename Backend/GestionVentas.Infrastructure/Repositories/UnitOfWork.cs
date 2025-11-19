using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using GestionVentas.Infrastructure.Context;

namespace GestionVentas.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IGenericRepository<Producto>? _productos;
    private IGenericRepository<Cliente>? _clientes;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Producto> Productos => 
        _productos ??= new GenericRepository<Producto>(_context);

    public IGenericRepository<Cliente> Clientes => 
        _clientes ??= new GenericRepository<Cliente>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}