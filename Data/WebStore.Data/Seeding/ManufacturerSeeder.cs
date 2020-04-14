using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;

namespace WebStore.Data.Seeding
{
    public class ManufacturerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Manufacturers.Any())
            {
                return;
            }

            var manufacturers = new List<(string name, string url)>()
            {
                ("Adidas", "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586880647/NERO_Boutique/Manufacturers_Logos/Adidas_tbuvgu.jpg"),
                ("Channel", "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586879888/NERO_Boutique/Manufacturers_Logos/Channel_vaqsmy.jpg"),
                ("Guess", "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586880647/NERO_Boutique/Manufacturers_Logos/Guess_jmm0qt.jpg"),
                ("LouisVuitton", "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586880289/NERO_Boutique/Manufacturers_Logos/LouisVuitton_ouofgz.jpg"),
                ("Nike", "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586880647/NERO_Boutique/Manufacturers_Logos/Nike_rmkwvo.jpg"),
                ("Versace", "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586880647/NERO_Boutique/Manufacturers_Logos/Versace_aijrqd.jpg"),
            };


            foreach (var item in manufacturers)
            {
                var manufacturer = new Manufacturer()
                {
                    Name = item.name,
                    LogoUrl = item.url,
                };

                await dbContext.Manufacturers.AddAsync(manufacturer);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
