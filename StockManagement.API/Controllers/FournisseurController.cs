using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly FournisseurService _fournisseurService;

        public FournisseurController(FournisseurService fournisseurService)
        {
            _fournisseurService = fournisseurService;
        }

        // GET: api/Fournisseur
        [HttpGet]
        public async Task<IActionResult> GetAllFournisseurs()
        {
            var fournisseurs = await _fournisseurService.GetAllFournisseursAsync();
            return Ok(fournisseurs);
        }

        // GET: api/Fournisseur/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFournisseurById(int id)
        {
            var fournisseur = await _fournisseurService.GetFournisseurByIdAsync(id);
            if (fournisseur == null)
            {
                return NotFound();
            }
            return Ok(fournisseur);
        }

        // POST: api/Fournisseur
        [HttpPost]
        public async Task<IActionResult> AddFournisseur([FromBody] Fournisseur fournisseur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFournisseur = await _fournisseurService.AddFournisseurAsync(fournisseur);
            return CreatedAtAction(nameof(GetFournisseurById), new { id = createdFournisseur.IdFournisseur }, createdFournisseur);
        }

        // PUT: api/Fournisseur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFournisseur(int id, [FromBody] Fournisseur fournisseur)
        {
            if (id != fournisseur.IdFournisseur)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _fournisseurService.UpdateFournisseurAsync(fournisseur);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Fournisseur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFournisseur(int id)
        {
            var result = await _fournisseurService.DeleteFournisseurAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Fournisseur/Search
        [HttpGet("Search")]
        public async Task<IActionResult> SearchFournisseursByName([FromQuery] string name)
        {
            var fournisseurs = await _fournisseurService.SearchFournisseursByNameAsync(name);
            return Ok(fournisseurs);
        }
    }
}