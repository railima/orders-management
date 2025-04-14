using System.ComponentModel.DataAnnotations;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.Domain.DTOs
{

    public class RevendaDTO
    {
        public int Id { get; set; }
        public required string NomeFantasia { get; set; }
        public required Cnpj Cnpj { get; set; }
        public required string RazaoSocial { get; set; }
        public required string Email { get; set; }

        public ICollection<TelefoneDTO>? Telefones { get; set; }
        public required ICollection<ContatoDTO> Contatos { get; set; }
        public required ICollection<EnderecoDTO> Enderecos { get; set; }

        public static implicit operator RevendaDTO(Revenda revenda)
        {
            return new RevendaDTO
            {
                Id = revenda.Id,
                NomeFantasia = revenda.NomeFantasia,
                Cnpj = revenda.Cnpj,
                RazaoSocial = revenda.RazaoSocial,
                Email = revenda.Email,
                Contatos = revenda.Contatos?.Select(c => (ContatoDTO)c).ToList() ?? new List<ContatoDTO>(),
                Enderecos = revenda.Enderecos?.Select(e => (EnderecoDTO)e).ToList() ?? new List<EnderecoDTO>(),
                Telefones = revenda.Telefones?.Select(t => (TelefoneDTO)t).ToList() ?? new List<TelefoneDTO>()
            };
        }
    }
}