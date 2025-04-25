using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : ControllerBase
    {
        private readonly LotService _lotService;

        public LotController(LotService lotService)
        {
            _lotService = lotService;
        }

        // GET: api/Lot
        [HttpGet]
        public async Task<IActionResult> GetAllLots()
        {
            var lots = await _lotService.GetAllLotsAsync();
            return Ok(lots);
        }

        // GET: api/Lot/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLotById(int id)
        {
            var lot = await _lotService.GetLotByIdAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            return Ok(lot);
        }

        // POST: api/Lot
        [HttpPost]
        public async Task<IActionResult> AddLot([FromBody] Lot lot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLot = await _lotService.AddLotAsync(lot);
            return CreatedAtAction(nameof(GetLotById), new { id = createdLot.IdLot }, createdLot);
        }

        // PUT: api/Lot/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLot(int id, [FromBody] Lot lot)
        {
            if (id != lot.IdLot)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _lotService.UpdateLotAsync(lot);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Lot/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLot(int id)
        {
            var result = await _lotService.DeleteLotAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Lot/ByExpiryDate
        [HttpGet("ByExpiryDate")]
        public async Task<IActionResult> GetLotsByExpiryDate([FromQuery] DateTime expiryDate)
        {
            var lots = await _lotService.GetLotsByExpiryDateAsync(expiryDate);
            return Ok(lots);
        }

        // GET: api/Lot/ByArticle/{articleId}
        [HttpGet("ByArticle/{articleId}")]
        public async Task<IActionResult> GetLotsByArticleId(int articleId)
        {
            var lots = await _lotService.GetLotsByArticleIdAsync(articleId);
            return Ok(lots);
        }
    }
}