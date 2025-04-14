namespace OrdersManagement.Application.Interfaces.Services
{
    using OrdersManagement.Domain.DTOs;

    public interface ICentralService
    {
        Task<PedidoCentralResponseDTO> CreatePedidoCentralAsync(PedidoCentralRequestDTO pedidoCentral);
    }
}