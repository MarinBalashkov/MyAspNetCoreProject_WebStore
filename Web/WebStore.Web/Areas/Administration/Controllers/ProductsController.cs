using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Common;
using WebStore.Services;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Administration.Products;
using WebStore.Web.ViewModels.Categoties;
using WebStore.Web.ViewModels.Products;

namespace WebStore.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ProductsController : Controller
    {
        private const int ItemsPerPage = 12;

        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly ICloudinaryService cloudinaryService;

        public ProductsController(ICategoriesService categoriesService, IProductsService productsService, ICloudinaryService cloudinaryService)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult ChooseCategory()
        {
            var mainCategories = this.categoriesService.GetAllParentCategories<CategoryViewModel>();

            return this.View(mainCategories);
        }

        public IActionResult Create(int mainCategoryId)
        {
            var model = new CreateProductInputViewModel();

            model.SubCategories = this.categoriesService.GetAllSubCategoriesByParentId<CategoryViewModel>(mainCategoryId);
            model.MainCategoryId = mainCategoryId;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubCategories = this.categoriesService.GetAllSubCategoriesByParentId<CategoryViewModel>(input.MainCategoryId);
                return this.View(input);
            }

            var mainPictur = new List<IFormFile>();
            mainPictur.Add(input.MainPicture);

            var mainPicturesUrls = await this.cloudinaryService.UploadAsync(mainPictur);
            var mainPicturesUrl = mainPicturesUrls.FirstOrDefault();
            var secondaryPictursUrls = await this.cloudinaryService.UploadAsync(input.SecondaryPicturs);

            var id = await this.productsService.CreateAsync(input.Name, input.Color, input.Description, input.Price, input.CategoryId, secondaryPictursUrls, mainPicturesUrl, input.ManufacturerId);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All(int page = 1)
        {
            var model = new ProductsAllViewModel();
            model.Products = this.productsService.GetLatestProducts<HomeIndexProductViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
            model.CurrentPage = page;

            if (model.Products == null)
            {
                return this.RedirectToAction("Create");
            }

            var count = this.productsService.GetLatestProducts<HomeIndexProductViewModel>().Count();
            model.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            return this.View(model);
        }

    }
}
