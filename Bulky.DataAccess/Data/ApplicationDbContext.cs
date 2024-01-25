using BulkyBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


// ASP.NET Core Identity: Is an API that supports user interface (UI) login functionality.

//Manages users, passwords, profile data, roles, claims, tokens, email confirmation, and more.


// For Adding IdentityDbContext class you have install NuGet Package
// Package Name : Microsoft.AspNetCore.Identity.EntityFrameworkCore(8.0.0)
// Make sure all version numbers are same while package installing like (8.0.0) 
// Do not install updated version like 8.0.1 or lesser version than 8.0.0 (We have all packages with 8.0.0 version) 

// Steps For Adding Identity functionalities :
// 1. public class ApplicationDbContext : IdentityDbContext  ....Inherit from IdentityDbContext class
// 2. Install Package : Microsoft.AspNetCore.Identity.EntityFrameworkCore(8.0.0) for IdentityDbContext class
// 3. base.OnModelCreating(modelBuilder);.....Add this line in void OnModelCreating(ModelBuilder modelBuilder) method
// 4. This is most important step "Scaffold Identity" :
//      Right click on BulkyBookWeb => Add => New Scaffolded Item => Identity => Add
//      => Tick mark checkbox of Override all files => Go to DbContext class : Select ApplicationDbContext file => then Add.                                                                         

// Note : After Scaffold Identity step you have to fix issue that occurs due to duplicate file of ApplicationDbContext.cs
// BulkyBookWeb Project in Areas => Inside Identity Folder => Data => ApplicationDbContext.cs file is created automatically 
// while Scaffolding so you have to delete that file.


namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        // DbSet will create table for us in database named Categories
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    

        // If you want insert data into Categories table then use OnModelCreating method and data into it
        // Inserting data Category table also called as Seed Category table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            // When you add IdentityDbContext class then add below line compulsory 

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }

                );

            // CategoryId = 1    This is Foreign Key

            modelBuilder.Entity<Company>().HasData(
             new Company { Id = 1, Name = "Tech Solution", StreetAddress = "123 Tech st", City = "Tech City", 
                 PostalCode = "12121", State = "IL", PhoneNumber ="6669990000" },

             new Company
             {
                 Id = 2,
                 Name = "Vivid Books",
                 StreetAddress = "999 vid st",
                 City = "Vid City",
                 PostalCode = "66666",
                 State = "IL",
                 PhoneNumber = "7779990000"
             },

             new Company
             {
                 Id = 3,
                 Name = "Reader Club",
                 StreetAddress = "999 Main st",
                 City = "Lala land",
                 PostalCode = "999999",
                 State = "NY",
                 PhoneNumber = "111333555"
             }

             );

            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl = ""
                }

                );


        }

        /*
         *Note : When you update related to database like above we have added data in Categories table
         *Go to Tools NuGet Package Manager => Package Manager Console and give following commands :
         *One more thing to note when you going to hit the following command make sure that in package manager console
         *Select Default Project : __________ Properly and then give command otherwise it will give error.
         *i) add-migration SeedCategoryTable.....(SeedCategoryTable is name given to migration process..you can give any name)
         *ii) update-database
         *
         
         */


    }
}
