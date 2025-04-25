using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Fournisseur
{
    [Key]
    public int IdFournisseur { get; set; }

    [Required]
    public required string Nom { get; set; }

    [Required]
    public required string Adresse { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Telephone { get; set; }

    // Relation avec Article
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<Lot> Lots { get; set; }= new List<Lot>();
    // Relation inverse avec ArticleFournisseur
    public required ICollection<ArticleFournisseur> ArticleFournisseurs { get; set; }
}