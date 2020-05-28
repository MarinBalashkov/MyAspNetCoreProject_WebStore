namespace WebStore.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    using WebStore.Web.ViewModels.Categoties;

    public class NavBarViewModel
    {
        public IEnumerable<NavBarItemViewModel> NavBarItems { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public string BrandName { get; set; }

        public string SearchString { get; set; }
    }
}
