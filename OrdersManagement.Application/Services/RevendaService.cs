namespace OrdersManagement.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrdersManagement.Application.Interfaces.Repositories;
    using OrdersManagement.Application.Interfaces.Services;
    using OrdersManagement.Domain.DTOs;
    using OrdersManagement.Domain.Entities;

    public class RevendaService : IRevendaService
    {
        private readonly IRevendaRepository _revendaRepository;
        public RevendaService(IRevendaRepository revendaRepository)
        {
            _revendaRepository = revendaRepository;
        }

        public async Task<RevendaDTO> CreateRevendaAsync(RevendaDTO revenda)
        {
            ArgumentNullException.ThrowIfNull(revenda, nameof(revenda));
            var revendaDb = await _revendaRepository.GetRevendaByIdAsync(revenda.Id);
            if (revendaDb != null)
            {
                throw new InvalidOperationException($"Já existe um revenda com o ID {revenda.Id}.");
            }

            var revendaCriado = await _revendaRepository.CreateRevendaAsync((Revenda)revenda);
            if (revendaCriado == null)
            {
                throw new InvalidOperationException("Falha ao criar o revenda");
            }
            return revendaCriado;
        }

        public async Task<bool> DeleteRevendaAsync(int id)
        {
            var revendaDb = await _revendaRepository.GetRevendaByIdAsync(id);
            if (revendaDb == null)
            {
                throw new KeyNotFoundException($"Revenda com ID {id} não encontrado.");
            }

            var resultado = await _revendaRepository.DeleteRevendaAsync(id);
            if (!resultado)
            {
                throw new InvalidOperationException("Falha ao deletar o revenda");
            }
            return resultado;
        }

        public async Task<IEnumerable<RevendaDTO>> GetAllRevendasAsync()
        {
            var revendas = await _revendaRepository.GetAllRevendasAsync();
            if (revendas == null || !revendas.Any())
            {
                throw new KeyNotFoundException("Nenhum revenda encontrado.");
            }
            
            return revendas.Select(r => (RevendaDTO)r).ToList();
        }

        public async Task<RevendaDTO> GetRevendaByIdAsync(int id)
        {
            var revenda = await _revendaRepository.GetRevendaByIdAsync(id);
            if (revenda == null)
            {
                throw new KeyNotFoundException($"Revenda com ID {id} não encontrado.");
            }
            return revenda;
        }

        public async Task<RevendaDTO> UpdateRevendaAsync(RevendaDTO revenda)
        {
            ArgumentNullException.ThrowIfNull(revenda, nameof(revenda));
            var revendaDb = await _revendaRepository.GetRevendaByIdAsync(revenda.Id);
            if (revendaDb == null)
            {
                throw new KeyNotFoundException($"Revenda com ID {revenda.Id} não encontrado.");
            }

            var revendaAtualizado = await _revendaRepository.UpdateRevendaAsync((Revenda)revenda);
            if (revendaAtualizado == null)
            {
                throw new InvalidOperationException("Falha ao atualizar o revenda");
            }
            return revendaAtualizado;
        }
    }
}