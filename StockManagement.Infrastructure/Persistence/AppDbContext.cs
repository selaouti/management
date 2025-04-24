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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration des tables
        modelBuilder.Entity<Article>().ToTable("Articles");
        modelBuilder.Entity<Lot>().ToTable("Lots");
        modelBuilder.Entity<Fournisseur>().ToTable("Fournisseurs");
        modelBuilder.Entity<MouvementStock>().ToTable("MouvementStocks");
        modelBuilder.Entity<Entrepot>().ToTable("Entrepots");
        modelBuilder.Entity<Utilisateur>().ToTable("Utilisateurs");
        modelBuilder.Entity<Capteur>().ToTable("Capteurs");
        modelBuilder.Entity<AlerteStock>().ToTable("AlerteStocks");
        modelBuilder.Entity<Stock>().ToTable("Stocks");
        modelBuilder.Entity<HistoriqueTemperature>().ToTable("HistoriqueTemperatures");

        // Exemple de relation entre Stock et Entrepot
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Entrepot)
            .WithMany(e => e.Stocks)
            .HasForeignKey(s => s.EntrepotId)
            .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade
        modelBuilder.Entity<Lot>()
            .HasOne(l => l.Article)
            .WithMany(a => a.Lots)
            .HasForeignKey(l => l.ArticleId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Lot>()
            .HasOne(l => l.Fournisseur)
            .WithMany(f => f.Lots)
            .HasForeignKey(l => l.FournisseurId)
            .OnDelete(DeleteBehavior.Restrict); 
        // Ajout d'autres relations et configurations spécifiques si nécessaire

        base.OnModelCreating(modelBuilder); // Appeler la méthode de base
    }
}