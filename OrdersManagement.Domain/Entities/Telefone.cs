namespace OrdersManagement.Domain.Entities
{
    public class Telefone
    {
        public int Id { get; set; }
        public required string Ddd { get; set; }
        public required string Numero { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int RevendaId { get; set; }

        public required Revenda Revenda { get; set; }
    }
}