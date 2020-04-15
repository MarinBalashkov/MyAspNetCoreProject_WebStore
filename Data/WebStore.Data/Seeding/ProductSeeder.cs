using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public string Name { get; set; }

public string Description { get; set; }

[StringLength(maximumLength: 50)]
public string Color { get; set; }

public decimal Price { get; set; }

public int? ManufacturerId { get; set; }

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

            var names = new List<string>();

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

            var ManufacturerIds = dbContext.Manufacturers.Select(x => x.Id).ToList();
        }
    }
}
