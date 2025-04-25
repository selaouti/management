using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlerteStockController : ControllerBase
    {
        private readonly AlerteStockService _alerteStockService;

        public AlerteStockController(AlerteStockService alerteStockService)
        {
            _alerteStockService = alerteStockService;
        }

        // GET: api/AlerteStock
        [HttpGet]
        public IActionResult GetAll()
        {
            var alertes = _alerteStockService.GetAll();
            return Ok(alertes);
        }

        // GET: api/AlerteStock/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var alerte = _alerteStockService.GetById(id);
            if (alerte == null)
            {
                return NotFound();
            }
            return Ok(alerte);
        }

        // POST: api/AlerteStock
        [HttpPost]
        public IActionResult Create([FromBody] AlerteStock alerteStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAlerte = _alerteStockService.Create(alerteStock);
            return CreatedAtAction(nameof(GetById), new { id = createdAlerte.IdAlerteStock }, createdAlerte);
        }

        // PUT: api/AlerteStock/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AlerteStock alerteStock)
        {
            if (id != alerteStock.IdAlerteStock)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedAlerte = _alerteStockService.Update(alerteStock);
            return Ok(updatedAlerte);
        }

        // DELETE: api/AlerteStock/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _alerteStockService.Delete(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}