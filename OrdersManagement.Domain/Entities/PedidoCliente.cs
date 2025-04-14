using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.Entities
{
    public class PedidoCliente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now.ToUniversalTime();
        public string NumeroPedido { get; set; } = Guid.NewGuid().ToString("N");
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int RevendaId { get; set; }
        public Revenda? Revenda { get; set; }
        
        public ICollection<ProdutoPedidoCliente>? ProdutosPedidoCliente { get; set; }
    }
}