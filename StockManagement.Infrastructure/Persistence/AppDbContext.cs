using Microsoft.EntityFrameworkCore;
using StockManagement.Domain.Entities;

namespace StockManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Déclaration des DbSet pour les entités
    public DbSet<Article> Articles { get; set; }
    public DbSet<Fournisseur> Fournisseurs { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<MouvementStock> MouvementStocks { get; set; }
    public DbSet<Entrepot> Entrepots { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Capteur> Capteurs { get; set; }
    public DbSet<AlerteStock> AlerteStocks { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<HistoriqueTemperature> HistoriqueTemperatures { get; set; }
    public DbSet<ArticleFournisseur> ArticleFournisseurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration des noms des tables
        modelBuilder.Entity<Article>().ToTable("Articles");
        modelBuilder.Entity<Fournisseur>().ToTable("Fournisseurs");
        modelBuilder.Entity<Lot>().ToTable("Lots");
        modelBuilder.Entity<MouvementStock>().ToTable("MouvementStocks");
        modelBuilder.Entity<Entrepot>().ToTable("Entrepots");
        modelBuilder.Entity<Utilisateur>().ToTable("Utilisateurs");
        modelBuilder.Entity<Capteur>().ToTable("Capteurs");
        modelBuilder.Entity<AlerteStock>().ToTable("AlerteStocks");
        modelBuilder.Entity<Stock>().ToTable("Stocks");
        modelBuilder.Entity<HistoriqueTemperature>().ToTable("HistoriqueTemperatures");
        modelBuilder.Entity<ArticleFournisseur>().ToTable("ArticleFournisseurs");

        // Configuration des relations

        // Relation : Stock -> Entrepot (Un entrepôt peut contenir plusieurs stocks)
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Entrepot)
            .WithMany(e => e.Stocks)
            .HasForeignKey(s => s.EntrepotId)
            .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade

        // Relation : Lot -> Article (Un article peut être associé à plusieurs lots)
        modelBuilder.Entity<Lot>()
            .HasOne(l => l.Article)
            .WithMany(a => a.Lots)
            .HasForeignKey(l => l.ArticleId)
            .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade

        // Relation : Lot -> Fournisseur (Un fournisseur peut fournir plusieurs lots)
        modelBuilder.Entity<Lot>()
            .HasOne(l => l.Fournisseur)
            .WithMany(f => f.Lots)
            .HasForeignKey(l => l.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade

        // Configuration relation : ArticleFournisseur (Relation entre Article et Fournisseur)
        modelBuilder.Entity<ArticleFournisseur>()
            .HasOne(af => af.Article) // Relation avec l'entité Article
            .WithMany(a => a.ArticleFournisseurs) // Un Article peut avoir plusieurs relations avec des Fournisseurs
            .HasForeignKey(af => af.ArticleId) // Utilise ArticleId comme clé étrangère
            .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade

        modelBuilder.Entity<ArticleFournisseur>()
            .HasOne(af => af.Fournisseur) // Relation avec l'entité Fournisseur
            .WithMany(f => f.ArticleFournisseurs) // Un Fournisseur peut avoir plusieurs relations avec des Articles
            .HasForeignKey(af => af.FournisseurId) // Utilise FournisseurId comme clé étrangère
            .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade
            // Configurer la clé primaire pour Entrepot
                modelBuilder.Entity<Entrepot>()
                .HasKey(e => e.IdEntrepot); // Déclare explicitement la clé primaire
              // Relation : MouvementStock -> Entrepot (source)
        modelBuilder.Entity<MouvementStock>()
        .HasOne(ms => ms.EntrepotSource)
        .WithMany(e => e.MouvementsSource) // Source des mouvements
        .HasForeignKey(ms => ms.EntrepotSourceId)
        .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade pour éviter des conflits

                // Relation : MouvementStock -> Entrepot (destination)
        modelBuilder.Entity<MouvementStock>()
        .HasOne(ms => ms.EntrepotDestination)
        .WithMany(e => e.MouvementsDestination) // Destination des mouvements
        .HasForeignKey(ms => ms.EntrepotDestinationId)
        .OnDelete(DeleteBehavior.Restrict); // Pas de suppression en cascade pour éviter des conflits

                // Relation : MouvementStock -> Lot
        modelBuilder.Entity<MouvementStock>()
        .HasOne(ms => ms.Lot)
        .WithMany(l => l.MouvementsStock)
        .HasForeignKey(ms => ms.LotId)
        .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade si un Lot est supprimé
    // Relation : Lot -> Stock (Un lot peut avoir plusieurs stocks)
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Lot)
            .WithMany(l => l.Stocks)
            .HasForeignKey(s => s.LotId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation : Stock -> Entrepot (Un stock est associé à un entrepôt unique)
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Entrepot)
            .WithMany(e => e.Stocks)
            .HasForeignKey(s => s.EntrepotId)
            .OnDelete(DeleteBehavior.Cascade);

    // Appeler la méthode de base pour appliquer les configurations restantes
    base.OnModelCreating(modelBuilder);
    }
}