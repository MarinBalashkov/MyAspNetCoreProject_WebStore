namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class ShoppingCartItem : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }

        public int Quantity { get; set; }
    }
}
