using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Including_in_.NET.Models {
    // Model
    public class Person {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public IEnumerable<PersonProduct> PersonProducts { get; set; }  // Many to Many

        public Person(string name, string surname, int age) {
            Name = name;
            Surname = surname;
            Age = age;
        }
    }

    // Configuration
    public class PersonConfiguration : IEntityTypeConfiguration<Person> {
        public void Configure(EntityTypeBuilder<Person> builder) {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Surname)
                .IsRequired()
                .HasMaxLength(30);

            //builder.Property(p => p.Age);
        }
    }
}