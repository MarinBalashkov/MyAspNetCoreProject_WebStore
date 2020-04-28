using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Common.Repositories;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Services.Data
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IDeletableEntityRepository<FavoriteProduct> favoritesProductsRepository;

        public FavoritesService(IDeletableEntityRepository<FavoriteProduct> favoritesProductsRepository)
        {
            this.favoritesProductsRepository = favoritesProductsRepository;
        }

        IEnumerable<T> IFavoritesService.GetAll<T>(string userId)
        {
            IQueryable<FavoriteProduct> query =
            this.favoritesProductsRepository.All()
            .Where(x => x.UserId == userId);
            if (!query.Any())
            {
                return null;
            }

            return query.To<T>().ToList();
        }

        public async Task AddAsync(string userId, int productId)
        {
            var favoriteProduct = new FavoriteProduct()
            {
                ProductId = productId,
                UserId = userId,
            };

            await this.favoritesProductsRepository.AddAsync(favoriteProduct);
            await this.favoritesProductsRepository.SaveChangesAsync();
        }
    }
}
