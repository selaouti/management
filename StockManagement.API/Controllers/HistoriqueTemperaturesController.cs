using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriqueTemperaturesController : ControllerBase
    {
        private readonly HistoriqueTemperatureService _service;

        public HistoriqueTemperaturesController(HistoriqueTemperatureService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] HistoriqueTemperature entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdEntity = _service.Create(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] HistoriqueTemperature entity)
        {
            if (id != entity.Id) return BadRequest();
            var updatedEntity = _service.Update(entity);
            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}