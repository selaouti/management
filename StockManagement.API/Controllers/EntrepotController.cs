using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrepotController : ControllerBase
    {
        private readonly EntrepotService _entrepotService;

        public EntrepotController(EntrepotService entrepotService)
        {
            _entrepotService = entrepotService;
        }

        // GET: api/Entrepot
        [HttpGet]
        public async Task<IActionResult> GetAllEntrepots()
        {
            var entrepots = await _entrepotService.GetAllEntrepotsAsync();
            return Ok(entrepots);
        }

        // GET: api/Entrepot/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntrepotById(int id)
        {
            var entrepot = await _entrepotService.GetEntrepotByIdAsync(id);
            if (entrepot == null)
            {
                return NotFound();
            }
            return Ok(entrepot);
        }

        // POST: api/Entrepot
        [HttpPost]
        public async Task<IActionResult> AddEntrepot([FromBody] Entrepot entrepot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntrepot = await _entrepotService.AddEntrepotAsync(entrepot);
            return CreatedAtAction(nameof(GetEntrepotById), new { id = createdEntrepot.IdEntrepot }, createdEntrepot);
        }

        // PUT: api/Entrepot/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntrepot(int id, [FromBody] Entrepot entrepot)
        {
            if (id != entrepot.IdEntrepot)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _entrepotService.UpdateEntrepotAsync(entrepot);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Entrepot/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrepot(int id)
        {
            var result = await _entrepotService.DeleteEntrepotAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Entrepot/Search
        [HttpGet("Search")]
        public async Task<IActionResult> SearchEntrepotsByLocation([FromQuery] string location)
        {
            var entrepots = await _entrepotService.SearchEntrepotsByLocationAsync(location);
            return Ok(entrepots);
        }
    }
}