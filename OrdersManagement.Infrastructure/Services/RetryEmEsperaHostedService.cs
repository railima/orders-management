using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.Enums;
using OrdersManagement.Domain.DTOs;

public class RetryEmEsperaHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RetryEmEsperaHostedService> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    public RetryEmEsperaHostedService(IServiceProvider serviceProvider,
        ILogger<RetryEmEsperaHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: int.MaxValue,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(5),
                onRetry: (ex, ts, attempt, context) =>
                {
                    _logger.LogWarning(ex, $"Tentativa {attempt} falhou. Retentando...", attempt);
                }
            );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessarPedidosEmEspera(stoppingToken);
            
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task ProcessarPedidosEmEspera(CancellationToken stoppingToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            
            var pedidoCentralService = scope.ServiceProvider.GetRequiredService<IPedidoCentralService>();
            var centralService = scope.ServiceProvider.GetRequiredService<ICentralService>();

            var pendentes = await pedidoCentralService.GetAllByStatusAsync(StatusPedido.EmEspera);

            foreach (var pedido in pendentes)
            {
                if (stoppingToken.IsCancellationRequested) break;

                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var pedidoCentral = new PedidoCentralRequestDTO
                    {
                        RevendaId = pedido.RevendaId,
                        Itens = pedido.ProdutosPedidoCentral.Select(p => new ProdutoPedidoCentralDTO
                        {
                            Id = p.Id,
                            NomeProduto = p.NomeProduto,
                            Quantidade = p.Quantidade
                        }).ToList()
                    };
                    await centralService.CreatePedidoCentralAsync(pedidoCentral);
                    
                    await pedidoCentralService.MarkAsEnviadoAsync(pedido.Id);
                    _logger.LogInformation($"Pedido {pedido.Id} reenviado com sucesso");
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pedidos em EM_ESPERA");
        }
    }
}