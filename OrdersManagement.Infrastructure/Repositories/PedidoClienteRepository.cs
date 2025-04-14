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
            var pedidoClienteDb = await _context.PedidosCliente
                .Include(p => p.Revenda)
                .Include(p => p.Cliente)
                .Where(p => p.RevendaId == revendaId && p.Status == StatusPedido.Pendente)
                .Select(p => new PedidoClienteResponseDTO
                {
                    Id = p.Id,
                    NumeroPedido = p.NumeroPedido,
                    ProdutosPedidoCliente = 
                        (ICollection<ProdutoPedidoClienteDTO>)
                        (p.ProdutosPedidoCliente ?? Enumerable.Empty<ProdutoPedidoCliente>()).Select(pp => new ProdutoPedidoCliente
                        {
                            Id = pp.Id,
                            NomeProduto = pp.NomeProduto,
                            Quantidade = pp.Quantidade
                        }).ToList(),
                })
                .ToListAsync();

            return pedidoClienteDb;
        }
    }
}