using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IPedidoCentralService
    {
        Task<PedidoCentral> CreatePedidoAsync(int revendaId, StatusPedido status, IEnumerable<ProdutoPedidoCentralDTO> produtos);
        Task MarkAsEnviadoAsync(int pedidoCentralId);
    }
}