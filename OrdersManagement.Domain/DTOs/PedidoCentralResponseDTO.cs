using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoCentralDTO
    {
        public int Id { get; set; }
        public string? NumeroPedido { get; set; }
        public int RevendaId { get; set; }

        public ICollection<ProdutoPedidoCentralDTO>? ProdutosPedidoCentral { get; set; }
        
        public static implicit operator PedidoCentralDTO(PedidoCentral entity)
        {
            return new PedidoCentralDTO
            {
                Id = entity.Id,
                NumeroPedido = entity.NumeroPedido,
                RevendaId = entity.RevendaId,
                ProdutosPedidoCentral = entity.ProdutosPedidoCentral?.Select(p => (ProdutoPedidoCentralDTO)p).ToList() ?? new List<ProdutoPedidoCentralDTO>()
            };
        }
    }
}