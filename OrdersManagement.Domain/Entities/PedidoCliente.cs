namespace OrdersManagement.Domain.Entities
{
    public class PedidoCliente
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public string NumeroPedido { get; set; } = Guid.NewGuid().ToString("N");
        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public int RevendaId { get; set; }
        public required Revenda Revenda { get; set; }
        
        public ICollection<ProdutoPedidoCliente>? ProdutosPedidoCliente { get; set; }
    }
}