using System;
using System.Reflection;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data.SeedData;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        try
        {
            //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            if (!context.Products.Any())
            {
                // Serialization data from json file
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products == null) return;
                context.Products.AddRange(products);
                
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            // var logger = context.GetService<ILogger<StoreContextSeed>>();
            // logger.LogError(ex, "An error occurred during seeding");

            Console.WriteLine(ex.Message);
        }
    }
}
