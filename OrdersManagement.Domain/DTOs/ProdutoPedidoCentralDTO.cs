using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class ProdutoPedidoCentralDTO
    {
        public int Id { get; set; }
        public required string NomeProduto { get; set; }
        public int Quantidade { get; set; }

        public static explicit operator ProdutoPedidoCentralDTO(ProdutoPedidoCentral entity)
        {
            return new ProdutoPedidoCentralDTO
            {
                Id = entity.Id,
                NomeProduto = entity.NomeProduto,
                Quantidade = entity.Quantidade
            };
        }
    }
}