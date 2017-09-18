using System.Data.Entity;

namespace Companies.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("DefaultConnection") { }

        static CompanyContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Company> Companies { get; set; }
    }
}