using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;

namespace WebStore.Data.Seeding
{
    public class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            var mainImagesUrls = new List<string>()
            {
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964580/NERO_Boutique/Products/10_alwvgd.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964580/NERO_Boutique/Products/5_gpm9lk.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964580/NERO_Boutique/Products/4_vo7mdg.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964580/NERO_Boutique/Products/3_paghks.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964580/NERO_Boutique/Products/2_cmspxh.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/9_lbmlws.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/1_wcdwfq.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/11_vhdl9p.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/12_dfnkwy.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/8_tegjlm.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/6_qxzdsp.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1586964579/NERO_Boutique/Products/7_u7nmb8.jpg",
            };

            var secondaryImageUrls = new List<string>()
            {
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1587057580/NERO_Boutique/Products/3_rzjib1.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1587057580/NERO_Boutique/Products/2_rsbgw2.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1587057580/NERO_Boutique/Products/1_vm0pjq.jpg",
                "https://res.cloudinary.com/dlrc2oa6y/image/upload/v1587057580/NERO_Boutique/Products/4_y3ychy.jpg",
            };

            var productsIds = dbContext.ProductItems.Select(x => x.Id).ToList();
            var images = new List<Image>();
            var random = new Random();

            foreach (var productId in productsIds)
            {
                var mainImageIndex = random.Next(mainImagesUrls.Count - 1);

                var mainiImage = new Image()
                {
                    ProductId = productId,
                    ImageType = ImageType.Main,
                    ImageUrl = mainImagesUrls[mainImageIndex],
                };

                images.Add(mainiImage);
                foreach (var secondaryImageUrl in secondaryImageUrls)
                {
                    var secondaryImage = new Image()
                    {
                        ProductId = productId,
                        ImageType = ImageType.Secondary,
                        ImageUrl = secondaryImageUrl,
                    };
                    images.Add(secondaryImage);
                }
            }

            await dbContext.Images.AddRangeAsync();
            await dbContext.SaveChangesAsync();
        }
    }
}
