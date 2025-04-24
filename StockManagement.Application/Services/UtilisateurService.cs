using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées aux utilisateurs.
    /// </summary>
    public class UtilisateurService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public UtilisateurService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les utilisateurs disponibles dans la base de données.
        /// </summary>
        /// <returns>Liste de tous les utilisateurs.</returns>
        public async Task<List<Utilisateur>> GetAllUtilisateursAsync()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        /// <summary>
        /// Récupère un utilisateur spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'utilisateur.</param>
        /// <returns>L'utilisateur correspondant ou null s'il n'existe pas.</returns>
        public async Task<Utilisateur?> GetUtilisateurByIdAsync(int id)
        {
            return await _context.Utilisateurs.FindAsync(id);
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur dans la base de données.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à ajouter.</param>
        /// <returns>L'utilisateur ajouté avec son ID généré.</returns>
        public async Task<Utilisateur> AddUtilisateurAsync(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();
            return utilisateur;
        }

        /// <summary>
        /// Met à jour un utilisateur existant dans la base de données.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            var existingUtilisateur = await _context.Utilisateurs.FindAsync(utilisateur.IdUtilisateur);
            if (existingUtilisateur == null) return false;

            _context.Entry(existingUtilisateur).CurrentValues.SetValues(utilisateur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un utilisateur par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'utilisateur à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteUtilisateurAsync(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null) return false;

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des utilisateurs par nom ou prénom.
        /// </summary>
        /// <param name="name">Le nom ou le prénom à rechercher.</param>
        /// <returns>Liste des utilisateurs correspondant au critère de recherche.</returns>
        public async Task<List<Utilisateur>> SearchUtilisateursByNameAsync(string name)
        {
            return await _context.Utilisateurs
                .Where(u => EF.Functions.Like(u.Nom, $"%{name}%") || EF.Functions.Like(u.Prenom, $"%{name}%"))
                .ToListAsync();
        }

        /// <summary>
        /// Authentifie un utilisateur en fonction de son email et de son mot de passe.
        /// </summary>
        /// <param name="email">L'email de l'utilisateur.</param>
        /// <param name="password">Le mot de passe de l'utilisateur.</param>
        /// <returns>L'utilisateur authentifié ou null si les informations sont incorrectes.</returns>
        public async Task<Utilisateur?> AuthenticateUtilisateurAsync(string email, string password)
        {
            return await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}