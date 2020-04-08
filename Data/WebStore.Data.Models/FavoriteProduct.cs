using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Data.Models
{
    public class FavoriteProduct
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
