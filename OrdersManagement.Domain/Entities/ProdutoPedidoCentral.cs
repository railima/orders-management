using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class ProdutoPedidoCentral
    {
        public int Id { get; set; }
        public required string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public int PedidoCentralId { get; set; }
        public PedidoCentral? PedidoCentral { get; set; }

        public static explicit operator ProdutoPedidoCentral(ProdutoPedidoCentralDTO dto)
        {
            return new ProdutoPedidoCentral
            {
                Id = dto.Id,
                NomeProduto = dto.NomeProduto,
                Quantidade = dto.Quantidade,
                
            };
        }
    }
}