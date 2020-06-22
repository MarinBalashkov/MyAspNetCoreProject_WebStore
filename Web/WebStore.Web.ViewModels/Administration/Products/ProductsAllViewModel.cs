using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Web.ViewModels.Products;

namespace WebStore.Web.ViewModels.Administration.Products
{
    public class ProductsAllViewModel
    {
        public IEnumerable<HomeIndexProductViewModel> Products { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
