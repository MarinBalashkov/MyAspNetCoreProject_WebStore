namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Products;
    using WebStore.Web.ViewModels.Reviews;
    using WebStore.Web.ViewModels.ShopingCardItems;

    public class ProductsController : BaseController
    {
        private const int ItemsPerPage = 12;

        private readonly IProductsService productService;

        public ProductsController(IProductsService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(AllProductsIndexInputViewModel input, int page = 1)
        {
            var model = new ProductsIndexViewModel()
            {
                Colors = this.productService.GetColors(),
                Sizes = this.productService.GetSizes(),
                Brands = this.productService.GetBrands(),
            };

            var products = this.productService.GetProductsByFilterWithPagenation<HomeIndexProductViewModel>(input.ParentCategoryName, input.ChildCategoryName, input.Color, input.Size, input.BrandName, input.SearchString, ItemsPerPage, (page - 1) * ItemsPerPage);

            if (products == null)
            {
                return this.NotFound();
            }

            model.Products = products;

            var sb = new StringBuilder();
            sb.AppendLine(!string.IsNullOrWhiteSpace(input.SearchString) ? $"Search result for : {input.SearchString}" : string.Empty);
            sb.AppendLine();
            sb.Append(input.ParentCategoryName != null ? $"Category : {input.ParentCategoryName}" : string.Empty);
            sb.Append(input.ChildCategoryName != null ? $" / {input.ChildCategoryName}" : string.Empty);
            sb.Append(input.Color != null ? $" / Color : {input.Color}" : string.Empty);
            sb.Append(input.Size != null ? $" / Size : {input.Size}" : string.Empty);
            sb.Append(input.BrandName != null ? $" / Brand : {input.BrandName}" : string.Empty);

            model.RouteInfo = sb.ToString();
            model.InputModel = input;

            var count = this.productService.GetProductsByFilter(input.ParentCategoryName, input.ChildCategoryName, input.Color, input.Size, input.BrandName, input.SearchString).Count();
            model.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = page;

            return this.View(model);
        }

        public IActionResult Details(int id)
        {
            if (!this.productService.IsExist(id))
            {
                return this.NotFound();
            }

            var model = this.productService.GetProductById<ProductDetailsViewModel>(id);
            var categoriesIds = model.CategoriesProducts.Select(x => x.CategoryId).ToList();
            model.RelatetProducts = this.productService.GetProductsBySubCategoiesIds<HomeIndexProductViewModel>(categoriesIds, 10);
            return this.View(model);
        }
    }
}
