using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.CategoriesProducts = new HashSet<CategoryProduct>();
        }

        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        public int? ParentCartegoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<CategoryProduct> CategoriesProducts { get; set; }
    }
}
