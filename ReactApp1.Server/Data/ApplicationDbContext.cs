using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Models.Project1VendingMachine;

namespace ReactApp1.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }



        //project1vendingmachine
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name="Drinks"
                },
                new Category
                {
                    Id = 2,
                    Name = "Foods"
                }
                );
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name="Food A",
                    CategoryId=2,
                    Price=32.21m
                },
                 new Item
                 {
                     Id = 2,
                     Name = "Food B",
                     CategoryId = 2,
                     Price = 99.21m
                 },
                  new Item
                  {
                      Id = 3,
                      Name = "Drink A",
                      CategoryId = 1,
                      Price = 2.54321m
                  },
                   new Item
                   {
                       Id = 4,
                       Name = "Drink B",
                       CategoryId = 1,
                       Price = 3.2121m
                   }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
