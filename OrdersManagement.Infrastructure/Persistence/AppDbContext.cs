 using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<PedidoCentral> PedidosCentral { get; set; }
        public DbSet<PedidoCliente> PedidosCliente { get; set; }
        public DbSet<ProdutoPedidoCentral> ProdutosPedidoCentral { get; set; }
        public DbSet<ProdutoPedidoCliente> ProdutosPedidoCliente { get; set; }
        public DbSet<Revenda> Revendas { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);
            
            modelBuilder.Entity<Contato>()
                .HasOne(c => c.Revenda)
                .WithMany(r => r.Contatos)
                .HasForeignKey(c => c.RevendaId);

            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.Revenda)
                .WithMany(r => r.Enderecos)
                .HasForeignKey(e => e.RevendaId);

            modelBuilder.Entity<PedidoCentral>()
                .HasOne(p => p.Revenda)
                .WithMany(r => r.PedidosCentral)
                .HasForeignKey(p => p.RevendaId);

            modelBuilder.Entity<PedidoCliente>()
                .HasOne(p => p.Revenda)
                .WithMany(r => r.PedidosCliente)
                .HasForeignKey(p => p.RevendaId);

            modelBuilder.Entity<ProdutoPedidoCentral>()
                .HasOne(pp => pp.PedidoCentral)
                .WithMany(p => p.ProdutosPedidoCentral)
                .HasForeignKey(pp => pp.PedidoCentralId);

            modelBuilder.Entity<ProdutoPedidoCliente>()
                .HasOne(pp => pp.PedidoCliente)
                .WithMany(p => p.ProdutosPedidoCliente)
                .HasForeignKey(pp => pp.PedidoClienteId);

            modelBuilder.Entity<Telefone>()
                .HasOne(t => t.Revenda)
                .WithMany(r => r.Telefones)
                .HasForeignKey(t => t.RevendaId);
        }
    }
}