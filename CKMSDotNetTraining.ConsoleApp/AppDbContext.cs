
using Microsoft.EntityFrameworkCore;

namespace CKMSDotNetTraining.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                String connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!;TrustServerCertificate=true;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Models.BlogEFCoreDataModel> Blogs { get; set; }
    }
}
