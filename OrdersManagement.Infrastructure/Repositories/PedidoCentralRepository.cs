using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure.Persistence;

namespace OrdersManagement.Infrastructure.Repositories
{
    public class PedidoCentralRepository : IPedidoCentralRepository
    {
        private readonly AppDbContext _context;
        public PedidoCentralRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoCentral> CreatePedidoCentralAsync(PedidoCentral pedidoCentral)
        {
            _context.PedidosCentral.Add(pedidoCentral);
            await _context.SaveChangesAsync();
            return pedidoCentral;
        }

        public async Task<bool> DeletePedidoCentralAsync(int id)
        {
            var pedidoCentral = await _context.PedidosCentral.FindAsync(id);
            if (pedidoCentral == null)
            {
                return false;
            }

            _context.PedidosCentral.Remove(pedidoCentral);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PedidoCentral>> GetAllPedidosCentralAsync()
        {
            return await _context.PedidosCentral.ToListAsync();
        }

        public async Task<PedidoCentral?> GetPedidoCentralByIdAsync(int id)
        {
            return await _context.PedidosCentral.FindAsync(id);
        }

        public async Task<PedidoCentral> UpdatePedidoCentralAsync(PedidoCentral pedidoCentral)
        {
            var existingPedidoCentral = await _context.PedidosCentral.FindAsync(pedidoCentral.Id);
            if (existingPedidoCentral == null)
            {
                throw new KeyNotFoundException($"Pedido Central com ID {pedidoCentral.Id} não encontrado.");
            }

            _context.Entry(existingPedidoCentral).CurrentValues.SetValues(pedidoCentral);
            await _context.SaveChangesAsync();
            return existingPedidoCentral;
        }
    }
}