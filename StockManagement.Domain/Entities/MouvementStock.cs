using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class MouvementStock
{
    [Key]
    public int IdMouvement { get; set; }

    [Required]
    public required string Type { get; set; } // Par exemple : Entrée ou Sortie

    public DateTime DateMouvement { get; set; }
    public int Quantite { get; set; }

    // Relation avec Lot
    public int LotId { get; set; } // Clé étrangère
    public Lot Lot { get; set; } = null!;
}