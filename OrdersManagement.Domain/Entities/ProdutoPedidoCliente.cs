namespace OrdersManagement.Domain.Entities
{
    public class ProdutoPedidoCliente
    {
        public int Id { get; set; }
        public int NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public int PedidoClienteId { get; set; }
        public required PedidoCliente PedidoCliente { get; set; } = null!;
    }
}