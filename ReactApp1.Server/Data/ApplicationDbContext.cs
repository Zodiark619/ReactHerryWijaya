using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Models.Project1VendingMachine;
using ReactApp1.Server.Models.Project2Exercise;

namespace ReactApp1.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
        //project2
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>().HasData(new MenuItem
            {
                Id = 1,
                Name = "Spring Roll",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/carrot love.jpg",
                Price = 7.99,
                Category = "Appetizer",
                SpecialTag = ""
            },
            new MenuItem
            {
                Id = 2,
                Name = "Samosa",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/hakka noodles.jpg",
                Price = 8.99,
                Category = "Appetizer",
                SpecialTag = ""
            },
            new MenuItem
            {
                Id = 3,
                Name = "Soup",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/idli.jpg",
                Price = 8.99,
                Category = "Appetizer",
                SpecialTag = "Best Seller"
            },
            new MenuItem
            {
                Id = 4,
                Name = "Noodles",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/malai kofta.jpg",
                Price = 10.99,
                Category = "Entrée",
                SpecialTag = ""
            },
            new MenuItem
            {
                Id = 5,
                Name = "Pav Bhaji",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/paneer pizza.jpg",
                Price = 12.99,
                Category = "Entrée",
                SpecialTag = "Top Rated"
            },
            new MenuItem
            {
                Id = 6,
                Name = "Paneer Pizza",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/paneer tikka.jpg",
                Price = 11.99,
                Category = "Entrée",
                SpecialTag = ""
            },
            new MenuItem
            {
                Id = 7,
                Name = "Mango Paradise",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/pani puri.jpg",
                Price = 13.99,
                Category = "Dessert",
                SpecialTag = "Chef's Special"
            },
            new MenuItem
            {
                Id = 8,
                Name = "Carrot Love",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/rasmalai.jpg",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = ""
            },
            new MenuItem
            {
                Id = 9,
                Name = "Sweet Rolls",
                Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                Image = "images/spring roll.jpg",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "Chef's Special"
            });


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
        }
    }
}
