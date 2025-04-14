
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
        private readonly IPedidoClienteService _pedidoClienteService;

        public PedidoClienteController(IPedidoClienteService pedidoClienteService)
        {
            _pedidoClienteService = pedidoClienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidosCliente()
        {
            var clientes = await _pedidoClienteService.GetAllPedidosClienteAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoClienteById(int id)
        {
            var cliente = await _pedidoClienteService.GetPedidoClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedidoCliente([FromBody] PedidoClienteDTO cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            var createdPedidoCliente = await _pedidoClienteService.CreatePedidoClienteAsync(cliente);
            return CreatedAtAction(nameof(GetPedidoClienteById), new { id = createdPedidoCliente.Id }, createdPedidoCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedidoCliente(int id, [FromBody] PedidoClienteDTO cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }
            var updatedPedidoCliente = await _pedidoClienteService.UpdatePedidoClienteAsync(cliente);
            return Ok(updatedPedidoCliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoCliente(int id)
        {
            var result = await _pedidoClienteService.DeletePedidoClienteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}