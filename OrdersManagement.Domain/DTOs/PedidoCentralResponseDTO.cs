using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoCentralResponseDTO
    {
        public int Id { get; set; }
        public string? NumeroPedido { get; set; }
        public int RevendaId { get; set; }

        public ICollection<ProdutoPedidoCentralDTO>? ProdutosPedidoCentral { get; set; }
        
        public static implicit operator PedidoCentralResponseDTO(PedidoCentral entity)
        {
            return new PedidoCentralResponseDTO
            {
                Id = entity.Id,
                NumeroPedido = entity.NumeroPedido,
                RevendaId = entity.RevendaId,
                ProdutosPedidoCentral = entity.ProdutosPedidoCentral?.Select(p => (ProdutoPedidoCentralDTO)p).ToList() ?? new List<ProdutoPedidoCentralDTO>()
            };
        }
    }
}