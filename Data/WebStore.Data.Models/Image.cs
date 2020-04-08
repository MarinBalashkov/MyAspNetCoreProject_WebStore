namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string ImageUrl { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
