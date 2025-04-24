using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées aux fournisseurs.
    /// </summary>
    public class FournisseurService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public FournisseurService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les fournisseurs disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les fournisseurs.</returns>
        public async Task<List<Fournisseur>> GetAllFournisseursAsync()
        {
            return await _context.Fournisseurs
                .Include(f => f.Articles) // Inclut les articles liés à chaque fournisseur
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un fournisseur spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du fournisseur.</param>
        /// <returns>Le fournisseur correspondant ou null s'il n'existe pas.</returns>
        public async Task<Fournisseur?> GetFournisseurByIdAsync(int id)
        {
            return await _context.Fournisseurs
                .Include(f => f.Articles) // Inclut les articles liés à ce fournisseur
                .FirstOrDefaultAsync(f => f.IdFournisseur == id);
        }

        /// <summary>
        /// Ajoute un nouveau fournisseur dans la base de données.
        /// </summary>
        /// <param name="fournisseur">Le fournisseur à ajouter.</param>
        /// <returns>Le fournisseur ajouté avec son ID généré.</returns>
        public async Task<Fournisseur> AddFournisseurAsync(Fournisseur fournisseur)
        {
            _context.Fournisseurs.Add(fournisseur);
            await _context.SaveChangesAsync();
            return fournisseur;
        }

        /// <summary>
        /// Met à jour un fournisseur existant dans la base de données.
        /// </summary>
        /// <param name="fournisseur">Le fournisseur avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateFournisseurAsync(Fournisseur fournisseur)
        {
            var existingFournisseur = await _context.Fournisseurs.FindAsync(fournisseur.IdFournisseur);
            if (existingFournisseur == null) return false;

            _context.Entry(existingFournisseur).CurrentValues.SetValues(fournisseur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un fournisseur par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du fournisseur à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteFournisseurAsync(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null) return false;

            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des fournisseurs par nom (recherche partielle).
        /// </summary>
        /// <param name="name">Le nom ou partie du nom à rechercher.</param>
        /// <returns>Liste des fournisseurs correspondant au critère de recherche.</returns>
        public async Task<List<Fournisseur>> SearchFournisseursByNameAsync(string name)
        {
            return await _context.Fournisseurs
                .Include(f => f.Articles)
                .Where(f => EF.Functions.Like(f.Nom, $"%{name}%"))
                .ToListAsync();
        }
    }
}