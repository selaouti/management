using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StockManagement.Domain.Interfaces
{
    /// <summary>
    /// Interface générique pour les opérations CRUD de base.
    /// </summary>
    /// <typeparam name="TEntity">Type de l'entité sur laquelle les opérations seront effectuées.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Récupère tous les objets de l'entité.
        /// </summary>
        /// <returns>Liste de toutes les entités.</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Récupère une entité par son identifiant unique.
        /// </summary>
        /// <param name="id">Identifiant unique de l'entité.</param>
        /// <returns>Entité correspondante ou null si introuvable.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Récupère les entités correspondant au filtre spécifié.
        /// </summary>
        /// <param name="predicate">Expression de filtre.</param>
        /// <returns>Liste des entités correspondant au filtre.</returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Ajoute une nouvelle entité.
        /// </summary>
        /// <param name="entity">Entité à ajouter.</param>
        /// <returns>Entité ajoutée.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Met à jour les informations d'une entité.
        /// </summary>
        /// <param name="entity">Entité à mettre à jour.</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Supprime une entité.
        /// </summary>
        /// <param name="entity">Entité à supprimer.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Supprime une entité par son identifiant unique.
        /// </summary>
        /// <param name="id">Identifiant unique de l'entité.</param>
        Task DeleteByIdAsync(int id);
    }
}