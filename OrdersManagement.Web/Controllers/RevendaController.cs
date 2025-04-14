
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Enums;

namespace OrdersManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendaController : ControllerBase
    {
        private readonly IRevendaService _revendaService;
        private readonly IPedidoClienteService _pedidoClienteService;
        private readonly ICentralService _centralService;
        private readonly IPedidoCentralService _pedidoCentralService;

        public RevendaController(
            IRevendaService revendaService,
            IPedidoClienteService pedidoClienteService,
            ICentralService centralService,
            IPedidoCentralService pedidoCentralService
            )
        {
            _revendaService = revendaService;
            _pedidoClienteService = pedidoClienteService;
            _centralService = centralService;
            _pedidoCentralService = pedidoCentralService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRevendas()
        {
            var revendas = await _revendaService.GetAllRevendasAsync();
            return Ok(revendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRevendaById(int id)
        {
            var revenda = await _revendaService.GetRevendaByIdAsync(id);
            if (revenda == null)
            {
                return NotFound();
            }
            return Ok(revenda);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRevenda([FromBody] RevendaDTO revenda)
        {
            if (revenda == null)
            {
                return BadRequest();
            }
            var createdRevenda = await _revendaService.CreateRevendaAsync(revenda);
            return CreatedAtAction(nameof(GetRevendaById), new { id = createdRevenda.Id }, createdRevenda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRevenda(int id, [FromBody] RevendaDTO revenda)
        {
            if (id != revenda.Id)
            {
                return BadRequest();
            }
            var updatedRevenda = await _revendaService.UpdateRevendaAsync(revenda);
            return Ok(updatedRevenda);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevenda(int id)
        {
            var result = await _revendaService.DeleteRevendaAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/pedido-cliente")]
        public async Task<IActionResult> CreatePedidoCliente(int id, [FromBody] PedidoClienteRequestDTO pedidoCliente)
        {
            if (pedidoCliente == null)
            {
                return BadRequest();
            }
            var createdPedido = await _revendaService.CreatePedidoClienteAsync(id, pedidoCliente);
            return CreatedAtAction(nameof(GetRevendaById), new { id = createdPedido.Id }, createdPedido);
        }

        [HttpPost("{id}/pedido-central")]
        public async Task<IActionResult> IssuePedidoCentral(int id)
        {
            var revenda = await _revendaService.GetRevendaByIdAsync(id);
            if (revenda == null) return NotFound("Revenda não encontrada.");

            var pedidosPendentes = await _pedidoClienteService.GetAllPendentesAsync(id);
            int total = pedidosPendentes?.Sum(p => p.ProdutosPedidoCliente?.Sum(pp => pp.Quantidade) ?? 0) ?? 0;
            if (total < 1000)
            {
                return BadRequest("O pedido mínimo é de 1000 itens.");
            }

            var itens = pedidosPendentes
                .SelectMany(p => p.ProdutosPedidoCliente)
                .Select(i => new ProdutoPedidoCentralDTO
                {
                    Id = i.Id,
                    NomeProduto = i.NomeProduto,
                    Quantidade = i.Quantidade
                })
                .ToList();

            var pedidoCentral = new PedidoCentralRequestDTO
            {
                RevendaId = id,
                Itens = itens
            };

            try
            {
                var response = await _centralService.CreatePedidoCentralAsync(pedidoCentral);

                if (response == null)
                {

                    return StatusCode(500, "Erro ao criar pedido central.");
                }

                foreach (var pedido in pedidosPendentes)
                {
                    await _pedidoClienteService.MarkAsEnviadoAsync(pedido.Id);
                }

                var status = StatusPedido.Enviado;
                await _pedidoCentralService.CreatePedidoAsync(id, status, itens);
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                var status = StatusPedido.EmEspera;
                await _pedidoCentralService.CreatePedidoAsync(id, status, itens);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "API da Central indisponível. Pedido salvo como EM ESPERA.");
            }
        }
    }
}