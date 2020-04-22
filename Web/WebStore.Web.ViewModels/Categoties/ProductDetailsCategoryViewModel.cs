using WebStore.Data.Models;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Categoties;

namespace WebStore.Web.ViewModels.Products
{
    public class ProductDetailsCategoryViewModel : IMapFrom<CategoryProduct>
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? CategoryParentCartegoryId { get; set; }

    }
}