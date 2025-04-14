namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.Entities;

    public interface IPedidoCentralRepository
    {
        Task<PedidoCentral?> GetPedidoCentralByIdAsync(int id);
        Task<IEnumerable<PedidoCentral>> GetAllPedidosCentralAsync();
        Task<PedidoCentral> CreatePedidoCentralAsync(PedidoCentral pedidoCentral);
        Task<PedidoCentral> UpdatePedidoCentralAsync(PedidoCentral pedidoCentral);
        Task<bool> DeletePedidoCentralAsync(int id);
    }
}