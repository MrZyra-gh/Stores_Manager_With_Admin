using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StoresManager.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.Repositories
{
    public class StoresManagerDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StoreToProduct> StoreToProducts { get; set; }

        public StoresManagerDbContext()
        {
            this.Stores = this.Set<Store>();
            this.Products = this.Set<Product>();
            this.StoreToProducts = this.Set<StoreToProduct>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
             .UseSqlServer(@"Server=DESKTOP-5MVFMU1\SQLEXPRESS;Database=StoresManagerDB;Trusted_Connection=True;TrustServerCertificate=True;")
             .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "nikiv",
                    Password = "nikipass",
                    FirstName = "Nikola",
                    LastName = "Valchanov"
                });
        }
    }
}
