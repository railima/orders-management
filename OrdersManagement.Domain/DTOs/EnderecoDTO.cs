using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class EnderecoDTO
    {
        public int Id { get; set; }
        public required string Logradouro { get; set; }
        public required string Numero { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public required string Cep { get; set; }

        public static implicit operator EnderecoDTO(Endereco endereco)
        {
            return new EnderecoDTO
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep
            };
        }
    }
}