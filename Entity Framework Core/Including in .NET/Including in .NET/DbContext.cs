using Including_in_.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Including_in_.NET
{
    // Database
    class ElnurDbContext : DbContext
    {
        // conn
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PeopleWithProducts;";

        // Tables
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PersonProduct> PersonProduct { get; set; }

        // Configure settings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // extension method from "Microsoft.EntityFrameworkCore.SqlServer"
            optionsBuilder.UseSqlServer(this.connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        // Apply configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PersonProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
