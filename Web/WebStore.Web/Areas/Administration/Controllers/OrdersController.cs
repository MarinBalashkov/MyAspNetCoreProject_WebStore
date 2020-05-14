using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Common;
using WebStore.Data.Models.Enums;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Orders;

namespace WebStore.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult AllOrders()
        {

            var model = this.ordersService.GetAllOrders<MyOrdersViewModel>();
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult OrderDetails(string orderId)
        {
            var model = this.ordersService.GetById<MyOrderDetailsViewModel>(orderId, null);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string orderId, OrderStatus orderStatus)
        {
            await this.ordersService.ChngeOrderStatus(orderId, orderStatus);
            return this.RedirectToAction(nameof(this.OrderDetails), new { orderId = orderId });
        }
    }
}
