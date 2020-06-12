namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class FavoritesService : IFavoritesService
    {
        private readonly IDeletableEntityRepository<FavoriteProduct> favoritesProductsRepository;

        public FavoritesService(IDeletableEntityRepository<FavoriteProduct> favoritesProductsRepository)
        {
            this.favoritesProductsRepository = favoritesProductsRepository;
        }

        IEnumerable<T> IFavoritesService.GetAllByUserId<T>(string userId, int? take = null, int skip = 0)
        {
            IQueryable<FavoriteProduct> query =
            this.favoritesProductsRepository.All()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedOn);
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

        public int GetCountGyUserId(string userId)
        {
            return this.favoritesProductsRepository.All().Count(x => x.UserId == userId);
        }

        public bool IsInMyFavorites(string userId, int productId)
        {
            return this.favoritesProductsRepository.All().Any(x => x.UserId == userId && x.ProductId == productId);
        }
    }
}
