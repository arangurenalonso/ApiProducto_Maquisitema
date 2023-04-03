using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;

namespace Infractructure.Persistence
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Text.Json;
    public class ApplicationDbContextSeedData
    {
        public static async Task LoadDataAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Categories!.Any())
                {
                    var categoryData = File.ReadAllText("../Infractructure/Persistence/DefaultData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                    await context.Categories!.AddRangeAsync(categories!);
                    await context.SaveChangesAsync();
                }

                

                if (!context.Products!.Any())
                {
                    var categoriesList = await context.Categories!.ToListAsync();

                    var productData = File.ReadAllText("../Infractructure/Persistence/DefaultData/product.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    foreach (var product in products)
                    {
                        product.Category = categoriesList.Where(x => x.Name == product.Category!.Name).FirstOrDefault();
                    }
                    await context.Products!.AddRangeAsync(products!);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContextSeedData>();
                logger.LogError(ex.Message);
            }
        }

    }
}

