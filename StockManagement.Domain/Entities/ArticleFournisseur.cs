namespace StockManagement.Domain.Entities
{
    /// <summary>
    /// Représente une relation entre un article et un fournisseur.
    /// </summary>
    public class ArticleFournisseur
    {
        // Identifiant unique pour cette relation
        public int Id { get; set; }

        // Clé étrangère pour l'Article
        public int ArticleId { get; set; }
        public required Article Article { get; set; } // Propriété de navigation vers l'entité Article

        // Clé étrangère pour le Fournisseur
        public int FournisseurId { get; set; }
        public required Fournisseur Fournisseur { get; set; } // Propriété de navigation vers l'entité Fournisseur

        // Propriétés supplémentaires de la relation
        public decimal Prix { get; set; } // Prix attribué à la relation
        public DateTime DateLivraison { get; set; } // Date prévue pour la livraison
    }
}