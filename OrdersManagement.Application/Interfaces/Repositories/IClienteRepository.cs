using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Application.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetClienteByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> CreateClienteAsync(Cliente cliente);
        Task<Cliente> UpdateClienteAsync(Cliente cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}