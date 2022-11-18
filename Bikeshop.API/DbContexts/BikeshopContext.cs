using Bikeshop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bikeshop.API.DbContexts
{
    public class BikeshopContext : DbContext
    {
        public BikeshopContext(DbContextOptions<BikeshopContext> options) : base(options)
        {

        }

        public DbSet<Bike> Bikes { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>().HasData(
                new Bike("Penny Farthing")
                {
                    Id = Guid.NewGuid()
                },
                new Bike("Mountainbike")
                {
                    Id = Guid.NewGuid()
                });
        }
    }
}
