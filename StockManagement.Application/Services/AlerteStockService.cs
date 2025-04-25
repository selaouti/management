using StockManagement.Domain.Entities;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Application.Services;

public class AlerteStockService
{
    private readonly AppDbContext _context;

    public AlerteStockService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<AlerteStock> GetAll()
    {
        return _context.AlerteStocks.ToList();
    }

    public AlerteStock GetById(int id)
    {
        return _context.AlerteStocks.Find(id);
    }

    public AlerteStock Create(AlerteStock alerteStock)
    {
        _context.AlerteStocks.Add(alerteStock);
        _context.SaveChanges();
        return alerteStock;
    }

    public AlerteStock Update(AlerteStock alerteStock)
    {
        _context.AlerteStocks.Update(alerteStock);
        _context.SaveChanges();
        return alerteStock;
    }

    public bool Delete(int id)
    {
        var alerteStock = _context.AlerteStocks.Find(id);
        if (alerteStock == null)
        {
            return false;
        }

        _context.AlerteStocks.Remove(alerteStock);
        _context.SaveChanges();
        return true;
    }
}