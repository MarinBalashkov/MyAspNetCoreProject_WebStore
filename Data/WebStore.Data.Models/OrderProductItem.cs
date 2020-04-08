namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrderProductItem
    {
        public OrderProductItem()
        {
            this.OrderProductItems = new HashSet<OrderProductItem>();
        }

        public int ProductItemId { get; set; }

        public ProductItem ProductItem { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<OrderProductItem> OrderProductItems { get; set; }
    }
}
