namespace WebStore.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    using WebStore.Web.ViewModels.Categoties;

    public class NavBarItemViewModel
    {
        public CategoryViewModel ParentCategory { get; set; }

        public IEnumerable<CategoryViewModel> SubCategories { get; set; }
    }
}