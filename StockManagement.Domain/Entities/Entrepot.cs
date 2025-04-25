using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Entrepot
{
        [Key]
        public int IdEntrepot { get; set; } // Clé primaire
        public required string Nom { get; set; }
        public required string Adresse { get; set; }
        public required string Localisation { get; set; }
        // Relation avec Stock (Un entrepôt peut contenir plusieurs stocks)
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

        // Mouvements provenant de cet entrepôt
        public ICollection<MouvementStock> MouvementsSource { get; set; } = new List<MouvementStock>();

        // Mouvements arrivant dans cet entrepôt
        public ICollection<MouvementStock> MouvementsDestination { get; set; } = new List<MouvementStock>();
}