namespace WebStore.Web.Controllers
{
    using System.Diagnostics;

    using WebStore.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using WebStore.Web.ViewModels.Home;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Products;

    public class HomeController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                LatestProducts = this.productService.GetLatestProducts<ProductViewModel>(20),
                MostLikedProducts = this.productService.GetMostLikedProducts<ProductViewModel>(16),
                SubCategoriesNames = this.categoryService.GetAllSubCategoriesNames(),
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
