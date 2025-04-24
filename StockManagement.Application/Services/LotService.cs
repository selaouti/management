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
    /// Service pour gérer les opérations métier liées aux lots.
    /// </summary>
    public class LotService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public LotService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les lots disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les lots.</returns>
        public async Task<List<Lot>> GetAllLotsAsync()
        {
            return await _context.Lots
                .Include(l => l.Article) // Inclut l'article associé à chaque lot
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un lot spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du lot.</param>
        /// <returns>Le lot correspondant ou null s'il n'existe pas.</returns>
        public async Task<Lot?> GetLotByIdAsync(int id)
        {
            return await _context.Lots
                .Include(l => l.Article)
                .FirstOrDefaultAsync(l => l.IdLot == id);
        }

        /// <summary>
        /// Ajoute un nouveau lot dans la base de données.
        /// </summary>
        /// <param name="lot">Le lot à ajouter.</param>
        /// <returns>Le lot ajouté avec son ID généré.</returns>
        public async Task<Lot> AddLotAsync(Lot lot)
        {
            _context.Lots.Add(lot);
            await _context.SaveChangesAsync();
            return lot;
        }

        /// <summary>
        /// Met à jour un lot existant dans la base de données.
        /// </summary>
        /// <param name="lot">Le lot avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateLotAsync(Lot lot)
        {
            var existingLot = await _context.Lots.FindAsync(lot.IdLot);
            if (existingLot == null) return false;

            _context.Entry(existingLot).CurrentValues.SetValues(lot);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un lot par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du lot à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteLotAsync(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            if (lot == null) return false;

            _context.Lots.Remove(lot);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des lots par date d'expiration.
        /// </summary>
        /// <param name="expiryDate">La date d'expiration à rechercher.</param>
        /// <returns>Liste des lots qui expirent à ou avant la date spécifiée.</returns>
        public async Task<List<Lot>> GetLotsByExpiryDateAsync(DateTime expiryDate)
        {
            return await _context.Lots
                .Include(l => l.Article)
                .Where(l => l.DateExpiration <= expiryDate)
                .ToListAsync();
        }

        /// <summary>
        /// Recherche des lots d'un article spécifique.
        /// </summary>
        /// <param name="articleId">L'identifiant de l'article.</param>
        /// <returns>Liste des lots associés à cet article.</returns>
        public async Task<List<Lot>> GetLotsByArticleIdAsync(int articleId)
        {
            return await _context.Lots
                .Include(l => l.Article)
                .Where(l => l.ArticleId == articleId)
                .ToListAsync();
        }
    }
}