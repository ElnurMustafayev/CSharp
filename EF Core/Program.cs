using App.EntityFramework;
using App.Models;

public class Program {
    private static readonly string[] PHONES = new string [3] { "IPhone 13 pro", "Samsung galaxy s22", "Bananaphone" };

    public static async Task Main() {
        // STEPS

        // dotnet tool uninstall --global dotnet-ef
        // dotnet tool install --global dotnet-ef

        // dotnet add package Microsoft.EntityFrameworkCore.SqlServer   // connect to sql server by code
        // dotnet add package Microsoft.EntityFrameworkCore.Design      // for db migrations

        // Add "DataContext : DbContext" class

        // dotnet ef migrations add "<migration title>"
        // dotnet ef database update

        AppDataContext context = new AppDataContext();

        ProductType newType = new ProductType() {
            Name = "Phone"
        };

        if(context.ProductTypes!.Any(pt => pt.Name == newType.Name) == false) {
            await context.ProductTypes!.AddAsync(newType);
        }
        else {
            newType = context.ProductTypes!.First(pt => pt.Name == newType.Name);
        }

        await context.Products!.AddAsync(entity: new Product() {
            Name = PHONES[new Random().Next(PHONES.Length)],
            ProductType = newType
        });

        await context.SaveChangesAsync();
    }
}