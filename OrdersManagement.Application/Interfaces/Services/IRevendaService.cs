using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Application.Interfaces.Services
{
    public interface IRevendaService
    {
        Task<RevendaDTO> GetRevendaByIdAsync(int id);
        Task<IEnumerable<RevendaDTO>> GetAllRevendasAsync();
        Task<RevendaDTO> CreateRevendaAsync(RevendaDTO revenda);
        Task<RevendaDTO> UpdateRevendaAsync(RevendaDTO revenda);
        Task<bool> DeleteRevendaAsync(int id);
    }
}