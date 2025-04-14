using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IPedidoClienteService
    {
        Task<PedidoClienteDTO> GetPedidoClienteByIdAsync(int id);
        Task<IEnumerable<PedidoClienteDTO>> GetAllPedidosClienteAsync();
        Task<PedidoClienteDTO> CreatePedidoClienteAsync(PedidoClienteDTO pedidoCliente);
        Task<PedidoClienteDTO> UpdatePedidoClienteAsync(PedidoClienteDTO pedidoCliente);
        Task<bool> DeletePedidoClienteAsync(int id);
    }
}