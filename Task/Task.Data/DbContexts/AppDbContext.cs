using Microsoft.EntityFrameworkCore;
using Task.Domain.Entites;

namespace Task.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost; Database=AppDb; Port=5432; User Id=postgres; Password=root;";
            optionsBuilder.UseNpgsql(connectionString);
        }
        public DbSet<User> Users { get; set; }
    }
}
