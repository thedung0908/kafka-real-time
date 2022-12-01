using Microsoft.EntityFrameworkCore;

namespace SeedData
{
    public class DataProvidersDbContext : DbContext
    {
        public DataProvidersDbContext(DbContextOptions<DataProvidersDbContext> options) :
            base(options)
        {
        }

        public virtual DbSet<LondonDaily> LondonDaily { get; set; }
        public virtual DbSet<FrankfurtDaily> FrankfurtDaily { get; set; }

        public virtual DbSet<InstrumentPrice> InstrumentPrice { get; set; }

    }
}
