using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;
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

        public async Task<PedidoCentral> CreatePedidoAsync(PedidoCentral pedidoCentral)
        {
            ArgumentNullException.ThrowIfNull(pedidoCentral, nameof(pedidoCentral));
            await _context.PedidosCentral.AddAsync(pedidoCentral);
            return await _context.SaveChangesAsync().ContinueWith(t => pedidoCentral);
        }

        public async Task<PedidoCentral> GetByIdAsync(int pedidoCentralId)
        {
            var pedidoCentral = await _context.PedidosCentral
                .Include(p => p.ProdutosPedidoCentral)
                .FirstOrDefaultAsync(p => p.Id == pedidoCentralId);

            if (pedidoCentral == null)
            {
                throw new KeyNotFoundException($"Pedido Central with ID {pedidoCentralId} not found.");
            }

            return pedidoCentral;
        }

        public async Task<PedidoCentral> UpdateAsync(PedidoCentral pedidoCentral)
        {
            ArgumentNullException.ThrowIfNull(pedidoCentral, nameof(pedidoCentral));
            _context.PedidosCentral.Update(pedidoCentral);
            await _context.SaveChangesAsync();
            return pedidoCentral;
        }
    }
}