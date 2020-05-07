namespace WebStore.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Favorites;

    [Authorize]
    public class FavoritesController : BaseController
    {
        private const int ItemsPerPage = 8;

        private readonly IFavoritesService favoritesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductsService productsService;

        public FavoritesController(IFavoritesService favoritesService, UserManager<ApplicationUser> userManager, IProductsService productsService)
        {
            this.favoritesService = favoritesService;
            this.userManager = userManager;
            this.productsService = productsService;
        }

        public IActionResult MyProducts(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = new FavoritesIndexViewModel();

            model.FavoriteProducts = this.favoritesService.GetAllByUserId<FavoriteProductViewModel>(userId, ItemsPerPage, (page - 1) * ItemsPerPage);

            if (model == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var count = this.favoritesService.GetCountGyUserId(userId);
            model.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = page;

            return this.View(model);
        }

        public async Task<IActionResult> Add(int productId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.productsService.IsExist(productId))
            {
                return this.NotFound();
            }

            if (this.favoritesService.IsInMyFavorites(userId, productId))
            {
                return this.RedirectToAction(nameof(this.MyProducts));
            }

            await this.favoritesService.AddAsync(userId, productId);

            return this.RedirectToAction(nameof(this.MyProducts));
        }
    }
}
