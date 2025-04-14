using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IPedidoClienteService
    {
        Task<IEnumerable<PedidoClienteResponseDTO>> GetAllPendentesAsync(int revendaId);
        Task MarkAsEnviadoAsync(int id);
    }
}