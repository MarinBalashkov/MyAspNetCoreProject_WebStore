using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Web.ViewModels.Orders
{
    public class MiniShoppingCartViewModel
    {
        public IEnumerable<MiniShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
