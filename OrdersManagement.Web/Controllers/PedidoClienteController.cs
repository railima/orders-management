
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoClienteController : ControllerBase
    {
        private readonly IPedidoClienteService _revendaService;

        public PedidoClienteController(IPedidoClienteService revendaService)
        {
            _revendaService = revendaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidosCliente()
        {
            var revendas = await _revendaService.GetAllPedidosClienteAsync();
            return Ok(revendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoClienteById(int id)
        {
            var revenda = await _revendaService.GetPedidoClienteByIdAsync(id);
            if (revenda == null)
            {
                return NotFound();
            }
            return Ok(revenda);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedidoCliente([FromBody] PedidoClienteDTO revenda)
        {
            if (revenda == null)
            {
                return BadRequest();
            }
            var createdPedidoCliente = await _revendaService.CreatePedidoClienteAsync(revenda);
            return CreatedAtAction(nameof(GetPedidoClienteById), new { id = createdPedidoCliente.Id }, createdPedidoCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedidoCliente(int id, [FromBody] PedidoClienteDTO revenda)
        {
            if (id != revenda.Id)
            {
                return BadRequest();
            }
            var updatedPedidoCliente = await _revendaService.UpdatePedidoClienteAsync(revenda);
            return Ok(updatedPedidoCliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoCliente(int id)
        {
            var result = await _revendaService.DeletePedidoClienteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}