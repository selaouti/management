using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriqueTemperatureController : ControllerBase
    {
        private readonly HistoriqueTemperatureService _historiqueService;

        public HistoriqueTemperatureController(HistoriqueTemperatureService historiqueService)
        {
            _historiqueService = historiqueService;
        }

        // GET: api/HistoriqueTemperature
        [HttpGet]
        public async Task<IActionResult> GetAllHistorique()
        {
            var historiqueList = await _historiqueService.GetAllHistoriqueAsync();
            return Ok(historiqueList);
        }

        // GET: api/HistoriqueTemperature/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoriqueById(int id)
        {
            var historique = await _historiqueService.GetHistoriqueByIdAsync(id);
            if (historique == null)
            {
                return NotFound();
            }
            return Ok(historique);
        }

        // POST: api/HistoriqueTemperature
        [HttpPost]
        public async Task<IActionResult> AddHistorique([FromBody] HistoriqueTemperature historiqueTemperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdHistorique = await _historiqueService.AddHistoriqueAsync(historiqueTemperature);
            return CreatedAtAction(nameof(GetHistoriqueById), new { id = createdHistorique.IdHistorique }, createdHistorique);
        }

        // PUT: api/HistoriqueTemperature/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistorique(int id, [FromBody] HistoriqueTemperature historiqueTemperature)
        {
            if (id != historiqueTemperature.IdHistorique)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _historiqueService.UpdateHistoriqueAsync(historiqueTemperature);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/HistoriqueTemperature/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorique(int id)
        {
            var result = await _historiqueService.DeleteHistoriqueAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/HistoriqueTemperature/ByCapteur/{capteurId}
        [HttpGet("ByCapteur/{capteurId}")]
        public async Task<IActionResult> GetHistoriqueByCapteurId(int capteurId)
        {
            var historiqueList = await _historiqueService.GetHistoriqueByCapteurIdAsync(capteurId);
            return Ok(historiqueList);
        }

        // GET: api/HistoriqueTemperature/ByDateRange
        [HttpGet("ByDateRange")]
        public async Task<IActionResult> GetHistoriqueByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            var historiqueList = await _historiqueService.GetHistoriqueByDateRangeAsync(startDate, endDate);
            return Ok(historiqueList);
        }
    }
}