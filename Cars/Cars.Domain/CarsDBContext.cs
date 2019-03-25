using System.Data.Entity;

namespace Cars.Domain
{
    class CarsDBContext : DbContext
    {
        public CarsDBContext(): base("DefaultContext")
        {

        }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}
