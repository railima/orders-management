
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCentralController : ControllerBase
    {
        private readonly IPedidoCentralService _pedidoCentralService;

        public PedidoCentralController(IPedidoCentralService pedidoCentralService)
        {
            _pedidoCentralService = pedidoCentralService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidosCentral()
        {
            var centrals = await _pedidoCentralService.GetAllPedidosCentralAsync();
            return Ok(centrals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoCentralById(int id)
        {
            var central = await _pedidoCentralService.GetPedidoCentralByIdAsync(id);
            if (central == null)
            {
                return NotFound();
            }
            return Ok(central);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedidoCentral([FromBody] PedidoCentralDTO central)
        {
            if (central == null)
            {
                return BadRequest();
            }
            var createdPedidoCentral = await _pedidoCentralService.CreatePedidoCentralAsync(central);
            return CreatedAtAction(nameof(GetPedidoCentralById), new { id = createdPedidoCentral.Id }, createdPedidoCentral);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedidoCentral(int id, [FromBody] PedidoCentralDTO central)
        {
            if (id != central.Id)
            {
                return BadRequest();
            }
            var updatedPedidoCentral = await _pedidoCentralService.UpdatePedidoCentralAsync(central);
            return Ok(updatedPedidoCentral);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoCentral(int id)
        {
            var result = await _pedidoCentralService.DeletePedidoCentralAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}