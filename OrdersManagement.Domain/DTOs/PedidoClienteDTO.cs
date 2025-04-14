using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoClienteDTO
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now.ToUniversalTime();
        public string? NumeroPedido { get; set; }

        public int ClienteId { get; set; }
        public int RevendaId { get; set; }

        public ICollection<ProdutoPedidoClienteDTO>? ProdutosPedidoCliente { get; set; }
        
        public static implicit operator PedidoClienteDTO(PedidoCliente entity)
        {
            return new PedidoClienteDTO
            {
                Id = entity.Id,
                DataPedido = entity.DataPedido,
                NumeroPedido = entity.NumeroPedido,
                ClienteId = entity.ClienteId,
                RevendaId = entity.RevendaId,
                ProdutosPedidoCliente = entity.ProdutosPedidoCliente?.Select(p => (ProdutoPedidoClienteDTO)p).ToList() ?? new List<ProdutoPedidoClienteDTO>()
            };
        }
    }
}