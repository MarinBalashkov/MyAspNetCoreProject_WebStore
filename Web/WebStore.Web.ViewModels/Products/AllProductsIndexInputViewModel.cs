namespace WebStore.Web.ViewModels.Products
{
    public class AllProductsIndexInputViewModel
    {
        public int? ParentCategoryId { get; set; }

        public int? ChildCategoryId { get; set; }

        public decimal? MaxPrice { get; set; }

        public decimal? MinPrice { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public string BrandName { get; set; }

    }
}
