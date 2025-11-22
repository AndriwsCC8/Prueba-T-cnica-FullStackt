using GestionVentas.Domain.Entities;
using GestionVentas.Domain.Interfaces;

namespace GestionVentas.Application.Services
{
    public class ClienteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientesAsync()
        {
            return await _unitOfWork.Clientes.GetAllAsync();
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(int id)
        {
            return await _unitOfWork.Clientes.GetByIdAsync(id);
        }

        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.CompleteAsync();
            return cliente;
        }

        public async Task<bool> ActualizarClienteAsync(Cliente cliente)
        {
            var existente = await _unitOfWork.Clientes.GetByIdAsync(cliente.Id);
            if (existente == null)
                return false;

            existente.Nombre = cliente.Nombre;
            existente.Email = cliente.Email;
            existente.Telefono = cliente.Telefono;

            _unitOfWork.Clientes.Update(existente);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return false;

            _unitOfWork.Clientes.Delete(cliente);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
