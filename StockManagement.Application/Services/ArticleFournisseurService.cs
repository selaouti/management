using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    /// <summary>
    /// Service pour gérer les opérations métier liées à la relation entre les articles et les fournisseurs.
    /// </summary>
    public class ArticleFournisseurService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructeur pour injecter le contexte de la base de données.
        /// </summary>
        /// <param name="context">Le contexte de la base de données.</param>
        public ArticleFournisseurService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les liens entre articles et fournisseurs.
        /// </summary>
        /// <returns>Liste de tous les liens Article-Fournisseur.</returns>
        public async Task<List<ArticleFournisseur>> GetAllLinksAsync()
        {
            return await _context.ArticleFournisseurs
                .Include(af => af.Article)
                .Include(af => af.Fournisseur)
                .ToListAsync();
        }

        /// <summary>
        /// Récupère un lien spécifique entre un article et un fournisseur par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du lien.</param>
        /// <returns>Le lien Article-Fournisseur correspondant ou null s'il n'existe pas.</returns>
        public async Task<ArticleFournisseur?> GetLinkByIdAsync(int id)
        {
            return await _context.ArticleFournisseurs
                .Include(af => af.Article)
                .Include(af => af.Fournisseur)
                .FirstOrDefaultAsync(af => af.Id == id);
        }

        /// <summary>
        /// Crée un nouveau lien entre un article et un fournisseur.
        /// </summary>
        /// <param name="articleFournisseur">Le lien Article-Fournisseur à ajouter.</param>
        /// <returns>Le lien ajouté avec son ID généré.</returns>
        public async Task<ArticleFournisseur> AddLinkAsync(ArticleFournisseur articleFournisseur)
        {
            _context.ArticleFournisseurs.Add(articleFournisseur);
            await _context.SaveChangesAsync();
            return articleFournisseur;
        }

        /// <summary>
        /// Met à jour un lien existant entre un article et un fournisseur.
        /// </summary>
        /// <param name="articleFournisseur">Le lien avec les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateLinkAsync(ArticleFournisseur articleFournisseur)
        {
            var existingLink = await _context.ArticleFournisseurs.FindAsync(articleFournisseur.Id);
            if (existingLink == null) return false;

            _context.Entry(existingLink).CurrentValues.SetValues(articleFournisseur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime un lien entre un article et un fournisseur par son ID.
        /// </summary>
        /// <param name="id">L'identifiant unique du lien à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteLinkAsync(int id)
        {
            var articleFournisseur = await _context.ArticleFournisseurs.FindAsync(id);
            if (articleFournisseur == null) return false;

            _context.ArticleFournisseurs.Remove(articleFournisseur);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Recherche des liens par article ou fournisseur.
        /// </summary>
        /// <param name="articleName">Nom de l'article (optionnel).</param>
        /// <param name="fournisseurName">Nom du fournisseur (optionnel).</param>
        /// <returns>Liste des liens correspondant aux critères.</returns>
        public async Task<List<ArticleFournisseur>> SearchLinksAsync(string? articleName, string? fournisseurName)
        {
            var query = _context.ArticleFournisseurs
                .Include(af => af.Article)
                .Include(af => af.Fournisseur)
                .AsQueryable();

            if (!string.IsNullOrEmpty(articleName))
            {
                query = query.Where(af => EF.Functions.Like(af.Article.CodeArticle, $"%{articleName}%"));
            }

            if (!string.IsNullOrEmpty(fournisseurName))
            {
                query = query.Where(af => EF.Functions.Like(af.Fournisseur.Nom, $"%{fournisseurName}%"));
            }

            return await query.ToListAsync();
        }
    }
}