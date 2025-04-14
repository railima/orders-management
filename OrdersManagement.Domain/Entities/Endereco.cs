using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;

namespace OrdersManagement.Domain.Entities
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public required string Logradouro { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public required string Numero { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public required string Bairro { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public required string Cidade { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public required string Estado { get; set; }
        [MinLength(8)]
        [MaxLength(8)]
        public required string Cep { get; set; }
        public int RevendaId { get; set; }

        public Revenda? Revenda { get; set; }

        public static implicit operator Endereco(EnderecoDTO v)
        {
            return new Endereco
            {
                Id = v.Id,
                Logradouro = v.Logradouro,
                Numero = v.Numero,
                Bairro = v.Bairro,
                Cidade = v.Cidade,
                Estado = v.Estado,
                Cep = v.Cep
            };
        }
    }
}