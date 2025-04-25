using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly UtilisateurService _utilisateurService;

        public UtilisateurController(UtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        // GET: api/Utilisateur
        [HttpGet]
        public async Task<IActionResult> GetAllUtilisateurs()
        {
            var utilisateurs = await _utilisateurService.GetAllUtilisateursAsync();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateur/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilisateurById(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return Ok(utilisateur);
        }

        // POST: api/Utilisateur
        [HttpPost]
        public async Task<IActionResult> AddUtilisateur([FromBody] Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUtilisateur = await _utilisateurService.AddUtilisateurAsync(utilisateur);
            return CreatedAtAction(nameof(GetUtilisateurById), new { id = createdUtilisateur.IdUtilisateur }, createdUtilisateur);
        }

        // PUT: api/Utilisateur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(int id, [FromBody] Utilisateur utilisateur)
        {
            if (id != utilisateur.IdUtilisateur)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _utilisateurService.UpdateUtilisateurAsync(utilisateur);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Utilisateur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var result = await _utilisateurService.DeleteUtilisateurAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Utilisateur/Search
        [HttpGet("Search")]
        public async Task<IActionResult> SearchUtilisateursByName([FromQuery] string name)
        {
            var utilisateurs = await _utilisateurService.SearchUtilisateursByNameAsync(name);
            return Ok(utilisateurs);
        }

        // POST: api/Utilisateur/Authenticate
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateUtilisateur([FromBody] AuthenticateRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var utilisateur = await _utilisateurService.AuthenticateUtilisateurAsync(request.Email, request.Password);
            if (utilisateur == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(utilisateur);
        }
    }

    /// <summary>
    /// Requête pour l'authentification.
    /// </summary>
    public class AuthenticateRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}