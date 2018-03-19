using System.Data.Entity;

namespace AspNet_Async.Models
{
    public class AdventureContext : DbContext
    {
        public AdventureContext() : base("Default")
        {
            Database.SetInitializer<AdventureContext>(null);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
    }
}