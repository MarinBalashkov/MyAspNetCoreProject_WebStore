namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebStore.Data.Common.Models;

    public class OrderProductItem : BaseDeletableModel<int>
    {
        public int ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }
    }
}
