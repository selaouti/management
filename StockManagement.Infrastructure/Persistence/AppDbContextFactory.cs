using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StockManagement.Infrastructure.Persistence;

namespace StockManagement.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Mise à jour avec votre chaîne de connexion SQL Server
            var connectionString = "Server=DESKTOP-0F63BER\\SQL2019;Database=StockManagement;User Id=sa;Password=@dmin123;Trusted_Connection=False;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}