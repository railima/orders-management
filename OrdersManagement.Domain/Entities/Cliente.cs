using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class Cliente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public required string Nome { get; set; }

        public ICollection<PedidoCliente>? Pedidos { get; set; }

        public static implicit operator ClienteDTO(Cliente cliente)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome
            };
        }
    }
}