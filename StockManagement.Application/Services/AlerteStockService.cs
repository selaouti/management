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
    /// Service pour gérer les opérations métier liées aux alertes de stock.
    /// </summary>
    public class AlerteStockService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public AlerteStockService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère toutes les alertes de stock enregistrées.
        /// </summary>
        /// <returns>Liste de toutes les alertes de stock.</returns>
        public async Task<List<AlerteStock>> GetAllAlertesAsync()
        {
            return await _context.AlertesStock
                .Include(a => a.Article) // Inclut l'article concerné par l'alerte
                .Include(a => a.Entrepot) // Inclut l'entrepôt concerné par l'alerte
                .ToListAsync();
        }

        /// <summary>
        /// Récupère une alerte de stock spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'alerte.</param>
        /// <returns>L'alerte de stock correspondante ou null si elle n'existe pas.</returns>
        public async Task<AlerteStock?> GetAlerteByIdAsync(int id)
        {
            return await _context.AlertesStock
                .Include(a => a.Article)
                .Include(a => a.Entrepot)
                .FirstOrDefaultAsync(a => a.IdAlerte == id);
        }

        /// <summary>
        /// Ajoute une nouvelle alerte de stock dans la base de données.
        /// </summary>
        /// <param name="alerteStock">L'alerte de stock à ajouter.</param>
        /// <returns>L'alerte ajoutée avec son ID généré.</returns>
        public async Task<AlerteStock> AddAlerteAsync(AlerteStock alerteStock)
        {
            _context.AlertesStock.Add(alerteStock);
            await _context.SaveChangesAsync();
            return alerteStock;
        }

        /// <summary>
        /// Met à jour une alerte de stock existante dans la base de données.
        /// </summary>
        /// <param name="alerteStock">L'alerte de stock avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateAlerteAsync(AlerteStock alerteStock)
        {
            var existingAlerte = await _context.AlertesStock.FindAsync(alerteStock.IdAlerte);
            if (existingAlerte == null) return false;

            _context.Entry(existingAlerte).CurrentValues.SetValues(alerteStock);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime une alerte de stock par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'alerte à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteAlerteAsync(int id)
        {
            var alerte = await _context.AlertesStock.FindAsync(id);
            if (alerte == null) return false;

            _context.AlertesStock.Remove(alerte);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des alertes de stock pour un article spécifique.
        /// </summary>
        /// <param name="articleId">L'identifiant de l'article.</param>
        /// <returns>Liste des alertes de stock associées à cet article.</returns>
        public async Task<List<AlerteStock>> GetAlertesByArticleIdAsync(int articleId)
        {
            return await _context.AlertesStock
                .Include(a => a.Article)
                .Include(a => a.Entrepot)
                .Where(a => a.ArticleId == articleId)
                .ToListAsync();
        }

        /// <summary>
        /// Recherche des alertes de stock déclenchées avant une date donnée.
        /// </summary>
        /// <param name="date">La date limite pour rechercher les alertes.</param>
        /// <returns>Liste des alertes de stock déclenchées avant cette date.</returns>
        public async Task<List<AlerteStock>> GetAlertesTriggeredBeforeAsync(DateTime date)
        {
            return await _context.AlertesStock
                .Include(a => a.Article)
                .Include(a => a.Entrepot)
                .Where(a => a.DateDeclenchement <= date)
                .ToListAsync();
        }
    }
}