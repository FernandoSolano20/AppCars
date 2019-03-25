using System.Data.Entity;

namespace Cars.Models
{
    public class CarsDBContext : DbContext
    {
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}