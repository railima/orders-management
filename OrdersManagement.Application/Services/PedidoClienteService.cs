namespace OrdersManagement.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrdersManagement.Application.Interfaces.Repositories;
    using OrdersManagement.Application.Interfaces.Services;
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;

    public class PedidoClienteService : IPedidoClienteService
    {
        private readonly IPedidoClienteRepository _pedidoClienteRepository;
        public PedidoClienteService(IPedidoClienteRepository pedidoClienteRepository)
        {
            _pedidoClienteRepository = pedidoClienteRepository;
        }

        public async Task<IEnumerable<PedidoClienteResponseDTO>> GetAllPendentesAsync(int revendaId)
        {
            var pedidoClienteDb = await _pedidoClienteRepository.GetAllPendentesAsync(revendaId);
            if (pedidoClienteDb == null)
            {
                throw new KeyNotFoundException($"PedidoCliente com ID {revendaId} não encontrado.");
            }

            return pedidoClienteDb;
        }
    }
}