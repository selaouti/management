
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StockManagement.Domain.Entities
{
    public class MouvementStock
    {
        [Key]
        public int IdMouvement { get; set; } // Clé primaire
        public string Type { get; set; } // Type de mouvement : entrée, sortie, transfert
        public DateTime DateMouvement { get; set; } // Date du mouvement
        public int Quantite { get; set; } // Quantité déplacée

        // Relation avec Lot
        public int LotId { get; set; }
        public Lot Lot { get; set; }

        // Relation avec Entrepot (source)
        public int? EntrepotSourceId { get; set; }
        public Entrepot EntrepotSource { get; set; }

        // Relation avec Entrepot (destination)
        public int? EntrepotDestinationId { get; set; } // Optionnel pour les transferts
        public Entrepot EntrepotDestination { get; set; }
    }
}