using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Data.Models
{
    public class CategoryProduct
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}
