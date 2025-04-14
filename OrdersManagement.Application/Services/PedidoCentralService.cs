namespace OrdersManagement.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrdersManagement.Application.Interfaces.Repositories;
    using OrdersManagement.Application.Interfaces.Services;
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;

    public class PedidoCentralService : IPedidoCentralService
    {
        private readonly IPedidoCentralRepository _pedidoCentralRepository;
        public PedidoCentralService(IPedidoCentralRepository pedidoCentralRepository)
        {
            _pedidoCentralRepository = pedidoCentralRepository;
        }

        public async Task<PedidoCentralDTO> CreatePedidoCentralAsync(PedidoCentralDTO pedidoCentral)
        {
            ArgumentNullException.ThrowIfNull(pedidoCentral, nameof(pedidoCentral));
            var pedidoCentralDb = await _pedidoCentralRepository.GetPedidoCentralByIdAsync(pedidoCentral.Id);
            if (pedidoCentralDb != null)
            {
                throw new InvalidOperationException($"Já existe um pedidoCentral com o ID {pedidoCentral.Id}.");
            }

            var pedidoCentralCriado = await _pedidoCentralRepository.CreatePedidoCentralAsync((PedidoCentral)pedidoCentral);
            if (pedidoCentralCriado == null)
            {
                throw new InvalidOperationException("Falha ao criar o pedidoCentral");
            }
            return pedidoCentralCriado;
        }

        public async Task<bool> DeletePedidoCentralAsync(int id)
        {
            var pedidoCentralDb = await _pedidoCentralRepository.GetPedidoCentralByIdAsync(id);
            if (pedidoCentralDb == null)
            {
                throw new KeyNotFoundException($"PedidoCentral com ID {id} não encontrado.");
            }

            var resultado = await _pedidoCentralRepository.DeletePedidoCentralAsync(id);
            if (!resultado)
            {
                throw new InvalidOperationException("Falha ao deletar o pedidoCentral");
            }
            return resultado;
        }

        public async Task<IEnumerable<PedidoCentralDTO>> GetAllPedidosCentralAsync()
        {
            var pedidoCentrals = await _pedidoCentralRepository.GetAllPedidosCentralAsync();
            if (pedidoCentrals == null || !pedidoCentrals.Any())
            {
                throw new KeyNotFoundException("Nenhum pedidoCentral encontrado.");
            }
            
            return pedidoCentrals.Select(r => (PedidoCentralDTO)r).ToList();
        }

        public async Task<PedidoCentralDTO> GetPedidoCentralByIdAsync(int id)
        {
            var pedidoCentral = await _pedidoCentralRepository.GetPedidoCentralByIdAsync(id);
            if (pedidoCentral == null)
            {
                throw new KeyNotFoundException($"PedidoCentral com ID {id} não encontrado.");
            }
            return pedidoCentral;
        }

        public async Task<PedidoCentralDTO> UpdatePedidoCentralAsync(PedidoCentralDTO pedidoCentral)
        {
            ArgumentNullException.ThrowIfNull(pedidoCentral, nameof(pedidoCentral));
            var pedidoCentralDb = await _pedidoCentralRepository.GetPedidoCentralByIdAsync(pedidoCentral.Id);
            if (pedidoCentralDb == null)
            {
                throw new KeyNotFoundException($"PedidoCentral com ID {pedidoCentral.Id} não encontrado.");
            }

            var pedidoCentralAtualizado = await _pedidoCentralRepository.UpdatePedidoCentralAsync((PedidoCentral)pedidoCentral);
            if (pedidoCentralAtualizado == null)
            {
                throw new InvalidOperationException("Falha ao atualizar o pedidoCentral");
            }
            return pedidoCentralAtualizado;
        }
    }
}