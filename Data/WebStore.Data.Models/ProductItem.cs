using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class ProductItem : BaseDeletableModel<int>
    {
        public ProductItem()
        {
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
            this.OrdersProductItems = new HashSet<OrderProductItem>();
        }

        [StringLength(maximumLength: 30)]
        public string Size { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual ICollection<OrderProductItem> OrdersProductItems { get; set; }

    }
}
