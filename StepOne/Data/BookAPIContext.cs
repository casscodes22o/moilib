using Microsoft.EntityFrameworkCore;
using StepOne.Models;

namespace StepOne.Data
{
    public class BookAPIContext : DbContext
    {
        public BookAPIContext(DbContextOptions<BookAPIContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base is added because I dont know
            // Bought is a public enum in the Models folder (sabi ni copilot: so we can use it here to seed data)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Le Grand Meaulnus",
                    Author = "Alain Henri",
                    Purchase = Bought.ONLINE
                },
                new Book
                {
                    Id = 2,
                    Title = "The Summer of Katya",
                    Author = "Trevanian",
                    Purchase = Bought.PSHOP,
                },
                new Book
                {
                    Id = 3,
                    Title = "A Song of Ice and Fire: A Game of Thrones",
                    Author = "George R.R. Martin",
                    Purchase = Bought.BNEW
                },
                new Book
                {
                    Id = 4,
                    Title = "Terrible Tudors",
                    Author = "Terry Deary",
                    Purchase = Bought.PSHOP
                }
            );
        }
        //instantiate DB set instance, migrate changes 
        public DbSet<Book> Books { get; set; }
    }
}
