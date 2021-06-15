using Including_in_.NET.Models;
using System;
using System.Linq;

namespace Including_in_.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Include "Microsoft.EntityFrameworkCore" for access DbContext
            // 2. Create DbContext class with: all tables(DbSet), OnConfiguring()
            // 3. Include "Microsoft.EntityFrameworkCore.SqlServer" for connect to SQL Server in overrided "OnConfiguring()" method
            // 4. Create Configures for each model
            // 5. Add Configures in DbContext.OnModelCreating() method
            // 6. Include "Microsoft.EntityFrameworkCore.Design" for migrations
            // 7. dotnet ef migrations add "init"
            // 8. dotnet ef database update

            ElnurDbContext context = new ElnurDbContext();
            if (!context.Database.EnsureCreated()) {
                var query = from p in context.People
                            where p.Age > 65
                            select new { p.Name, p.Age };

                query.ToList().ForEach(p => Console.WriteLine($"{p.Name} {p.Age}"));
            }
        }
    }
}
