using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class AlerteStock
{
    [Key]
    public int IdAlerteStock { get; set; }

    [Required] // Marque cette propriété comme obligatoire
    public required string Type { get; set; } // Utilisez 'required' pour éviter les avertissements

    [Required]
    public required string Message { get; set; }

    public DateTime DateAlerte { get; set; }
    public bool EstResolue { get; set; }

    // Propriété de navigation pour la relation avec Stock
    public int StockId { get; set; } // Clé étrangère
    public Stock Stock { get; set; } = null!;
}