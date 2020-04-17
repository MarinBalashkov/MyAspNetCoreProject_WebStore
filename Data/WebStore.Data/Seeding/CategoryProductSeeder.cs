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

            var categotyIds = dbContext.Categories
                .Where(x => x.ParentCartegoryId != null)
                .Select(x => x.Id)
                .ToList();

            var productsIds = dbContext.Products.Select(x => x.Id).ToList();
            var categoryProducts = new List<CategoryProduct>();

            foreach (var categoryId in categotyIds)
            {
                for (int i = 0; i < 50; i++)
                {
                    var categoryProduct = new CategoryProduct()
                    {
                        CategoryId = categoryId,
                        ProductId = productsIds[i],
                    };
                    categoryProducts.Add(categoryProduct);
                }
            }

            await dbContext.CategoriesProducts.AddRangeAsync(categoryProducts);
            await dbContext.SaveChangesAsync();
        }
    }
}
