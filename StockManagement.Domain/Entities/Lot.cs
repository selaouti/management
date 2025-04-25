using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Domain.Entities;

public class Lot
{
    [Key]
    public int IdLot { get; set; }

    [Required]
    public required string Numero { get; set; }=null!;

    [ForeignKey("Article")]
    public int ArticleId { get; set; }
    public required Article Article { get; set; }=null!;
    [Required]  
    // Relation avec Stock
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>(); // Ajout de la relation  
    public DateTime DateExpiration { get; set; }
    
    [ForeignKey("Fournisseur")]
    public int FournisseurId { get; set; }
    public required Fournisseur Fournisseur { get; set; }
    // Relation avec MouvementStock (1 -> 0..*)
        public ICollection<MouvementStock>? MouvementsStock { get; set; }
}