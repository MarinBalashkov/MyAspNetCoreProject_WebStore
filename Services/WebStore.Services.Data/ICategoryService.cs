using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Services.Data
{
    public interface ICategoryService
    {
        IEnumerable<T> GetAllParentCategories<T>(int? count = null);

        IEnumerable<T> GetAllSubCategoriesByParentId<T>(int parentCategoryId);
    }
}
