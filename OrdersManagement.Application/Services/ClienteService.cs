namespace OrdersManagement.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrdersManagement.Application.Interfaces.Repositories;
    using OrdersManagement.Application.Interfaces.Services;
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> CreateClienteAsync(ClienteDTO cliente)
        {
            ArgumentNullException.ThrowIfNull(cliente, nameof(cliente));
            var clienteDb = await _clienteRepository.GetClienteByIdAsync(cliente.Id);
            if (clienteDb != null)
            {
                throw new InvalidOperationException($"Já existe um cliente com o ID {cliente.Id}.");
            }

            var clienteCriado = await _clienteRepository.CreateClienteAsync((Cliente)cliente);
            if (clienteCriado == null)
            {
                throw new InvalidOperationException("Falha ao criar o cliente");
            }
            return clienteCriado;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var clienteDb = await _clienteRepository.GetClienteByIdAsync(id);
            if (clienteDb == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            }

            var resultado = await _clienteRepository.DeleteClienteAsync(id);
            if (!resultado)
            {
                throw new InvalidOperationException("Falha ao deletar o cliente");
            }
            return resultado;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllClientesAsync()
        {
            var clientes = await _clienteRepository.GetAllClientesAsync();
            if (clientes == null || !clientes.Any())
            {
                throw new KeyNotFoundException("Nenhum cliente encontrado.");
            }
            return clientes.Select(cliente => new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
            });
        }

        public async Task<ClienteDTO> GetClienteByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            }
            return cliente;
        }

        public async Task<ClienteDTO> UpdateClienteAsync(ClienteDTO cliente)
        {
            ArgumentNullException.ThrowIfNull(cliente, nameof(cliente));
            var clienteDb = await _clienteRepository.GetClienteByIdAsync(cliente.Id);
            if (clienteDb == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {cliente.Id} não encontrado.");
            }

            var clienteAtualizado = await _clienteRepository.UpdateClienteAsync(cliente);
            if (clienteAtualizado == null)
            {
                throw new InvalidOperationException("Falha ao atualizar o cliente");
            }
            return clienteAtualizado;
        }
    }
}