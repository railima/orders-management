namespace OrdersManagement.Domain.Entities
{
    public class ProdutoPedidoCentral
    {
        public int Id { get; set; }
        public int NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public int PedidoCentralId { get; set; }
        public required PedidoCentral PedidoCentral { get; set; } = null!;
    }
}