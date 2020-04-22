namespace WebStore.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Web.ViewModels.Categoties;
    using WebStore.Web.ViewModels.Products;

    public class IndexViewModel
    {


        public IEnumerable<HomeIndexProductViewModel> LatestProducts { get; set; }

        public IEnumerable<HomeIndexProductViewModel> MostLikedProducts { get; set; }

        public IEnumerable<string> SubCategoriesNames { get; set; }


    }
}
