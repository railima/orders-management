using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteDTO> GetClienteByIdAsync(int id);
        Task<IEnumerable<ClienteDTO>> GetAllClientesAsync();
        Task<ClienteDTO> CreateClienteAsync(ClienteDTO cliente);
        Task<ClienteDTO> UpdateClienteAsync(ClienteDTO cliente);
        Task<bool> DeleteClienteAsync(int id);
        Task<IEnumerable<PedidoClienteResponseDTO>> GetPedidosByClienteIdAsync(int id);
    }
}