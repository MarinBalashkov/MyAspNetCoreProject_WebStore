using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;

namespace WebStore.Data.Seeding
{
    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var parentCategoriesNames = new List<string>() { "Men", "Women", "Kids" };
            var childCategoriesNames = new List<string>() { "Coats", "Dresses‎", "Footwears", "Jackets‎", "Jeans", "Lounge wear", "T-Shirts", "Shorts" };

            foreach (var parentCategoyName in parentCategoriesNames)
            {
                var parentCategory = new Category() { Name = parentCategoyName };

                await dbContext.Categories.AddAsync(parentCategory);
                await dbContext.SaveChangesAsync();
                var childCategories = new List<Category>();

                foreach (var childCategoryNames in childCategoriesNames)
                {
                    var childCategory = new Category()
                    {
                        Name = childCategoryNames,
                        ParentCartegoryId = parentCategory.Id,
                    };

                    childCategories.Add(childCategory);
                }

                await dbContext.Categories.AddRangeAsync(childCategories);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
