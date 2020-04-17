using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Web.ViewModels.Categoties;

namespace WebStore.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<CategoryViewModel> ParentCategorie { get; set; }

        public IEnumerable<CategoryViewModel> ChildCategories { get; set; }

    }
}
