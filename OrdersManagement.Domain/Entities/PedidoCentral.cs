using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.Entities
{
    public class PedidoCentral
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public string NumeroPedido { get; set; } = Guid.NewGuid().ToString("N");
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        public int RevendaId { get; set; }
        public Revenda? Revenda { get; set; }
        public ICollection<ProdutoPedidoCentral>? ProdutosPedidoCentral { get; set; }

        public static implicit operator PedidoCentral(PedidoCentralDTO pedidoCentralDTO)
        {
            return new PedidoCentral
            {
                Id = pedidoCentralDTO.Id,
                DataPedido = pedidoCentralDTO.DataPedido,
                NumeroPedido = pedidoCentralDTO.NumeroPedido ?? Guid.NewGuid().ToString("N"),
                RevendaId = pedidoCentralDTO.RevendaId,
                ProdutosPedidoCentral = pedidoCentralDTO.ProdutosPedidoCentral?.Select(p => (ProdutoPedidoCentral)p).ToList() ?? new List<ProdutoPedidoCentral>()
            };
        }
    }
}