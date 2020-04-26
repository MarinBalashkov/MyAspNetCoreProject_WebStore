using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Web.ViewModels.Products;

namespace WebStore.Web.ViewModels.ShopingCardItems
{
    public class ShoppingCartIndexViewModel
    {
        public IEnumerable<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        public string TotalPrice => this.ShoppingCartItems.Select(x => (decimal)x.Quantity * x.ProductItemProductPrice).Sum().ToString("F2");

        public IEnumerable<HomeIndexProductViewModel> LatestProducts { get; set; }

    }
}
