namespace OrdersManagement.Domain.Entities
{
    public class Contato
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int RevendaId { get; set; }

        public required Revenda Revenda { get; set; }
    }
}