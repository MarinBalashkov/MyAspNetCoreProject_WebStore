using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.Models;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Orders;

namespace WebStore.Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderService ordersService;
        private readonly IUserService userService;
        private readonly IShoppingCartItemsService shoppingCartItemsService;

        public OrdersController(UserManager<ApplicationUser> userManager, IOrderService orderService, IUserService userService, IShoppingCartItemsService shoppingCartItemsService)
        {
            this.userManager = userManager;
            this.ordersService = orderService;
            this.userService = userService;
            this.shoppingCartItemsService = shoppingCartItemsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);
            var model = new CreateOrderViewModel();

            model.InputModel = new CreateOrderInputModel();
            model.MiniShoppingCart.ShoppingCartItems = this.shoppingCartItemsService.GetAllShoppingCartItems<MiniShoppingCartItemViewModel>(userId);
            model.MiniShoppingCart.TotalPrice = this.shoppingCartItemsService.GetShoppingCartItemsTotalPrice(userId);

            model.InputModel.RecipientName = this.userService.GetUser<OrderCreateUserViewModel>(userId)?.FullName;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (this.shoppingCartItemsService.GetShoppingCartItemsCount(userId) == 0)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var orderId = await this.ordersService.CreateAsync(input.InputModel.ShippingType, input.InputModel.RecipientName, input.InputModel.RecipientPhoneNumber, userId, input.InputModel.AddressId);

            return this.RedirectToAction(nameof(this.Confirmation), orderId);
        }

        public IActionResult Confirmation(int orderId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var model = this.ordersService.GetById<ConfirmationOrderViewModel>(orderId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmed(int orderId)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (this.ordersService.IsMyOrder(userId, orderId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.ordersService.ConfirmOrder(orderId);
            await this.shoppingCartItemsService.DeleteAllShoppingCartItems(userId);

            return this.RedirectToAction(nameof(this.ThankYou));
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
