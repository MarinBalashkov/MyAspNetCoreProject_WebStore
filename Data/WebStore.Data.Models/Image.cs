namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Common.Models;
    using WebStore.Data.Models.Enums;

    public class Image : BaseDeletableModel<int>
    {
        public ImageType ImageType { get; set; }

        public string ImageUrl { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
