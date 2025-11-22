using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;
using GestionVentas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionVentas.Infrastructure.Repositories
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private new readonly AppDbContext _context;

        public VentaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Venta?> GetVentaCompletaAsync(int id)
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venta>> GetVentasCompletaAsync()
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();
        }
    }
}
