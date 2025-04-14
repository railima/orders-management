using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Application.Services
{
    public class CentralService : ICentralService
    {
        public async Task<PedidoCentralResponseDTO> CreatePedidoCentralAsync(PedidoCentralRequestDTO pedidoCentral)
        {
            ArgumentNullException.ThrowIfNull(pedidoCentral, nameof(pedidoCentral));

            await Task.Delay(1000);

            bool disponivel = true; // Simulando disponibilidade. Para simular indisponibilidade, altere para false.
            if (!disponivel)
            {
                throw new HttpRequestException("API indisponível no momento.");
            }

            return new PedidoCentralResponseDTO
            {
                Id = 1,
                ProdutosPedidoCentral = pedidoCentral.Itens
            };
        }
    }
}