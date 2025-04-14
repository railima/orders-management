using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.Domain.Entities
{
    public class Revenda
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public required string NomeFantasia { get; set; }
        [Required]
        public required Cnpj Cnpj { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public required string RazaoSocial { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public ICollection<Telefone>? Telefones { get; set; }
        public required ICollection<Contato> Contatos { get; set; }
        public required ICollection<Endereco> Enderecos { get; set; }
        public ICollection<PedidoCliente>? PedidosCliente { get; set; }
        public ICollection<PedidoCentral>? PedidosCentral { get; set; }

        public static implicit operator Revenda(RevendaDTO revenda)
        {
            return new Revenda
            {
                Id = revenda.Id,
                NomeFantasia = revenda.NomeFantasia,
                Cnpj = revenda.Cnpj,
                RazaoSocial = revenda.RazaoSocial,
                Email = revenda.Email,
                Contatos = revenda.Contatos.Select(c => (Contato)c).ToList(),
                Enderecos = revenda.Enderecos.Select(e => (Endereco)e).ToList(),
                Telefones = revenda.Telefones?.Select(t => (Telefone)t).ToList()
            };
        }
    }
}