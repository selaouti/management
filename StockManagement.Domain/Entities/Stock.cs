using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Stock
{
    [Key]
    public int IdStock { get; set; }

    public int Quantite { get; set; }

    // Relation avec Entrepot
    public int EntrepotId { get; set; } // Clé étrangère
    public Entrepot Entrepot { get; set; } = null!;

    // Relation avec AlerteStock
    public ICollection<AlerteStock> Alertes { get; set; } = new List<AlerteStock>();
}