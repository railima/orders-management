namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.Entities;

    public interface IPedidoCentralRepository
    {
        Task<PedidoCentral> CreatePedidoAsync(PedidoCentral pedidoCentral);
        Task<PedidoCentral> GetByIdAsync(int pedidoCentralId);
        Task<PedidoCentral> UpdateAsync(PedidoCentral pedidoCentral);
    }
}