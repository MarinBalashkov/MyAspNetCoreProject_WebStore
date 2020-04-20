namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IProductService
    {
        IEnumerable<T> GetLatestProducts<T>(int? count);

        IEnumerable<T> GetTopSellingProducts<T>(int? count);

        IEnumerable<T> GetMostLikedProducts<T>(int? count);
    }
}
