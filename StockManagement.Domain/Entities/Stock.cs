using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Domain.Entities;

public class Stock
{
    [Key]
    public int IdStock { get; set; }

    public int Quantite { get; set; }

    // Relation avec Entrepot
    public int EntrepotId { get; set; } // Clé étrangère
    public Entrepot Entrepot { get; set; } = null!;
 // Relation avec Lot
    public int LotId { get; set; } // Clé étrangère
    public Lot Lot { get; set; } = null!;
    // Relation avec AlerteStock
    public ICollection<AlerteStock> Alertes { get; set; } = new List<AlerteStock>();
}