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
    }
}
