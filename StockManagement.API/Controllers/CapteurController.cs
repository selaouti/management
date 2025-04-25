using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapteurController : ControllerBase
    {
        private readonly CapteurService _capteurService;

        public CapteurController(CapteurService capteurService)
        {
            _capteurService = capteurService;
        }

        // GET: api/Capteur
        [HttpGet]
        public async Task<IActionResult> GetAllCapteurs()
        {
            var capteurs = await _capteurService.GetAllCapteursAsync();
            return Ok(capteurs);
        }

        // GET: api/Capteur/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCapteurById(int id)
        {
            var capteur = await _capteurService.GetCapteurByIdAsync(id);
            if (capteur == null)
            {
                return NotFound();
            }
            return Ok(capteur);
        }

        // POST: api/Capteur
        [HttpPost]
        public async Task<IActionResult> AddCapteur([FromBody] Capteur capteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCapteur = await _capteurService.AddCapteurAsync(capteur);
            return CreatedAtAction(nameof(GetCapteurById), new { id = createdCapteur.IdCapteur }, createdCapteur);
        }

        // PUT: api/Capteur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCapteur(int id, [FromBody] Capteur capteur)
        {
            if (id != capteur.IdCapteur)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _capteurService.UpdateCapteurAsync(capteur);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Capteur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapteur(int id)
        {
            var result = await _capteurService.DeleteCapteurAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Capteur/ByEntrepotLocation
        [HttpGet("ByEntrepotLocation")]
        public async Task<IActionResult> GetCapteursByEntrepotLocation([FromQuery] string entrepotLocation)
        {
            var capteurs = await _capteurService.GetCapteursByEntrepotLocationAsync(entrepotLocation);
            return Ok(capteurs);
        }
    }
}