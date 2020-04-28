namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Data.Models.Enums;
    using WebStore.Services.Mapping;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<ProductItem> productsItemsRepository;

        public ProductService(IDeletableEntityRepository<Product> productsRepository, IDeletableEntityRepository<ProductItem> productsItemsRepository)
        {
            this.productsRepository = productsRepository;
            this.productsItemsRepository = productsItemsRepository;
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

        public IEnumerable<T> GetProductsByFilter<T>(int? parentCategoryId = null, int? childCategoryId = null, decimal? maxPrice = null, decimal? minPrice = null, string color = null, string size = null, string brandName = null)
        {
            IQueryable<Product> query = this.productsRepository
            .All();

            if (parentCategoryId.HasValue)
            {
                query = query.Where(x => x.CategoriesProducts.Any(cp => cp.Category.ParentCartegoryId == parentCategoryId));
            }

            if (childCategoryId.HasValue)
            {
                query = query.Where(x => x.CategoriesProducts.Any(cp => cp.CategoryId == childCategoryId));
            }

            if (maxPrice.HasValue && minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            }

            if (color != null)
            {
                query = query.Where(x => x.Color == color);
            }

            if (color != null)
            {
                query = query.Where(x => x.ProductItems.Any(pi => pi.Size == size));
            }

            if (brandName != null)
            {
                query = query.Where(x => x.Manufacturer.Name == brandName);
            }

            return query.To<T>().ToList();
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
    }
}
