using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class HistoriqueTemperature
{
    [Key]
    public int IdHistorique { get; set; }

    public DateTime DateMesure { get; set; } // Date d'enregistrement dans le système
    public float Valeur { get; set; }

    // Relation avec Capteur
    public int CapteurId { get; set; } // Clé étrangère
    public Capteur Capteur { get; set; } = null!;
}