using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées aux capteurs.
    /// </summary>
    public class CapteurService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public CapteurService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les capteurs disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les capteurs.</returns>
        public async Task<List<Capteur>> GetAllCapteursAsync()
        {
            return await _context.Capteurs
                .Include(c => c.Entrepot) // Inclut l'entrepôt associé à chaque capteur
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un capteur spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du capteur.</param>
        /// <returns>Le capteur correspondant ou null s'il n'existe pas.</returns>
        public async Task<Capteur?> GetCapteurByIdAsync(int id)
        {
            return await _context.Capteurs
                .Include(c => c.Entrepot)
                .FirstOrDefaultAsync(c => c.IdCapteur == id);
        }

        /// <summary>
        /// Ajoute un nouveau capteur dans la base de données.
        /// </summary>
        /// <param name="capteur">Le capteur à ajouter.</param>
        /// <returns>Le capteur ajouté avec son ID généré.</returns>
        public async Task<Capteur> AddCapteurAsync(Capteur capteur)
        {
            _context.Capteurs.Add(capteur);
            await _context.SaveChangesAsync();
            return capteur;
        }

        /// <summary>
        /// Met à jour un capteur existant dans la base de données.
        /// </summary>
        /// <param name="capteur">Le capteur avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateCapteurAsync(Capteur capteur)
        {
            var existingCapteur = await _context.Capteurs.FindAsync(capteur.IdCapteur);
            if (existingCapteur == null) return false;

            _context.Entry(existingCapteur).CurrentValues.SetValues(capteur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un capteur par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du capteur à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteCapteurAsync(int id)
        {
            var capteur = await _context.Capteurs.FindAsync(id);
            if (capteur == null) return false;

            _context.Capteurs.Remove(capteur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des capteurs par localisation de l'entrepôt.
        /// </summary>
        /// <param name="entrepotLocation">La localisation de l'entrepôt à rechercher.</param>
        /// <returns>Liste des capteurs associés aux entrepôts correspondant à la localisation.</returns>
        public async Task<List<Capteur>> GetCapteursByEntrepotLocationAsync(string entrepotLocation)
        {
            return await _context.Capteurs
                .Include(c => c.Entrepot)
                .Where(c => EF.Functions.Like(c.Entrepot.Localisation, $"%{entrepotLocation}%"))
                .ToListAsync();
        }
    }
}