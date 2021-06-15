using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Including_in_.NET.Models {
    // Model
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<PersonProduct> PersonProducts { get; set; }  // Many to Many

        public Product(string name, decimal price) {
            Name = name;
            Price = price;
        }
    }

    // Configuration
    public class ProductConfiguration : IEntityTypeConfiguration<Product> {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            //builder.Property(p => p.Price);
        }
    }
}
