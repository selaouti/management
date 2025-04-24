using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées au stock.
    /// </summary>
    public class StockService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public StockService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les stocks disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les stocks.</returns>
        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks
                .Include(s => s.Article) // Inclut les articles associés
                .Include(s => s.Entrepot) // Inclut les entrepôts associés
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un stock spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du stock.</param>
        /// <returns>Le stock correspondant ou null s'il n'existe pas.</returns>
        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks
                .Include(s => s.Article)
                .Include(s => s.Entrepot)
                .FirstOrDefaultAsync(s => s.IdStock == id);
        }

        /// <summary>
        /// Ajoute un nouveau stock dans la base de données.
        /// </summary>
        /// <param name="stock">Le stock à ajouter.</param>
        /// <returns>Le stock ajouté avec son ID généré.</returns>
        public async Task<Stock> AddStockAsync(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        /// <summary>
        /// Met à jour un stock existant dans la base de données.
        /// </summary>
        /// <param name="stock">Le stock avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            var existingStock = await _context.Stocks.FindAsync(stock.IdStock);
            if (existingStock == null) return false;

            _context.Entry(existingStock).CurrentValues.SetValues(stock);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un stock par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du stock à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return false;

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des stocks par article.
        /// </summary>
        /// <param name="articleId">L'identifiant de l'article.</param>
        /// <returns>Liste des stocks associés à cet article.</returns>
        public async Task<List<Stock>> GetStocksByArticleIdAsync(int articleId)
        {
            return await _context.Stocks
                .Include(s => s.Article)
                .Include(s => s.Entrepot)
                .Where(s => s.ArticleId == articleId)
                .ToListAsync();
        }

        /// <summary>
        /// Recherche des stocks par entrepôt.
        /// </summary>
        /// <param name="entrepotId">L'identifiant de l'entrepôt.</param>
        /// <returns>Liste des stocks dans cet entrepôt.</returns>
        public async Task<List<Stock>> GetStocksByEntrepotIdAsync(int entrepotId)
        {
            return await _context.Stocks
                .Include(s => s.Article)
                .Include(s => s.Entrepot)
                .Where(s => s.EntrepotId == entrepotId)
                .ToListAsync();
        }
    }
}