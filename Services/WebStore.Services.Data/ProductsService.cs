namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Data.Models.Enums;
    using WebStore.Services.Mapping;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<ProductItem> productsItemsRepository;
        private readonly IDeletableEntityRepository<CategoryProduct> categoriesProductsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<ProductItem> itemsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository, IDeletableEntityRepository<ProductItem> productsItemsRepository, IDeletableEntityRepository<CategoryProduct> categoriesProductsRepository, IDeletableEntityRepository<Image> imagesRepository, IDeletableEntityRepository<ProductItem> itemsRepository)
        {
            this.productsRepository = productsRepository;
            this.productsItemsRepository = productsItemsRepository;
            this.categoriesProductsRepository = categoriesProductsRepository;
            this.imagesRepository = imagesRepository;
            this.itemsRepository = itemsRepository;
        }

        public IEnumerable<T> GetLatestProducts<T>(int? count = null)
        {
            IQueryable<Product> query = this.productsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetMostLikedProducts<T>(int? count = null)
        {
            IQueryable<Product> query = this.productsRepository
              .All()
              .OrderByDescending(x => x.FavoriteProducts.Count);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetProductById<T>(int id)
        {
            var product = this.productsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return product;
        }


        public IEnumerable<T> GetProductsBySubCategoiesIds<T>(IEnumerable<int> categoriesIds, int? count)
        {
            IQueryable<Product> query = this.productsRepository
              .All()
              .Where(x => x.CategoriesProducts.Any(cp => categoriesIds.Contains(cp.CategoryId)));
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetTopSellingProducts<T>(int? count = null)
        {
            IEnumerable<int> bestSellersProductsIds = this.productsItemsRepository
                .All()
                .OrderByDescending(x => x.OrdersProductItems.Count)
                .Select(x => x.ProductId)
                .ToList()
                .Distinct();

            if (count.HasValue)
            {
                bestSellersProductsIds = bestSellersProductsIds.Take(count.Value);
            }


            IQueryable<Product> query = this.productsRepository
                .All()
                .Where(x => bestSellersProductsIds.Contains(x.Id));

            return query.To<T>().ToList();
        }

        public bool IsExist(int id)
        {
            return this.productsRepository.All().Any(x => x.Id == id);
        }

        public IEnumerable<T> GetProductsByFilterWithPagenation<T>(string parentCategoryName, string childCategoryName, string color, string size, string brandName, string searchString, int? take = null, int skip = 0)
        {
            IQueryable<Product> query = this.GetProductsByFilter(parentCategoryName, childCategoryName, color, size, brandName, searchString);

            if (!query.Any())
            {
                return null;
            }

            query = query.Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IQueryable<Product> GetProductsByFilter(string parentCategoryName, string childCategoryName, string color, string size, string brandName, string searchString)
        {
            IQueryable<Product> query = this.productsRepository
            .All()
            .OrderByDescending(x => x.CreatedOn);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var searchWords = searchString.Split(new char[] { ' ', '?', '&', '^', '$', '#', '@', '!', '(', ')', '+', '-', ',', ':', ';', '<', '>', '\'', '\\', '-', '_', '*', '"' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                // var searchWords = new string(searchString.Where(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)).ToArray()).Split(" ").ToArray();

                foreach (var word in searchWords)
                {
                    query = query.Where(x => x.Name.Contains(word) || x.Color.Contains(word) || x.Description.Contains(word) || x.Manufacturer.Name.Contains(word));
                }
            }

            if (!string.IsNullOrWhiteSpace(parentCategoryName))
            {
                query = query.Where(x => x.CategoriesProducts.Any(cp => cp.Category.ParentCategory.Name.ToLower() == parentCategoryName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(childCategoryName))
            {
                query = query.Where(x => x.CategoriesProducts.Any(cp => cp.Category.Name.ToLower() == childCategoryName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(color))
            {
                query = query.Where(x => x.Color.ToLower() == color.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(size))
            {
                query = query.Where(x => x.ProductItems.Any(pi => pi.Size.ToLower() == size.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(brandName))
            {
                query = query.Where(x => x.Manufacturer.Name.ToLower() == brandName.ToLower());
            }

            return query;
        }

        public IEnumerable<string> GetColors()
        {
            return this.productsRepository.All().Select(x => x.Color).Distinct().ToList();
        }

        public IEnumerable<string> GetSizes()
        {
            return this.productsItemsRepository.All().Select(x => x.Size).Distinct().ToList();
        }

        public IEnumerable<string> GetBrands()
        {
            return this.productsRepository.All().Select(x => x.Manufacturer.Name).Distinct().ToList();
        }

        public async Task<int> CreateAsync(string name, string color, string description, decimal price, int categoryId, IEnumerable<string> secondaryPictursUrls, string mainPicturesUrl, int? manufacturerId = null)
        {
            var product = new Product()
            {
                Name = name,
                Color = color,
                Description = description,
                Price = price,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            await this.CreateCategoriesProductsConnection(categoryId, product.Id);

            if (manufacturerId.HasValue)
            {
                product.ManufacturerId = manufacturerId;
                this.productsRepository.Update(product);
                await this.productsRepository.SaveChangesAsync();
            }

            await this.CreateImages(product.Id ,secondaryPictursUrls, mainPicturesUrl);
            await this.CreateProductItems(product.Id);

            return product.Id;
        }

        private async Task CreateCategoriesProductsConnection(int categoryId, int productId)
        {
            var categoryproduct = new CategoryProduct()
            {
                CategoryId = categoryId,
                ProductId = productId,
            };

            await this.categoriesProductsRepository.AddAsync(categoryproduct);
            await this.categoriesProductsRepository.SaveChangesAsync();
        }

        private async Task CreateImages(int productId,  IEnumerable<string> secondaryPictursUrls, string mainPicturesUrl)
        {
            var mainIamage = new Image()
            {
                ImageUrl = mainPicturesUrl,
                ImageType = ImageType.Main,
                ProductId = productId,
            };

            await this.imagesRepository.AddAsync(mainIamage);

            foreach (var imageUrl in secondaryPictursUrls)
            {
                var image = new Image()
                {
                    ImageUrl = imageUrl,
                    ImageType = ImageType.Secondary,
                    ProductId = productId,
                };
                await this.imagesRepository.AddAsync(image);
            }

            await this.imagesRepository.SaveChangesAsync();
        }

        private async Task CreateProductItems(int productId)
        {
            var sizes = new List<string>() { "XS", "S", "M", "L", "XL", "XXL" };

            foreach (var size in sizes)
            {
                var producteItem = new ProductItem()
                {
                    ProductId = productId,
                    Size = size,
                    Quantity = 100,
                };
                await this.itemsRepository.AddAsync(producteItem);
            }

            await this.itemsRepository.SaveChangesAsync();
        }
    }
}
