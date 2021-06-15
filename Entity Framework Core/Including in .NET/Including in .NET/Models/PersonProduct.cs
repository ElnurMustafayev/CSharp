using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Including_in_.NET.Models {
    // Model
    public class PersonProduct {
        public int Id { get; set; }

        // Person foreign key
        public int PersonId { get; set; }
        public Person Person { get; set; }

        // Product foreign key
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    // Configuration
    public class PersonProductConfiguration : IEntityTypeConfiguration<PersonProduct> {
        public void Configure(EntityTypeBuilder<PersonProduct> builder) {
            builder.HasOne(p => p.Person)
                .WithMany(p => p.PersonProducts)
                .HasForeignKey(p => p.PersonId)
                //.OnDelete(DeleteBehavior.Cascade)
                ;

            builder.HasOne(p => p.Product)
                .WithMany(p => p.PersonProducts)
                .HasForeignKey(p => p.ProductId)
                //.OnDelete(DeleteBehavior.Cascade)
                ;

            builder.HasKey(p => new { p.PersonId, p.ProductId });
        }
    }
}