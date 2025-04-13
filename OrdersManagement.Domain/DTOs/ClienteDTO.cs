using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public static implicit operator Cliente(ClienteDTO clienteDTO)
        {
            return new Cliente
            {
                Id = clienteDTO.Id,
                Nome = clienteDTO.Nome
            };
        }
    }
}