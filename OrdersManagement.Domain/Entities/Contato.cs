using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class Contato
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public required string Nome { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int RevendaId { get; set; }

        public Revenda? Revenda { get; set; }

        public static implicit operator Contato(ContatoDTO v)
        {
            return new Contato
            {
                Id = v.Id,
                Nome = v.Nome,
                IsPrincipal = v.IsPrincipal
            };
        }
    }
}