namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrderProductItem
    {
        public int ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

    }
}
