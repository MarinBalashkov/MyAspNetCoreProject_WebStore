using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Models;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Products;
using WebStore.Web.ViewModels.ShopingCardItems;

namespace WebStore.Web.Controllers
{
    [Authorize]
    public class ShoppingCartItemsController : BaseController
    {
        private readonly IShoppingCartItemsService shoppingCartItemsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductService productService;

        public ShoppingCartItemsController(IShoppingCartItemsService shoppingCartItemsService, UserManager<ApplicationUser> userManager, IProductService productService)
        {
            this.shoppingCartItemsService = shoppingCartItemsService;
            this.userManager = userManager;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var shoppingCartItems = this.shoppingCartItemsService.GetAllShoppingCartItems<ShoppingCartItemViewModel>(userId);

            if (shoppingCartItems == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = new ShoppingCartIndexViewModel()
            {
                ShoppingCartItems = shoppingCartItems,
                LatestProducts = this.productService.GetLatestProducts<HomeIndexProductViewModel>(20),

            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShopingCardItemInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.shoppingCartItemsService.AddShoppingCartItemAsync(userId, input.ProductItemId, input.Quantity);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int productItemId, int quantity)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.shoppingCartItemsService.UpdateShopingCartItem(userId, productItemId, quantity);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int productItemId)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.shoppingCartItemsService.DeleteShopingCartItem(userId, productItemId);

            return this.RedirectToAction(nameof(this.Index));
        }

    }
}
