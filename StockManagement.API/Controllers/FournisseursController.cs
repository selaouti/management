using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseursController : ControllerBase
    {
        private readonly FournisseurService _service;

        public FournisseursController(FournisseurService service)
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
        public IActionResult Create([FromBody] Fournisseur fournisseur)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdFournisseur = _service.Create(fournisseur);
            return CreatedAtAction(nameof(GetById), new { id = createdFournisseur.Id }, createdFournisseur);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Fournisseur fournisseur)
        {
            if (id != fournisseur.Id) return BadRequest();
            var updatedFournisseur = _service.Update(fournisseur);
            return Ok(updatedFournisseur);
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