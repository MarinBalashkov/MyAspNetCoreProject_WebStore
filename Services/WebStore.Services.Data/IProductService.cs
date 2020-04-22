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

        IEnumerable<T> GetProductsByCategoiesIds<T>(IEnumerable<int> categoriesIds, int? count);

    }
}
