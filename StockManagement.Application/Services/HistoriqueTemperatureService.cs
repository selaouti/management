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
    /// Service pour gérer les opérations métier liées à l'historique des températures.
    /// </summary>
    public class HistoriqueTemperatureService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public HistoriqueTemperatureService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les enregistrements d'historique de température.
        /// </summary>
        /// <returns>Liste de tous les enregistrements d'historique.</returns>
        public async Task<List<HistoriqueTemperature>> GetAllHistoriqueAsync()
        {
            return await _context.HistoriqueTemperatures
                .Include(ht => ht.Capteur) // Inclut le capteur associé
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un enregistrement d'historique de température spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'enregistrement.</param>
        /// <returns>L'enregistrement d'historique correspondant ou null s'il n'existe pas.</returns>
        public async Task<HistoriqueTemperature?> GetHistoriqueByIdAsync(int id)
        {
            return await _context.HistoriqueTemperatures
                .Include(ht => ht.Capteur)
                .FirstOrDefaultAsync(ht => ht.IdHistorique == id);
        }

        /// <summary>
        /// Ajoute un nouvel enregistrement d'historique de température dans la base de données.
        /// </summary>
        /// <param name="historiqueTemperature">L'enregistrement à ajouter.</param>
        /// <returns>L'enregistrement ajouté avec son ID généré.</returns>
        public async Task<HistoriqueTemperature> AddHistoriqueAsync(HistoriqueTemperature historiqueTemperature)
        {
            _context.HistoriqueTemperatures.Add(historiqueTemperature);
            await _context.SaveChangesAsync();
            return historiqueTemperature;
        }

        /// <summary>
        /// Met à jour un enregistrement d'historique de température existant dans la base de données.
        /// </summary>
        /// <param name="historiqueTemperature">L'enregistrement avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateHistoriqueAsync(HistoriqueTemperature historiqueTemperature)
        {
            var existingHistorique = await _context.HistoriqueTemperatures.FindAsync(historiqueTemperature.IdHistorique);
            if (existingHistorique == null) return false;

            _context.Entry(existingHistorique).CurrentValues.SetValues(historiqueTemperature);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un enregistrement d'historique de température par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'enregistrement à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteHistoriqueAsync(int id)
        {
            var historique = await _context.HistoriqueTemperatures.FindAsync(id);
            if (historique == null) return false;

            _context.HistoriqueTemperatures.Remove(historique);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des enregistrements d'historique par capteur.
        /// </summary>
        /// <param name="capteurId">L'identifiant du capteur.</param>
        /// <returns>Liste des enregistrements associés à ce capteur.</returns>
        public async Task<List<HistoriqueTemperature>> GetHistoriqueByCapteurIdAsync(int capteurId)
        {
            return await _context.HistoriqueTemperatures
                .Include(ht => ht.Capteur)
                .Where(ht => ht.CapteurId == capteurId)
                .ToListAsync();
        }

        /// <summary>
        /// Recherche des enregistrements d'historique pour une plage de dates.
        /// </summary>
        /// <param name="startDate">Date de début de la plage.</param>
        /// <param name="endDate">Date de fin de la plage.</param>
        /// <returns>Liste des enregistrements dans cette plage de dates.</returns>
        public async Task<List<HistoriqueTemperature>> GetHistoriqueByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.HistoriqueTemperatures
                .Include(ht => ht.Capteur)
                .Where(ht => ht.DateMesure >= startDate && ht.DateMesure <= endDate) // Utilise DateMesure
                .ToListAsync();
        }
    }
}