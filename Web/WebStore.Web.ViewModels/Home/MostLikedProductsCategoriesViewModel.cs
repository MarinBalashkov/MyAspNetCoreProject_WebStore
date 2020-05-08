using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Home
{
    public class MostLikedProductsCategoriesViewModel : IMapFrom<CategoryProduct>
    {
        public string CategoryName { get; set; }

        public string CategoryParentCategoryName { get; set; }
    }
}