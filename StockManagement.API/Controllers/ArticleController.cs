using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles);
        }

        // GET: api/Article/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // POST: api/Article
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdArticle = await _articleService.AddArticleAsync(article);
            return CreatedAtAction(nameof(GetArticleById), new { id = createdArticle.IdArticle }, createdArticle);
        }

        // PUT: api/Article/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Article article)
        {
            if (id != article.IdArticle)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _articleService.UpdateArticleAsync(article);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Article/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Article/Search
        [HttpGet("Search")]
        public async Task<IActionResult> SearchArticlesByName([FromQuery] string name)
        {
            var articles = await _articleService.SearchArticlesByNameAsync(name);
            return Ok(articles);
        }
    }
}