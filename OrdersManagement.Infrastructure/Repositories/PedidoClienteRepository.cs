using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;
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

        public async Task<IEnumerable<PedidoClienteResponseDTO>> GetAllPendentesAsync(int revendaId)
        {
            var pedidosEmMemoria = await _context.PedidosCliente
                .Include(p => p.Revenda)
                .Include(p => p.Cliente)
                .Include(p => p.ProdutosPedidoCliente)
                .Where(p => p.RevendaId == revendaId && p.Status == StatusPedido.Pendente)
                .ToListAsync();

            var pedidoClienteDb = pedidosEmMemoria.Select(p => new PedidoClienteResponseDTO
            {
                Id = p.Id,
                NumeroPedido = p.NumeroPedido,
                ProdutosPedidoCliente = p.ProdutosPedidoCliente?
                    .Select(pp => new ProdutoPedidoClienteDTO
                    {
                        Id = pp.Id,
                        NomeProduto = pp.NomeProduto,
                        Quantidade = pp.Quantidade
                    })
                    .ToList() ?? new List<ProdutoPedidoClienteDTO>()
            })
            .ToList();
            return pedidoClienteDb;
        }

        public async Task<PedidoCliente> GetByIdAsync(int pedidoClienteId)
        {
            var pedidoCliente = await _context.PedidosCliente
                .Include(p => p.ProdutosPedidoCliente)
                .FirstOrDefaultAsync(p => p.Id == pedidoClienteId);

            if (pedidoCliente == null)
            {
                throw new KeyNotFoundException($"Pedido Cliente with ID {pedidoClienteId} not found.");
            }

            return pedidoCliente;
        }

        public async Task<PedidoCliente> UpdateAsync(PedidoCliente pedidoCliente)
        {
            ArgumentNullException.ThrowIfNull(pedidoCliente, nameof(pedidoCliente));
            _context.PedidosCliente.Update(pedidoCliente);
            await _context.SaveChangesAsync();
            return pedidoCliente;
        }
    }
}