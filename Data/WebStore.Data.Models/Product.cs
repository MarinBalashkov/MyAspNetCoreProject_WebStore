namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.CategoriesProducts = new HashSet<CategoryProduct>();
            this.Images = new HashSet<Image>();
            this.FavoriteProducts = new HashSet<FavoriteProduct>();
            this.ProductItems = new HashSet<ProductItem>();
        }

        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(maximumLength: 50)]
        public string Color { get; set; }

        public decimal Price { get; set; }

        public int? ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<CategoryProduct> CategoriesProducts { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }

        public virtual ICollection<ProductItem> ProductItems { get; set; }

    }
}
