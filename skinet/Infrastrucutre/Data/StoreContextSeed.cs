using System.Text.Json;
using Core.Entities;

namespace Infrastrucutre.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productData = File.ReadAllText("../Infrastrucutre/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            if (products is null) return;

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }

       
    }
}
