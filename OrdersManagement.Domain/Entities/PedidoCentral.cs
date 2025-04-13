using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.Entities
{
    public class PedidoCentral
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public string NumeroPedido { get; set; } = Guid.NewGuid().ToString("N");
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        public int RevendaId { get; set; }
        public required Revenda Revenda { get; set; }
        public ICollection<ProdutoPedidoCentral>? ProdutosPedidoCentral { get; set; }
    }
}