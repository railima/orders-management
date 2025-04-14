using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure.Persistence;

namespace OrdersManagement.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<PedidoClienteResponseDTO>> GetPedidosByClienteIdAsync(int id)
        {
            var pedidos = await _context.PedidosCliente
                .Include(p => p.ProdutosPedidoCliente)
                .Where(p => p.ClienteId == id)
                .ToListAsync();

            return pedidos.Select(p => new PedidoClienteResponseDTO
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
            });
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            var existingCliente = await _context.Clientes.FindAsync(cliente.Id);
            if (existingCliente == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {cliente.Id} não encontrado.");
            }

            _context.Entry(existingCliente).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return existingCliente;
        }
    }
}