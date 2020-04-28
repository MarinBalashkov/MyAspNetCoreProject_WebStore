﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IProductsService productService;

        public ProductsController(IProductsService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(AllProductsIndexInputViewModel input)
        {
            var products = this.productService.GetProductsByFilter<HomeIndexProductViewModel>(input.ParentCategoryId, input.ChildCategoryId, input.Color, input.Size, input.BrandName);
            if (products == null)
            {
                return this.NotFound();
            }

            var model = new ProductsIndexViewModel()
            {
                Products = products,
                Colors = this.productService.GetColors(),
                Sizes = this.productService.GetSizes(),
                Brands = this.productService.GetBrands(),
            };

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
