using App.EntityFramework.Configurations;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.EntityFramework;

public class AppDataContext : DbContext
{
    private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=App;";

        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductType>? ProductTypes { get; set; }

        // before connect
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
}