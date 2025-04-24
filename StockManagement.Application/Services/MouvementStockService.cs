using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées aux mouvements de stock.
    /// </summary>
    public class MouvementStockService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public MouvementStockService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les mouvements de stock enregistrés.
        /// </summary>
        /// <returns>Liste de tous les mouvements de stock.</returns>
        public async Task<List<MouvementStock>> GetAllMouvementsAsync()
        {
            return await _context.MouvementsStock
                .Include(ms => ms.Article) // Inclut l'article associé au mouvement
                .Include(ms => ms.Entrepot) // Inclut l'entrepôt associé au mouvement
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un mouvement de stock spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du mouvement.</param>
        /// <returns>Le mouvement de stock correspondant ou null s'il n'existe pas.</returns>
        public async Task<MouvementStock?> GetMouvementByIdAsync(int id)
        {
            return await _context.MouvementsStock
                .Include(ms => ms.Article)
                .Include(ms => ms.Entrepot)
                .FirstOrDefaultAsync(ms => ms.IdMouvement == id);
        }

        /// <summary>
        /// Ajoute un nouveau mouvement de stock dans la base de données.
        /// </summary>
        /// <param name="mouvementStock">Le mouvement de stock à ajouter.</param>
        /// <returns>Le mouvement ajouté avec son ID généré.</returns>
        public async Task<MouvementStock> AddMouvementAsync(MouvementStock mouvementStock)
        {
            _context.MouvementsStock.Add(mouvementStock);

            // Mise à jour du stock en conséquence
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.ArticleId == mouvementStock.ArticleId && s.EntrepotId == mouvementStock.EntrepotId);

            if (stock != null)
            {
                stock.Quantite += mouvementStock.TypeMouvement == "Entrée" 
                    ? mouvementStock.Quantite 
                    : -mouvementStock.Quantite;

                _context.Stocks.Update(stock);
            }

            await _context.SaveChangesAsync();
            return mouvementStock;
        }

        /// <summary>
        /// Met à jour un mouvement de stock existant dans la base de données.
        /// </summary>
        /// <param name="mouvementStock">Le mouvement de stock avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateMouvementAsync(MouvementStock mouvementStock)
        {
            var existingMouvement = await _context.MouvementsStock.FindAsync(mouvementStock.IdMouvement);
            if (existingMouvement == null) return false;

            _context.Entry(existingMouvement).CurrentValues.SetValues(mouvementStock);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un mouvement de stock par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du mouvement à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteMouvementAsync(int id)
        {
            var mouvement = await _context.MouvementsStock.FindAsync(id);
            if (mouvement == null) return false;

            _context.MouvementsStock.Remove(mouvement);

            // Optionnel : ajuster le stock lors de la suppression d'un mouvement
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.ArticleId == mouvement.ArticleId && s.EntrepotId == mouvement.EntrepotId);

            if (stock != null)
            {
                stock.Quantite -= mouvement.TypeMouvement == "Entrée" 
                    ? mouvement.Quantite 
                    : -mouvement.Quantite;

                _context.Stocks.Update(stock);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des mouvements de stock pour un article spécifique.
        /// </summary>
        /// <param name="articleId">L'identifiant de l'article.</param>
        /// <returns>Liste des mouvements de stock associés à cet article.</returns>
        public async Task<List<MouvementStock>> GetMouvementsByArticleIdAsync(int articleId)
        {
            return await _context.MouvementsStock
                .Include(ms => ms.Article)
                .Include(ms => ms.Entrepot)
                .Where(ms => ms.ArticleId == articleId)
                .ToListAsync();
        }

        /// <summary>
        /// Recherche des mouvements de stock par type (Entrée ou Sortie).
        /// </summary>
        /// <param name="type">Le type de mouvement ("Entrée" ou "Sortie").</param>
        /// <returns>Liste des mouvements correspondant au type spécifié.</returns>
        public async Task<List<MouvementStock>> GetMouvementsByTypeAsync(string type)
        {
            return await _context.MouvementsStock
                .Include(ms => ms.Article)
                .Include(ms => ms.Entrepot)
                .Where(ms => ms.TypeMouvement == type)
                .ToListAsync();
        }
    }
}