using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoClienteRequestDTO
    {
        public int ClienteId { get; set; }

        public ICollection<ProdutoPedidoClienteDTO>? ProdutosPedidoCliente { get; set; }
        
        public static implicit operator PedidoClienteRequestDTO(PedidoCliente entity)
        {
            return new PedidoClienteRequestDTO
            {
                ClienteId = entity.ClienteId,
                ProdutosPedidoCliente = entity.ProdutosPedidoCliente?.Select(p => (ProdutoPedidoClienteDTO)p).ToList() ?? new List<ProdutoPedidoClienteDTO>()
            };
        }
    }
}