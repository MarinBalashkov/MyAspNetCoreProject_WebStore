namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Data.Models;

    public interface IProductsService
    {
        Task<int> CreateAsync(string name, string color, string description, decimal price, int categoryId, IEnumerable<string> secondaryPictursUrls, string mainPicturesUrl, int? manufacturerId);

        bool IsExist(int id);

        T GetProductById<T>(int id);

        int GetProductItemQuantity(int id);

        IEnumerable<T> GetLatestProducts<T>(int? count);

        IEnumerable<T> GetTopSellingProducts<T>(int? count);

        IEnumerable<T> GetMostLikedProducts<T>(int? count);

        IEnumerable<T> GetProductsBySubCategoiesIds<T>(IEnumerable<int> categoriesIds, int? count);

        IEnumerable<T> GetProductsByFilterWithPagenation<T>(string parentCategoryName, string childCategoryName, string color, string size, string brandName, string searchString, int? take = null, int skip = 0);

        IQueryable<Product> GetProductsByFilter(string parentCategoryName, string childCategoryName, string color, string size, string brandName, string searchString);

        IEnumerable<string> GetColors();

        IEnumerable<string> GetSizes();

        IEnumerable<string> GetBrands();
    }
}
