using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure.Persistence;

namespace OrdersManagement.Infrastructure.Repositories
{
    public class PedidoClienteRepository : IPedidoClienteRepository
    {
        private readonly AppDbContext _context;
        public PedidoClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoCliente> CreatePedidoClienteAsync(PedidoCliente pedidoCliente)
        {
            _context.PedidosCliente.Add(pedidoCliente);
            await _context.SaveChangesAsync();
            return pedidoCliente;
        }

        public async Task<bool> DeletePedidoClienteAsync(int id)
        {
            var pedidoCliente = await _context.PedidosCliente.FindAsync(id);
            if (pedidoCliente == null)
            {
                return false;
            }

            _context.PedidosCliente.Remove(pedidoCliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PedidoCliente>> GetAllPedidosClienteAsync()
        {
            return await _context.PedidosCliente.ToListAsync();
        }

        public async Task<PedidoCliente?> GetPedidoClienteByIdAsync(int id)
        {
            return await _context.PedidosCliente.FindAsync(id);
        }

        public async Task<PedidoCliente> UpdatePedidoClienteAsync(PedidoCliente pedidoCliente)
        {
            var existingPedidoCliente = await _context.PedidosCliente.FindAsync(pedidoCliente.Id);
            if (existingPedidoCliente == null)
            {
                throw new KeyNotFoundException($"Pedido Cliente com ID {pedidoCliente.Id} não encontrado.");
            }

            _context.Entry(existingPedidoCliente).CurrentValues.SetValues(pedidoCliente);
            await _context.SaveChangesAsync();
            return existingPedidoCliente;
        }
    }
}