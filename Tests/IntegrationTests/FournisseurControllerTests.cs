using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using StockManagement.Domain.Entities;
using Xunit;

namespace IntegrationTests
{
    public class FournisseurControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FournisseurControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllFournisseurs_ReturnsOk_WithListOfFournisseurs()
        {
            // Act
            var response = await _client.GetAsync("/api/fournisseur");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<List<Fournisseur>>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFournisseurById_ReturnsNotFound_WhenFournisseurDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync("/api/fournisseur/999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddFournisseur_ReturnsCreated_WithFournisseur()
        {
            // Arrange
            var newFournisseur = new Fournisseur { Nom = "Nouveau Fournisseur" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/fournisseur", newFournisseur);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdFournisseur = await response.Content.ReadFromJsonAsync<Fournisseur>();
            Assert.NotNull(createdFournisseur);
            Assert.Equal("Nouveau Fournisseur", createdFournisseur!.Nom);
        }
    }
}