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

        public async Task<PedidoClienteDTO> CreatePedidoClienteAsync(PedidoClienteDTO pedidoCliente)
        {
            ArgumentNullException.ThrowIfNull(pedidoCliente, nameof(pedidoCliente));
            var pedidoClienteDb = await _pedidoClienteRepository.GetPedidoClienteByIdAsync(pedidoCliente.Id);
            if (pedidoClienteDb != null)
            {
                throw new InvalidOperationException($"Já existe um pedidoCliente com o ID {pedidoCliente.Id}.");
            }

            var pedidoClienteCriado = await _pedidoClienteRepository.CreatePedidoClienteAsync((PedidoCliente)pedidoCliente);
            if (pedidoClienteCriado == null)
            {
                throw new InvalidOperationException("Falha ao criar o pedidoCliente");
            }
            return pedidoClienteCriado;
        }

        public async Task<bool> DeletePedidoClienteAsync(int id)
        {
            var pedidoClienteDb = await _pedidoClienteRepository.GetPedidoClienteByIdAsync(id);
            if (pedidoClienteDb == null)
            {
                throw new KeyNotFoundException($"PedidoCliente com ID {id} não encontrado.");
            }

            var resultado = await _pedidoClienteRepository.DeletePedidoClienteAsync(id);
            if (!resultado)
            {
                throw new InvalidOperationException("Falha ao deletar o pedidoCliente");
            }
            return resultado;
        }

        public async Task<IEnumerable<PedidoClienteDTO>> GetAllPedidosClienteAsync()
        {
            var pedidoClientes = await _pedidoClienteRepository.GetAllPedidosClienteAsync();
            if (pedidoClientes == null || !pedidoClientes.Any())
            {
                throw new KeyNotFoundException("Nenhum pedidoCliente encontrado.");
            }
            
            return pedidoClientes.Select(r => (PedidoClienteDTO)r).ToList();
        }

        public async Task<PedidoClienteDTO> GetPedidoClienteByIdAsync(int id)
        {
            var pedidoCliente = await _pedidoClienteRepository.GetPedidoClienteByIdAsync(id);
            if (pedidoCliente == null)
            {
                throw new KeyNotFoundException($"PedidoCliente com ID {id} não encontrado.");
            }
            return pedidoCliente;
        }

        public async Task<PedidoClienteDTO> UpdatePedidoClienteAsync(PedidoClienteDTO pedidoCliente)
        {
            ArgumentNullException.ThrowIfNull(pedidoCliente, nameof(pedidoCliente));
            var pedidoClienteDb = await _pedidoClienteRepository.GetPedidoClienteByIdAsync(pedidoCliente.Id);
            if (pedidoClienteDb == null)
            {
                throw new KeyNotFoundException($"PedidoCliente com ID {pedidoCliente.Id} não encontrado.");
            }

            var pedidoClienteAtualizado = await _pedidoClienteRepository.UpdatePedidoClienteAsync((PedidoCliente)pedidoCliente);
            if (pedidoClienteAtualizado == null)
            {
                throw new InvalidOperationException("Falha ao atualizar o pedidoCliente");
            }
            return pedidoClienteAtualizado;
        }
    }
}