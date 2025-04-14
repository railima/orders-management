using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class ContatoDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool IsPrincipal { get; set; } = false;

        public static implicit operator ContatoDTO(Contato contato)
        {
            return new ContatoDTO
            {
                Id = contato.Id,
                Nome = contato.Nome,
            };
        }
    }
}