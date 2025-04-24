using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;
using Xunit;

namespace UnitTests
{
    public class ArticleServiceTests
    {
        private readonly Mock<AppDbContext> _mockDbContext;
        private readonly ArticleService _articleService;

        public ArticleServiceTests()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _articleService = new ArticleService(_mockDbContext.Object);
        }

        [Fact]
        public async Task GetAllArticlesAsync_ReturnsListOfArticles()
        {
            // Arrange
            var articles = new List<Article>
            {
                new Article { IdArticle = 1, Nom = "Article 1" },
                new Article { IdArticle = 2, Nom = "Article 2" }
            };
            var dbSetMock = new Mock<DbSet<Article>>();
            _mockDbContext.Setup(m => m.Articles).Returns(dbSetMock.Object);

            // Act
            var result = await _articleService.GetAllArticlesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Article>>(result);
        }

        [Fact]
        public async Task GetArticleByIdAsync_ReturnsArticle_WhenExists()
        {
            // Arrange
            var article = new Article { IdArticle = 1, Nom = "Article 1" };
            _mockDbContext.Setup(m => m.Articles.FindAsync(1)).ReturnsAsync(article);

            // Act
            var result = await _articleService.GetArticleByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdArticle);
        }
    }
}