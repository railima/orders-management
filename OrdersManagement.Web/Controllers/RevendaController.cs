
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Application.Interfaces.Services;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendaController : ControllerBase
    {
        private readonly IRevendaService _revendaService;

        public RevendaController(IRevendaService revendaService)
        {
            _revendaService = revendaService;
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
    }
}