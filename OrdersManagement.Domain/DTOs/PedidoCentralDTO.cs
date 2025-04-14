using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Domain.DTOs
{
    public class PedidoCentralDTO
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now.ToUniversalTime();
        public string? NumeroPedido { get; set; }
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;

        public int RevendaId { get; set; }

        public ICollection<ProdutoPedidoCentralDTO>? ProdutosPedidoCentral { get; set; }
        
        public static implicit operator PedidoCentralDTO(PedidoCentral entity)
        {
            return new PedidoCentralDTO
            {
                Id = entity.Id,
                DataPedido = entity.DataPedido,
                NumeroPedido = entity.NumeroPedido,
                RevendaId = entity.RevendaId,
                Status = entity.Status,
                ProdutosPedidoCentral = entity.ProdutosPedidoCentral?.Select(p => (ProdutoPedidoCentralDTO)p).ToList() ?? new List<ProdutoPedidoCentralDTO>()
            };
        }
    }
}