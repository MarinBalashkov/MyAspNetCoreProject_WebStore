using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class ShoppingCartItem
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductItemId { get; set; }

        public ProductItem ProductItem { get; set; }

        public int Quantity { get; set; }
    }
}
