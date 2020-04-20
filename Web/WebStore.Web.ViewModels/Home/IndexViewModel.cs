namespace WebStore.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Web.ViewModels.Categoties;
    using WebStore.Web.ViewModels.Products;

    public class IndexViewModel
    {


        public IEnumerable<ProductViewModel> LatestProducts { get; set; }

        public IEnumerable<ProductViewModel> MostLikedProducts { get; set; }

        public IEnumerable<string> SubCategoriesNames { get; set; }


    }
}
