using GestionVentas.Domain.Entities;

namespace GestionVentas.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositorios específicos (solo de lectura de la colección)
    IGenericRepository<Producto> Productos { get; }
    IGenericRepository<Cliente> Clientes { get; }
    // Las ventas son más complejas, se manejarán en el Servicio.

    /// <summary>
    /// Confirma todos los cambios pendientes en la base de datos de forma asíncrona.
    /// </summary>
    /// <returns>El número de objetos escritos en la base de datos.</returns>
    Task<int> CommitAsync();
}