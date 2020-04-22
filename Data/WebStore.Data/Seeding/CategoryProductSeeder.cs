using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;

namespace WebStore.Data.Seeding
{
    public class CategoryProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CategoriesProducts.Any())
            {
                return;
            }

            var categoriesIds = dbContext.Categories
                .Where(x => x.ParentCartegoryId != null)
                .Select(x => x.Id)
                .ToList();

            var productsIds = dbContext.Products.Select(x => x.Id).ToList();
            var categoryProducts = new List<CategoryProduct>();

            var random = new Random();

            foreach (var productId in productsIds)
            {
                var categoryIndex = random.Next(categoriesIds.Count - 1);
                var categoryProduct = new CategoryProduct()
                {
                    ProductId = productId,
                    CategoryId = categoriesIds[categoryIndex],
                };
                categoryProducts.Add(categoryProduct);
            }

            await dbContext.CategoriesProducts.AddRangeAsync(categoryProducts);
            await dbContext.SaveChangesAsync();
        }
    }
}
