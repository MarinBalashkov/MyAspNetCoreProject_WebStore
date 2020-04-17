using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;

namespace WebStore.Data.Seeding
{
    public class ProductItemsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductItems.Any())
            {
                return;
            }

            var sizes = new List<string>() { "S", "M", "L", "XL", "XXL" };
            var productsIds = dbContext.Products.Select(x => x.Id).ToList();
            var productItems = new List<ProductItem>();
            foreach (var productId in productsIds)
            {
                foreach (var size in sizes)
                {
                    var productItem = new ProductItem()
                    {
                        ProductId = productId,
                        Size = size,
                        Quantity = 3,
                    };

                    productItems.Add(productItem);
                }
            }

            await dbContext.ProductItems.AddRangeAsync(productItems);
            await dbContext.SaveChangesAsync();
        }
    }
}
