using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Orders
{
    public class OrderProductItemViewModel : IMapFrom<ShoppingCartItem>
    {
        public int ProductItemId { get; set; }

        public int Quantity { get; set; }
    }
}
