using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleFournisseurController : ControllerBase
    {
        private readonly ArticleFournisseurService _articleFournisseurService;

        public ArticleFournisseurController(ArticleFournisseurService articleFournisseurService)
        {
            _articleFournisseurService = articleFournisseurService;
        }

        // GET: api/ArticleFournisseur
        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            var links = await _articleFournisseurService.GetAllLinksAsync();
            return Ok(links);
        }

        // GET: api/ArticleFournisseur/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLinkById(int id)
        {
            var link = await _articleFournisseurService.GetLinkByIdAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            return Ok(link);
        }

        // POST: api/ArticleFournisseur
        [HttpPost]
        public async Task<IActionResult> AddLink([FromBody] ArticleFournisseur articleFournisseur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLink = await _articleFournisseurService.AddLinkAsync(articleFournisseur);
            return CreatedAtAction(nameof(GetLinkById), new { id = createdLink.Id }, createdLink);
        }

        // PUT: api/ArticleFournisseur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLink(int id, [FromBody] ArticleFournisseur articleFournisseur)
        {
            if (id != articleFournisseur.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _articleFournisseurService.UpdateLinkAsync(articleFournisseur);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/ArticleFournisseur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var result = await _articleFournisseurService.DeleteLinkAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/ArticleFournisseur/Search
        [HttpGet("Search")]
        public async Task<IActionResult> SearchLinks([FromQuery] string? articleName, [FromQuery] string? fournisseurName)
        {
            var links = await _articleFournisseurService.SearchLinksAsync(articleName, fournisseurName);
            return Ok(links);
        }
    }
}