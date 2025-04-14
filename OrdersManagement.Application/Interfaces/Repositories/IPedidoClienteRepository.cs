namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;

    public interface IPedidoClienteRepository
    {
        Task<IEnumerable<PedidoClienteResponseDTO>> GetAllPendentesAsync(int revendaId);
        Task<PedidoCliente> GetByIdAsync(int pedidoClienteId);
        Task<PedidoCliente> UpdateAsync(PedidoCliente pedidoCliente);
    }
}