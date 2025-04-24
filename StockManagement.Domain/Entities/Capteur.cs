using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Capteur
{
    [Key]
    public int IdCapteur { get; set; }

    [Required]
    public required string Nom { get; set; }

    [Required]
    public required string Type { get; set; }

    [Required]
    public required string Emplacement { get; set; }

    // Relation avec Entrepot
    public int EntrepotId { get; set; } // Clé étrangère
    public Entrepot Entrepot { get; set; } = null!;

    // Relation avec HistoriqueTemperature
    public ICollection<HistoriqueTemperature> Historiques { get; set; } = new List<HistoriqueTemperature>();
}