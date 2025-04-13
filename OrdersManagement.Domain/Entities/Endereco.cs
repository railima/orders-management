namespace OrdersManagement.Domain.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        public required string Logradouro { get; set; }
        public required string Numero { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public required string Cep { get; set; }
        public int RevendaId { get; set; }

        public required Revenda Revenda { get; set; }
    }
}