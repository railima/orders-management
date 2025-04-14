using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class Telefone
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public required string Ddd { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(9)]
        public required string Numero { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int RevendaId { get; set; }

        public Revenda? Revenda { get; set; }

        public static explicit operator Telefone(TelefoneDTO v)
        {
            return new Telefone
            {
                Id = v.Id,
                Ddd = v.Ddd,
                Numero = v.Numero,
                IsPrincipal = v.IsPrincipal
            };
        }
    }
}