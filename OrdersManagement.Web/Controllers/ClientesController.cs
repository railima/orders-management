
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteDTO cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            var createdCliente = await _clienteService.CreateClienteAsync(cliente);
            return CreatedAtAction(nameof(GetClienteById), new { id = createdCliente.Id }, createdCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDTO cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }
            var updatedCliente = await _clienteService.UpdateClienteAsync(cliente);
            return Ok(updatedCliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clienteService.DeleteClienteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}