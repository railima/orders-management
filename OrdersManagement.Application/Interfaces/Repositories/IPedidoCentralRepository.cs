namespace OrdersManagement.Application.Interfaces.Repositories
{
    using System.Collections.Generic;
    using OrdersManagement.Domain.Entities;
    using OrdersManagement.Domain.Enums;

    public interface IPedidoCentralRepository
    {
        Task<PedidoCentral> CreatePedidoAsync(PedidoCentral pedidoCentral);
        Task<IEnumerable<PedidoCentral>> GetAllByStatusAsync(StatusPedido emEspera);
        Task<PedidoCentral> GetByIdAsync(int pedidoCentralId);
        Task<PedidoCentral> UpdateAsync(PedidoCentral pedidoCentral);
    }
}