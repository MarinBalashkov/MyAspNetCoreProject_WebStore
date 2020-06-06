namespace WebStore.Web.ViewModels.ShoppingCartItems
{
    using System.Collections.Generic;
    using System.Linq;

    using WebStore.Web.ViewModels.Products;

    public class ShoppingCartIndexViewModel
    {
        public IEnumerable<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        public string TotalPrice => this.ShoppingCartItems.Select(x => (decimal)x.Quantity * x.ProductItemProductPrice).Sum().ToString("F2");

        public IEnumerable<HomeIndexProductViewModel> LatestProducts { get; set; }

    }
}
