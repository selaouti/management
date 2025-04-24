namespace StockManagement.Domain.Entities
{
    public class ArticleFournisseur
    {
        public int Id { get; set; }
        public required string ArticleName { get; set; } // Propriété obligatoire
        public required string FournisseurName { get; set; } // Propriété obligatoire
        public decimal Prix { get; set; }
        public DateTime DateLivraison { get; set; }
    }
}