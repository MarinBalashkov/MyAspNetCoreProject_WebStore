using System.Collections.Generic;

namespace WebStore.Web.ViewModels.Products
{
    public class ProductsIndexViewModel
    {
        public string RouteInfo { get; set; }

        public IEnumerable<HomeIndexProductViewModel> Products { get; set; }

        public IEnumerable<string> Colors { get; set; }

        public IEnumerable<string> Sizes { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public AllProductsIndexInputViewModel InputModel { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
