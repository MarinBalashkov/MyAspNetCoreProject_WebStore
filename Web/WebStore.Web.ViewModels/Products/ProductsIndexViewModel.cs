using System.Collections.Generic;

namespace WebStore.Web.ViewModels.Products
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<HomeIndexProductViewModel> Products { get; set; }

        public AllProductsIndexInputViewModel Input { get; set; }

        public IEnumerable<string> Colors { get; set; }

        public IEnumerable<string> Sizes { get; set; }

        public IEnumerable<string> Brands { get; set; }

    }
}
