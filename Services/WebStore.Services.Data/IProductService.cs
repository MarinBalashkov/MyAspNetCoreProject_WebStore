namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IProductService
    {
        bool IsExist(int id);

        T GetProductById<T>(int id);

        IEnumerable<T> GetLatestProducts<T>(int? count);

        IEnumerable<T> GetTopSellingProducts<T>(int? count);

        IEnumerable<T> GetMostLikedProducts<T>(int? count);

        IEnumerable<T> GetProductsBySubCategoiesIds<T>(IEnumerable<int> categoriesIds, int? count);

        IEnumerable<T> GetProductsByFilter<T>(int? parentCategoryId, int? childCategoryId, decimal? maxPrice, decimal? minPrice, string color, string size, string brandName);

        IEnumerable<string> GetColors();

        IEnumerable<string> GetSizes();

        IEnumerable<string> GetBrands();
    }
}
