namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CategoryProduct
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
