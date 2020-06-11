namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ShoppingCartItemsService : IShoppingCartItemsService
    {
        private readonly IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository;

        public ShoppingCartItemsService(IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository)
        {
            this.shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task DeleteShopingCartItem(string userId, int productItemId)
        {
            var shoppingCartItem = this.shoppingCartItemRepository
                .All()
                .Where(x => x.UserId == userId && x.ProductItemId == productItemId)
                .FirstOrDefault();

            if (shoppingCartItem == null)
            {
                return;
            }

            this.shoppingCartItemRepository.HardDelete(shoppingCartItem);
            await this.shoppingCartItemRepository.SaveChangesAsync();
        }

        public async Task AddShoppingCartItemAsync(string userId, int productItemId, int quantity)
        {
            if (quantity < 0)
            {
                return;
            }

            if (this.shoppingCartItemRepository.AllWithDeleted().Any(x => x.UserId == userId && x.ProductItemId == productItemId))
            {
                await this.UpdateShopingCartItem(userId, productItemId, quantity);
                return;
            }

            var shoppingCartItem = new ShoppingCartItem()
            {
                ProductItemId = productItemId,
                UserId = userId,
                Quantity = quantity,
            };

            await this.shoppingCartItemRepository.AddAsync(shoppingCartItem);
            await this.shoppingCartItemRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllShoppingCartItems<T>(string userId)
        {
            IQueryable<ShoppingCartItem> query =
            this.shoppingCartItemRepository.All()
            .Where(x => x.UserId == userId && x.Quantity > 0);
            if (!query.Any())
            {
                return null;
            }

            return query.To<T>().ToList();
        }

        public async Task DeleteAllShoppingCartItems(string userId)
        {
            var shoppingCartItems = this.shoppingCartItemRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList();

            foreach (var item in shoppingCartItems)
            {
                this.shoppingCartItemRepository.HardDelete(item);
            }

            await this.shoppingCartItemRepository.SaveChangesAsync();
        }

        public async Task UpdateShopingCartItem(string userId, int productItemId, int quantity)
        {
            if (quantity <= 0)
            {
                await this.DeleteShopingCartItem(userId, productItemId);
                return;
            }

            var shoppingCartItem = this.shoppingCartItemRepository
                .All()
                .Where(x => x.UserId == userId && x.ProductItemId == productItemId)
                .FirstOrDefault();

            if (shoppingCartItem == null)
            {
                return;
            }

            shoppingCartItem.Quantity = quantity;

            this.shoppingCartItemRepository.Update(shoppingCartItem);
            await this.shoppingCartItemRepository.SaveChangesAsync();

        }

        public int GetShoppingCartItemsCount(string userId)
        {
            return this.shoppingCartItemRepository.All().Where(x => x.UserId == userId && x.Quantity > 0).Count();
        }

        public decimal GetShoppingCartItemsTotalPrice(string userId)
        {
            IQueryable<ShoppingCartItem> query =
            this.shoppingCartItemRepository.All()
            .Where(x => x.UserId == userId && x.Quantity > 0);
            if (!query.Any())
            {
                return 0;
            }

            var totalPrice = query.Select(x => (decimal)x.Quantity * x.ProductItem.Product.Price).Sum();

            return Math.Round(totalPrice, 2);
        }
    }
}
