using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Products;
using WebStore.Web.ViewModels.Reviews;
using WebStore.Web.ViewModels.ShopingCardItems;

namespace WebStore.Web.Controllers
{
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

            var products = this.productService.GetProductsByFilter<HomeIndexProductViewModel>(input.ParentCategoryName, input.ChildCategoryName, input.Color, input.Size, input.BrandName, ItemsPerPage, (page - 1) * ItemsPerPage);
            if (products == null)
            {
                return this.NotFound();
            }

            model.RouteInfo = input.ParentCategoryName != null ? input.ParentCategoryName + "/" : string.Empty +
                              input.ChildCategoryName != null ? input.ChildCategoryName + "/" : string.Empty +
                              input.Color != null ? input.Color + "/" : string.Empty +
                              input.Size != null ? input.Size + "/" : string.Empty +
                              input.BrandName != null ? input.BrandName + "/" : string.Empty;
            model.Products = products;
            model.InputModel = input;

            var count = this.productService.GetCountByFilter(input.ParentCategoryName, input.ChildCategoryName, input.Color, input.Size, input.BrandName);

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
