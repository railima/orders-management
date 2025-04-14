namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.DTOs;

    public interface IPedidoClienteRepository
    {
        Task<IEnumerable<PedidoClienteResponseDTO>> GetAllPendentesAsync(int revendaId);
    }
}