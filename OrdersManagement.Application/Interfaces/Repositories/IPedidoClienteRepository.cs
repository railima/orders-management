namespace OrdersManagement.Application.Interfaces.Repositories
{
    using OrdersManagement.Domain.Entities;

    public interface IPedidoClienteRepository
    {
        Task<PedidoCliente?> GetPedidoClienteByIdAsync(int id);
        Task<IEnumerable<PedidoCliente>> GetAllPedidosClienteAsync();
        Task<PedidoCliente> CreatePedidoClienteAsync(PedidoCliente pedidoCliente);
        Task<PedidoCliente> UpdatePedidoClienteAsync(PedidoCliente pedidoCliente);
        Task<bool> DeletePedidoClienteAsync(int id);
    }
}