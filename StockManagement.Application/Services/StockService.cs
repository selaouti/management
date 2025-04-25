using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services;

public class StockService
{
    private readonly AppDbContext _context;

    public StockService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllStocksAsync()
    {
        return await _context.Stocks
            .Include(s => s.Lot) // Inclut les lots associés
            .Include(s => s.Entrepot) // Inclut les entrepôts associés
            .ToListAsync();
    }

    public async Task<Stock?> GetStockByIdAsync(int id)
    {
        return await _context.Stocks
            .Include(s => s.Lot)
            .Include(s => s.Entrepot)
            .FirstOrDefaultAsync(s => s.IdStock == id);
    }

    public async Task<Stock> AddStockAsync(Stock stock)
    {
        _context.Stocks.Add(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<bool> UpdateStockAsync(Stock stock)
    {
        var existingStock = await _context.Stocks.FindAsync(stock.IdStock);
        if (existingStock == null) return false;

        _context.Entry(existingStock).CurrentValues.SetValues(stock);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteStockAsync(int id)
    {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock == null) return false;

        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Stock>> GetStocksByLotIdAsync(int lotId)
    {
        return await _context.Stocks
            .Include(s => s.Lot)
            .Include(s => s.Entrepot)
            .Where(s => s.LotId == lotId)
            .ToListAsync();
    }

    public async Task<List<Stock>> GetStocksByEntrepotIdAsync(int entrepotId)
    {
        return await _context.Stocks
            .Include(s => s.Lot)
            .Include(s => s.Entrepot)
            .Where(s => s.EntrepotId == entrepotId)
            .ToListAsync();
    }
}