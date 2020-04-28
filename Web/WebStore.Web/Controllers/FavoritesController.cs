using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Models;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Favorites;

namespace WebStore.Web.Controllers
{
    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService favoritesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductsService productsService;

        public FavoritesController(IFavoritesService favoritesService, UserManager<ApplicationUser> userManager, IProductsService productsService)
        {
            this.favoritesService = favoritesService;
            this.userManager = userManager;
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = this.favoritesService.GetAll<FavoriteProductViewModel>(userId);

            if (model == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        public async Task<IActionResult> Add(int productId)
        {
            if (!this.productsService.IsExist(productId))
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.favoritesService.AddAsync(userId, productId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
