namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAllSubCategoriesByParentId<T>(int parentCategoryId)
        {
            IQueryable<Category> query =
            this.categoriesRepository.All()
            .Where(x => x.ParentCartegoryId == parentCategoryId);

            if (!query.Any())
            {
                return null;
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllParentCategories<T>(int? count = null)
        {
            IQueryable<Category> query =
               this.categoriesRepository.All()
               .Where(x => x.ParentCartegoryId == null)
               .OrderBy(x => x.Name);

            if (!query.Any())
            {
                return null;
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
