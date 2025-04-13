namespace OrdersManagement.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public ICollection<PedidoCliente>? Pedidos { get; set; }
    }
}