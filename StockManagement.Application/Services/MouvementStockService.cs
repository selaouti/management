using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services
{
    public class MouvementStockService
    {
        private readonly AppDbContext _context;

        public MouvementStockService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère les mouvements de stock pour un lot spécifique.
        /// </summary>
        /// <param name="lotId">L'identifiant du lot.</param>
        /// <returns>Liste des mouvements associés au lot.</returns>
        public async Task<List<MouvementStock>> GetMouvementsByLotAsync(int lotId)
        {
            return await _context.MouvementStocks
                .Include(ms => ms.EntrepotSource)
                .Include(ms => ms.EntrepotDestination)
                .Where(ms => ms.LotId == lotId)
                .ToListAsync();
        }

        /// <summary>
        /// Récupère les mouvements de stock pour un entrepôt spécifique.
        /// </summary>
        /// <param name="entrepotId">L'identifiant de l'entrepôt.</param>
        /// <returns>Liste des mouvements associés à l'entrepôt.</returns>
        public async Task<List<MouvementStock>> GetMouvementsByEntrepotAsync(int entrepotId)
        {
            return await _context.MouvementStocks
                .Include(ms => ms.Lot)
                .Include(ms => ms.EntrepotSource)
                .Include(ms => ms.EntrepotDestination)
                .Where(ms => ms.EntrepotSourceId == entrepotId || ms.EntrepotDestinationId == entrepotId)
                .ToListAsync();
        }
    }
}