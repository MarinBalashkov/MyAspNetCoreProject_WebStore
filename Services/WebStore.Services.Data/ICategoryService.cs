namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Web.ViewModels.Products;

    public interface ICategoryService
    {
        IEnumerable<T> GetAllParentCategories<T>(int? count = null);

        IEnumerable<T> GetAllSubCategoriesByParentId<T>(int parentCategoryId);

        IEnumerable<T> GetParentCategoryIdByName<T>(int? count = null);

        IEnumerable<string> GetAllSubCategoriesNames(int? count = null);

    }
}
