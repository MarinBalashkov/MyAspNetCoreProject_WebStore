namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Web.ViewModels.Products;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAllParentCategories<T>(int? count = null);

        IEnumerable<T> GetAllSubCategoriesByParentId<T>(int parentCategoryId);

        IEnumerable<string> GetAllSubCategoriesNames(int? count = null);

    }
}
