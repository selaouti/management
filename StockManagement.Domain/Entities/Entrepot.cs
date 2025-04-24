using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Entrepot
{
    [Key]
    public int IdEntrepot { get; set; } // Clé primaire pour Entrepôt

    [Required]
    public required string Nom { get; set; } // Nom de l'entrepôt

    [Required]
    public required string Adresse { get; set; } // Adresse de l'entrepôt

    [Required]
    public required string Localisation { get; set; } // Localisation (propriété manquante ajoutée)

    // Relation avec Stock
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    // Relation avec Capteur
    public ICollection<Capteur> Capteurs { get; set; } = new List<Capteur>();
}