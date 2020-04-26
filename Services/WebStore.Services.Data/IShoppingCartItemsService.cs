namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IShoppingCartItemsService
    {
        IEnumerable<T> GetAllShoppingCartItems<T>(string userId);

        Task AddShoppingCartItemAsync(string userId, int productItemId, int quantity);

        Task UpdateShopingCartItem(string userId, int productItemId, int quantity);

        Task DeleteShopingCartItem(string userId, int productItemId);

        Task DeleteAllShoppingCartItems(string userId);

    }
}
