using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées aux articles.
    /// </summary>
    public class ArticleService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public ArticleService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les articles disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les articles.</returns>
        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.Include(a => a.Fournisseur).ToListAsync();
        }

        /// <summary>
        /// Récupère un article spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'article.</param>
        /// <returns>L'article correspondant ou null s'il n'existe pas.</returns>
        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.Include(a => a.Fournisseur).FirstOrDefaultAsync(a => a.IdArticle == id);
        }

        /// <summary>
        /// Ajoute un nouvel article dans la base de données.
        /// </summary>
        /// <param name="article">L'article à ajouter.</param>
        /// <returns>L'article ajouté avec son ID généré.</returns>
        public async Task<Article> AddArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        /// <summary>
        /// Met à jour un article existant dans la base de données.
        /// </summary>
        /// <param name="article">L'article avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateArticleAsync(Article article)
        {
            var existingArticle = await _context.Articles.FindAsync(article.IdArticle);
            if (existingArticle == null) return false;

            _context.Entry(existingArticle).CurrentValues.SetValues(article);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un article par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'article à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des articles par nom (recherche partielle).
        /// </summary>
        /// <param name="name">Le nom ou partie du nom à rechercher.</param>
        /// <returns>Liste des articles correspondants au critère de recherche.</returns>
        public async Task<List<Article>> SearchArticlesByNameAsync(string name)
        {
            return await _context.Articles
                .Include(a => a.Fournisseur)
                .Where(a => EF.Functions.Like(a.CodeArticle, $"%{name}%"))
                .ToListAsync();
        }
    }
}