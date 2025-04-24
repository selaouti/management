using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées aux entrepôts.
    /// </summary>
    public class EntrepotService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public EntrepotService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les entrepôts disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les entrepôts.</returns>
        public async Task<List<Entrepot>> GetAllEntrepotsAsync()
        {
            return await _context.Entrepots
                .Include(e => e.Stocks) // Inclut les stocks associés à chaque entrepôt
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un entrepôt spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'entrepôt.</param>
        /// <returns>L'entrepôt correspondant ou null s'il n'existe pas.</returns>
        public async Task<Entrepot?> GetEntrepotByIdAsync(int id)
        {
            return await _context.Entrepots
                .Include(e => e.Stocks)
                .FirstOrDefaultAsync(e => e.IdEntrepot == id);
        }

        /// <summary>
        /// Ajoute un nouvel entrepôt dans la base de données.
        /// </summary>
        /// <param name="entrepot">L'entrepôt à ajouter.</param>
        /// <returns>L'entrepôt ajouté avec son ID généré.</returns>
        public async Task<Entrepot> AddEntrepotAsync(Entrepot entrepot)
        {
            _context.Entrepots.Add(entrepot);
            await _context.SaveChangesAsync();
            return entrepot;
        }

        /// <summary>
        /// Met à jour un entrepôt existant dans la base de données.
        /// </summary>
        /// <param name="entrepot">L'entrepôt avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateEntrepotAsync(Entrepot entrepot)
        {
            var existingEntrepot = await _context.Entrepots.FindAsync(entrepot.IdEntrepot);
            if (existingEntrepot == null) return false;

            _context.Entry(existingEntrepot).CurrentValues.SetValues(entrepot);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un entrepôt par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'entrepôt à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteEntrepotAsync(int id)
        {
            var entrepot = await _context.Entrepots.FindAsync(id);
            if (entrepot == null) return false;

            _context.Entrepots.Remove(entrepot);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des entrepôts par localisation.
        /// </summary>
        /// <param name="location">La localisation à rechercher.</param>
        /// <returns>Liste des entrepôts correspondant à la localisation.</returns>
        public async Task<List<Entrepot>> SearchEntrepotsByLocationAsync(string location)
        {
            return await _context.Entrepots
                .Where(e => EF.Functions.Like(e.Localisation, $"%{location}%"))
                .ToListAsync();
        }
    }
}