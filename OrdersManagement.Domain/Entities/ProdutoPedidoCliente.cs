using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class ProdutoPedidoCliente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public required string NomeProduto { get; set; }
        [Required]
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