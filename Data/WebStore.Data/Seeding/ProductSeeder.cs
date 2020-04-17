using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;

namespace WebStore.Data.Seeding
{
    public class ProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            var colors = new List<string>()
            {
                "Red",
                "Orange",
                "Yellow",
                "Green",
                "Blue",
                "Purple",
                "Brown",
                "Magenta",
                "Tan",
                "Cyan",
                "Olive",
                "Maroon",
                "Navy",
                "Aquamarine",
                "Turquoise",
                "Silver",
                "Lime",
                "Teal",
                "Indigo",
                "Violet",
                "Pink",
                "Black",
                "White",
            };

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin pharetra tempor so dales. Phasellus sagittis auctor gravida. Integer bibendum sodales arcu id te mpus. Ut consectetur lacus leo, non scelerisque nulla euismod nec.";
            var manufacturerIds = dbContext.Manufacturers.Select(x => x.Id).ToList();
            var names = new List<string>()
            {
                "Black and White Stripes Dress",
                "Flamboyant Pink Top",
                "Animal Print Dress",
                "Ruffle Pink Top",
                "Skinny Jeans",
                "WHITE PEPLUM TOP",
            };

            var random = new Random();

            var products = new List<Product>();

            for (int i = 0; i < 150; i++)
            {
                var colorIndex = random.Next(colors.Count - 1);
                var manufacturerIndexx = random.Next(manufacturerIds.Count - 1);
                var nameIndex = random.Next(names.Count - 1);

                var product = new Product()
                {
                    Color = colors[colorIndex],
                    ManufacturerId = manufacturerIds[manufacturerIndexx],
                    Name = names[nameIndex],
                    Description = description,
                    Price = 10 + i,
                };

                products.Add(product);
            }

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
        }
    }
}
