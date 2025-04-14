using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class ProdutoPedidoCentral
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