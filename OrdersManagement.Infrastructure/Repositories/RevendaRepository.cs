using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure.Persistence;

namespace OrdersManagement.Infrastructure.Repositories
{
    public class RevendaRepository : IRevendaRepository
    {
        private readonly AppDbContext _context;
        public RevendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Revenda> CreateRevendaAsync(Revenda revenda)
        {
            _context.Revendas.Add(revenda);
            await _context.SaveChangesAsync();
            return revenda;
        }

        public async Task<bool> DeleteRevendaAsync(int id)
        {
            var revenda = await _context.Revendas.FindAsync(id);
            if (revenda == null)
            {
                return false;
            }

            _context.Revendas.Remove(revenda);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Revenda>> GetAllRevendasAsync()
        {
            return await _context.Revendas
                .Include(r => r.Contatos)
                .Include(r => r.Enderecos)
                .Include(r => r.Telefones)
                .ToListAsync();
        }

        public async Task<Revenda?> GetRevendaByIdAsync(int id)
        {
            return await _context.Revendas.FindAsync(id);
        }

        public async Task<Revenda> UpdateRevendaAsync(Revenda revenda)
        {
            var existingRevenda = await _context.Revendas.FindAsync(revenda.Id);
            if (existingRevenda == null)
            {
                throw new KeyNotFoundException($"Revenda com ID {revenda.Id} não encontrado.");
            }

            _context.Entry(existingRevenda).CurrentValues.SetValues(revenda);
            await _context.SaveChangesAsync();
            return existingRevenda;
        }
    }
}