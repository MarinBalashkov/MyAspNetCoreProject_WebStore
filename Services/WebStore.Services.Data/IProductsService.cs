﻿namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<int> CreateAsync(string name, string color, string description, decimal price, int categoryId, IEnumerable<string> secondaryPictursUrls, string mainPicturesUrl, int? manufacturerId);

        bool IsExist(int id);

        T GetProductById<T>(int id);

        IEnumerable<T> GetLatestProducts<T>(int? count);

        IEnumerable<T> GetTopSellingProducts<T>(int? count);

        IEnumerable<T> GetMostLikedProducts<T>(int? count);

        IEnumerable<T> GetProductsBySubCategoiesIds<T>(IEnumerable<int> categoriesIds, int? count);

        IEnumerable<T> GetProductsByFilter<T>(string parentCategoryName, string childCategoryName, string color, string size, string brandName, int? take = null, int skip = 0);

        int GetCountByFilter(string parentCategoryName, string childCategoryName, string color, string size, string brandName);

        IEnumerable<string> GetColors();

        IEnumerable<string> GetSizes();

        IEnumerable<string> GetBrands();
    }
}