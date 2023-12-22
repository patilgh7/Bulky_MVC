using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSet will create table for us in database named Categories
        public DbSet<Category> Categories { get; set; }

        // If you want insert data into Categories table then use OnModelCreating method and data into it
        // Inserting data Category table also called as Seed Category table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }

                );
        }

        /*
         *Note : When you update related to database like above we have added data in Categories table
         *Go to Tools NuGet Package Manager => Package Manager Console and give following commands :
         *i) add-migration SeedCategoryTable.....(SeedCategoryTable is name given to migration process..you can give any name)
         *ii) update-database
         *
         
         */


    }
}
