using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class ProdutoPedidoClienteDTO
    {
        public int Id { get; set; }
        public required string NomeProduto { get; set; }
        public int Quantidade { get; set; }

        public static explicit operator ProdutoPedidoClienteDTO(ProdutoPedidoCliente entity)
        {
            return new ProdutoPedidoClienteDTO
            {
                Id = entity.Id,
                NomeProduto = entity.NomeProduto,
                Quantidade = entity.Quantidade
            };
        }
    }
}