using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Models;
using WebStore.Services.Data;

namespace WebStore.Web.ViewComponents
{
    [ViewComponent(Name = "ShoppingCart")]

    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartItemsService shoppingCartItemsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartViewComponent(IShoppingCartItemsService shoppingCartItemsService, UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartItemsService = shoppingCartItemsService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.UserClaimsPrincipal);

            int count = this.shoppingCartItemsService.GetShoppingCartItemsCount(userId);

            return this.View(count);
        }
    }
}
