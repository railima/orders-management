using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class TelefoneDTO
    {
        public int Id { get; set; }
        public required string Ddd { get; set; }
        public required string Numero { get; set; }
        public bool IsPrincipal { get; set; } = false;

        public static implicit operator TelefoneDTO(Telefone telefone)
        {
            return new TelefoneDTO
            {
                Id = telefone.Id,
                Ddd = telefone.Ddd,
                Numero = telefone.Numero,
                IsPrincipal = telefone.IsPrincipal
            };
        }
    }
}