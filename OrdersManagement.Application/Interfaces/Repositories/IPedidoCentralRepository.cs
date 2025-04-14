namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.Entities;

    public interface IPedidoCentralRepository
    {
        Task<PedidoCentral> CreatePedidoCentralAsync(PedidoCentral pedidoCentral);
        Task<PedidoCentral> GetByIdAsync(int pedidoCentralId);
        Task<PedidoCentral> UpdateAsync(PedidoCentral pedidoCentral);
    }
}