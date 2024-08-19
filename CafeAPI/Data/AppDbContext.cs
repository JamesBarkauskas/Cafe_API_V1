using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CafeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>().HasData(
                new Food
                {
                    Id = 1,
                    Name = "Sandwich",
                    Price = 4.99,
                    Details = "Egg and Cheese",
                    ImageUrl = "",
                    CreatedDate = DateTime.Now
                },
                new Food
                {
                    Id = 2,
                    Name = "Soup",
                    Price = 2.99,
                    Details = "Loaded Potato soup",
                    ImageUrl = "",
                    CreatedDate = DateTime.Now
                },
                new Food
                {
                    Id = 3,
                    Name = "Muffin",
                    Price = 1.99,
                    Details = "Warm delicious muffin",
                    ImageUrl = "",
                    CreatedDate = DateTime.Now
                }
                );
        }
    }
}
