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
    public class FournisseurServiceTests
    {
        private readonly Mock<AppDbContext> _mockDbContext;
        private readonly FournisseurService _fournisseurService;

        public FournisseurServiceTests()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _fournisseurService = new FournisseurService(_mockDbContext.Object);
        }

        [Fact]
        public async Task GetAllFournisseursAsync_ReturnsListOfFournisseurs()
        {
            // Arrange
            var fournisseurs = new List<Fournisseur>
            {
                new Fournisseur { IdFournisseur = 1, Nom = "Fournisseur 1" },
                new Fournisseur { IdFournisseur = 2, Nom = "Fournisseur 2" }
            };
            var dbSetMock = new Mock<DbSet<Fournisseur>>();
            _mockDbContext.Setup(m => m.Fournisseurs).Returns(dbSetMock.Object);

            // Act
            var result = await _fournisseurService.GetAllFournisseursAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Fournisseur>>(result);
        }

        [Fact]
        public async Task GetFournisseurByIdAsync_ReturnsFournisseur_WhenExists()
        {
            // Arrange
            var fournisseur = new Fournisseur { IdFournisseur = 1, Nom = "Fournisseur 1" };
            _mockDbContext.Setup(m => m.Fournisseurs.FindAsync(1)).ReturnsAsync(fournisseur);

            // Act
            var result = await _fournisseurService.GetFournisseurByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdFournisseur);
        }
    }
}