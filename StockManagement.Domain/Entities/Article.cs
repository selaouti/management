using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Article
{
    [Key]
    public int IdArticle { get; set; }

    [Required]
    public required string Codification { get; set; }

    [Required]
    public required string Designation { get; set; }

    [Required]
    public required string CodeArticle { get; set; }

    [Required]
    public required string CodeBarre { get; set; }

    [Required]
    public required string Unite { get; set; }

    // Relation avec Fournisseur
    public int FournisseurId { get; set; } // Clé étrangère
    public Fournisseur Fournisseur { get; set; } = null!;

    // Relation avec Lot
    public ICollection<Lot> Lots { get; set; } = new List<Lot>();
}