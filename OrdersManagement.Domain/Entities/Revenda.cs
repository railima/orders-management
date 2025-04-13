namespace OrdersManagement.Domain.Entities
{
    public class Revenda
    {
        public int Id { get; set; }
        public required string NomeFantasia { get; set; }
        public required string Cnpj { get; set; }
        public required string RazaoSocial { get; set; }
        public required string Email { get; set; }

        public ICollection<Telefone>? Telefones { get; set; }
        public required ICollection<Contato> Contatos { get; set; }
        public required ICollection<Endereco> Enderecos { get; set; }
        public ICollection<PedidoCliente>? PedidosCliente { get; set; }
        public ICollection<PedidoCentral>? PedidosCentral { get; set; }
    }
}