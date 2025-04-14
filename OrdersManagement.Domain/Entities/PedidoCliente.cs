using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class PedidoCliente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public string NumeroPedido { get; set; } = Guid.NewGuid().ToString("N");
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int RevendaId { get; set; }
        public Revenda? Revenda { get; set; }
        
        public ICollection<ProdutoPedidoCliente>? ProdutosPedidoCliente { get; set; }

        public static implicit operator PedidoCliente(PedidoClienteDTO pedidoClienteDTO)
        {
            return new PedidoCliente
            {
                Id = pedidoClienteDTO.Id,
                DataPedido = pedidoClienteDTO.DataPedido,
                NumeroPedido = pedidoClienteDTO.NumeroPedido ?? Guid.NewGuid().ToString("N"),
                ClienteId = pedidoClienteDTO.ClienteId,
                RevendaId = pedidoClienteDTO.RevendaId,
                ProdutosPedidoCliente = pedidoClienteDTO.ProdutosPedidoCliente?.Select(p => (ProdutoPedidoCliente)p).ToList() ?? new List<ProdutoPedidoCliente>()
            };
        }
    }
}