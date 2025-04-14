using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IPedidoCentralService
    {
        Task<PedidoCentral> CreatePedidoPendenteAsync(int revendaId, IEnumerable<ProdutoPedidoCentralDTO> produtos);
        Task MarkAsEnviadoAsync(int pedidoCentralId);
    }
}