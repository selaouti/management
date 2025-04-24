using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Domain.Entities;

public class Lot
{
    [Key]
    public int IdLot { get; set; }

    [Required]
    public required string Numero { get; set; }

    [ForeignKey("Article")]
    public int ArticleId { get; set; }
    public required Article Article { get; set; }

    [ForeignKey("Fournisseur")]
    public int FournisseurId { get; set; }
    public required Fournisseur Fournisseur { get; set; }
}