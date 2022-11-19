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
                new Bike("Penny Farthing", new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"))
                {
                    Id = Guid.NewGuid(),
                    ShortDescription = "That iconic bike",
                    FullDescription = "It might look very silly nowadays, but it sure does turn heads."
                },
                new Bike("Aplha Explorer", new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"))
                {
                    Id = Guid.NewGuid(),
                    ShortDescription = "Mud has nothing on it",
                    FullDescription = "The number one choice for mountainous adventures."
                });;

            modelBuilder.Entity<Category>().HasData(
                new Category("Specialty bikes")
                {
                    Id = new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"),
                    Description = "The bikes you always wanted"
                },
                new Category("Mountainbikes")
                {
                    Id = new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"),
                    Description = "Metal goats"
                });
        }
    }
}
