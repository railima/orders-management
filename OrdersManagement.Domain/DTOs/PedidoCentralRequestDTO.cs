using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoCentralRequestDTO
    {
        public int RevendaId { get; set; }

        public ICollection<ProdutoPedidoCentralDTO>? Itens { get; set; }
        
        public static implicit operator PedidoCentralRequestDTO(PedidoCentral entity)
        {
            return new PedidoCentralRequestDTO
            {
                RevendaId = entity.RevendaId,
                Itens = entity.ProdutosPedidoCentral?.Select(p => (ProdutoPedidoCentralDTO)p).ToList() ?? new List<ProdutoPedidoCentralDTO>()
            };
        }
    }
}