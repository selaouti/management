using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _service;

        public ArticlesController(ArticleService service)
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
        public IActionResult Create([FromBody] Article article)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdArticle = _service.Create(article);
            return CreatedAtAction(nameof(GetById), new { id = createdArticle.Id }, createdArticle);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Article article)
        {
            if (id != article.Id) return BadRequest();
            var updatedArticle = _service.Update(article);
            return Ok(updatedArticle);
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