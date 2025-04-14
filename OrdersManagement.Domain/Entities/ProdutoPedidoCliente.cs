using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class ProdutoPedidoCliente
    {
        public int Id { get; set; }
        public required string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public int PedidoClienteId { get; set; }
        public PedidoCliente? PedidoCliente { get; set; }

        public static explicit operator ProdutoPedidoCliente(ProdutoPedidoClienteDTO dto)
        {
            return new ProdutoPedidoCliente
            {
                Id = dto.Id,
                NomeProduto = dto.NomeProduto,
                Quantidade = dto.Quantidade,
                
            };
        }
    }
}