namespace OrdersManagement.Domain.Enums
{
    public enum StatusPedido
    {
        Pendente = 1,
        EmEspera = 2, // Quando a API da central estiver fora do ar
        Enviado = 3
    }
}