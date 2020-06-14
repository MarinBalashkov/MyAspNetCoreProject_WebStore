namespace WebStore.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Orders;

    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;
        private readonly IUsersService userService;
        private readonly IShoppingCartItemsService shoppingCartItemsService;
        private readonly IAddressesService addressesService;

        public OrdersController(UserManager<ApplicationUser> userManager, IOrdersService orderService, IUsersService userService, IShoppingCartItemsService shoppingCartItemsService, IAddressesService addressesService)
        {
            this.userManager = userManager;
            this.ordersService = orderService;
            this.userService = userService;
            this.shoppingCartItemsService = shoppingCartItemsService;
            this.addressesService = addressesService;
        }

        [HttpGet]
        public IActionResult Create(int? addressId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (this.shoppingCartItemsService.GetShoppingCartItemsCount(userId) == 0)
            {
                return this.RedirectToAction("Index", "Home");
                //this.ModelState.AddModelError(nameof(CreateOrderViewModel.MiniShoppingCart.ShoppingCartItems), "Add Products in ShoppingCart");
            }

            var model = new CreateOrderViewModel();

            model.MyAddresses = this.addressesService.GetMyAddresses<AddressViewModel>(userId);

            model.MiniShoppingCart = new MiniShoppingCartViewModel();
            model.MiniShoppingCart.ShoppingCartItems = this.shoppingCartItemsService.GetAllShoppingCartItems<MiniShoppingCartItemViewModel>(userId);
            model.MiniShoppingCart.TotalPrice = this.shoppingCartItemsService.GetShoppingCartItemsTotalPrice(userId);

            model.InputModel = new CreateOrderInputModel();
            var address = this.addressesService.GetById<AddressViewModel>(addressId, userId);
            if (address != null)
            {
                model.InputModel.AddressId = address.Id;
                model.InputModel.City = address.City;
                model.InputModel.District = address.District;
                model.InputModel.Street = address.Street;
            }

            model.InputModel.RecipientName = this.userService.GetUser<OrderCreateUserViewModel>(userId)?.FullName;
            model.InputModel.RecipientPhoneNumber = this.userService.GetUser<OrderCreateUserViewModel>(userId)?.PhoneNumber;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (this.shoppingCartItemsService.GetShoppingCartItemsCount(userId) == 0)
            {
                return this.RedirectToAction("Index", "Home");
                //this.ModelState.AddModelError(nameof(CreateOrderViewModel.MiniShoppingCart.ShoppingCartItems), "Add Products in ShoppingCart");
            }

            if (!this.ModelState.IsValid)
            {
                input.MyAddresses = this.addressesService.GetMyAddresses<AddressViewModel>(userId);

                input.MiniShoppingCart = new MiniShoppingCartViewModel();
                input.MiniShoppingCart.ShoppingCartItems = this.shoppingCartItemsService.GetAllShoppingCartItems<MiniShoppingCartItemViewModel>(userId);
                input.MiniShoppingCart.TotalPrice = this.shoppingCartItemsService.GetShoppingCartItemsTotalPrice(userId);
                return this.View(input);
            }

            int addressId;

            if (input.InputModel.AddressId == null)
            {
                addressId = await this.addressesService.CreateAsync(userId, input.InputModel.District, input.InputModel.City, input.InputModel.Street);
            }
            else
            {
                addressId = await this.addressesService.UpdateAddressAsync(userId, input.InputModel.AddressId, input.InputModel.District, input.InputModel.City, input.InputModel.Street);
            }

            if (addressId == 0)
            {
                return this.NotFound();
            }

            var currentOrderId = await this.ordersService.CreateAsync(input.InputModel.ShippingType, input.InputModel.RecipientName, input.InputModel.RecipientPhoneNumber, userId, addressId);

            return this.RedirectToAction(nameof(this.Confirmation), new { orderId = currentOrderId });
        }

        public IActionResult Confirmation(string orderId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = this.ordersService.GetById<ConfirmationOrderViewModel>(orderId, userId);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmed(string orderId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.ordersService.IsMyOrder(userId, orderId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.ordersService.ConfirmOrder(orderId);
            await this.shoppingCartItemsService.DeleteAllShoppingCartItems(userId);

            return this.RedirectToAction("ThankYou", "Orders");
        }

        [HttpPost] // TODO Sent Email
        public async Task<IActionResult> Delete(string orderId)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (!this.ordersService.IsMyOrder(userId, orderId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.ordersService.DeleteOrder(orderId);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult MyOrders()
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = this.ordersService.GetAllMyOrders<MyOrdersViewModel>(userId);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult MyOrderDetails(string orderId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = this.ordersService.GetById<MyOrderDetailsViewModel>(orderId, userId);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
