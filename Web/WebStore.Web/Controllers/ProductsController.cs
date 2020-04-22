using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Products;

namespace WebStore.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Details(int id)
        {
            if (!this.productService.IsExist(id))
            {
                return this.NotFound();
            }

            var model = this.productService.GetProductById<ProductDetailsViewModel>(id);
            var categoriesIds = model.CategoriesProducts.Select(x => x.CategoryId).ToList();
            model.RelatetProducts = this.productService.GetProductsByCategoiesIds<HomeIndexProductViewModel>(categoriesIds, 10);

            return this.View(model);
        }
    }
}
