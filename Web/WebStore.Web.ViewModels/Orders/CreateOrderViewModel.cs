using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Web.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        public CreateOrderInputModel InputModel { get; set; }

        public MiniShoppingCartViewModel MiniShoppingCart { get; set; }
    }
}
