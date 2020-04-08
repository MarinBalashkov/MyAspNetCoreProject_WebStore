namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<int>
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Product>();
        }

        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
