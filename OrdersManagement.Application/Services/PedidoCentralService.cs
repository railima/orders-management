namespace OrdersManagement.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrdersManagement.Application.Interfaces.Repositories;
    using OrdersManagement.Application.Interfaces.Services;
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;
    using OrdersManagement.Domain.Enums;

    public class PedidoCentralService : IPedidoCentralService
    {
        private readonly IPedidoCentralRepository _pedidoCentralRepository;
        public PedidoCentralService(IPedidoCentralRepository pedidoCentralRepository)
        {
            _pedidoCentralRepository = pedidoCentralRepository;
        }

        public Task<PedidoCentral> CreatePedidoPendenteAsync(int revendaId, IEnumerable<ProdutoPedidoCentralDTO> produtos)
        {
            ArgumentNullException.ThrowIfNull(produtos, nameof(produtos));
            var pedidoCentral = new PedidoCentral
            {
                RevendaId = revendaId,
                ProdutosPedidoCentral = produtos.Select(p => new ProdutoPedidoCentral
                {
                    Id = p.Id,
                    NomeProduto = p.NomeProduto,
                    Quantidade = p.Quantidade,
                }).ToList(),
            };

            return _pedidoCentralRepository.CreatePedidoCentralAsync(pedidoCentral);
        }

        public async Task MarkAsEnviadoAsync(int pedidoCentralId)
        {
            var pedidoCentral = await _pedidoCentralRepository.GetByIdAsync(pedidoCentralId);
            if (pedidoCentral == null)
            {
                throw new KeyNotFoundException($"Pedido Central with ID {pedidoCentralId} not found.");
            }
            pedidoCentral.Status = StatusPedido.Enviado;
            await _pedidoCentralRepository.UpdateAsync(pedidoCentral);
        }
    }
}