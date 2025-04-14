namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;

    public interface IRevendaRepository
    {
        Task<Revenda?> GetRevendaByIdAsync(int id);
        Task<IEnumerable<Revenda>> GetAllRevendasAsync();
        Task<Revenda> CreateRevendaAsync(Revenda revenda);
        Task<Revenda> UpdateRevendaAsync(Revenda revenda);
        Task<bool> DeleteRevendaAsync(int id);
        Task<PedidoClienteResponseDTO> CreatePedidoClienteAsync(int id, PedidoClienteRequestDTO pedidoCliente);
    }
}