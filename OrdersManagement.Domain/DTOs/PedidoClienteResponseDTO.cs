using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoClienteResponseDTO
    {
        public int Id { get; set; }
        public string? NumeroPedido { get; set; }
        public StatusPedido? Status { get; set; }

        public ICollection<ProdutoPedidoClienteDTO>? ProdutosPedidoCliente { get; set; }
        
        public static implicit operator PedidoClienteResponseDTO(PedidoCliente entity)
        {
            return new PedidoClienteResponseDTO
            {
                Id = entity.Id,
                NumeroPedido = entity.NumeroPedido,
                ProdutosPedidoCliente = entity.ProdutosPedidoCliente?.Select(p => (ProdutoPedidoClienteDTO)p).ToList() ?? new List<ProdutoPedidoClienteDTO>()
            };
        }
    }
}