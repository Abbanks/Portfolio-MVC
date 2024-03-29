using Microsoft.EntityFrameworkCore;
 
using Portfolio.Models.Entity;

namespace Portfolio.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AdminInfo> AdminInfos { get; set; }
        public DbSet<WorkHistory> WorkHistories { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}
