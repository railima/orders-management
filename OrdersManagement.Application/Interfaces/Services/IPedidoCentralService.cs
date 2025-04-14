using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IPedidoCentralService
    {
        Task<PedidoCentralDTO> GetPedidoCentralByIdAsync(int id);
        Task<IEnumerable<PedidoCentralDTO>> GetAllPedidosCentralAsync();
        Task<PedidoCentralDTO> CreatePedidoCentralAsync(PedidoCentralDTO pedidoCentral);
        Task<PedidoCentralDTO> UpdatePedidoCentralAsync(PedidoCentralDTO pedidoCentral);
        Task<bool> DeletePedidoCentralAsync(int id);
    }
}