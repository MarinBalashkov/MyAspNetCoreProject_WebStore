using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common;
using WebStore.Data.Common.Repositories;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Orders;

namespace WebStore.Services.Data
{
    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderProductItem> orderProductItemRepository;
        private readonly IShoppingCartItemsService shoppingCartItemsService;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository, IDeletableEntityRepository<OrderProductItem> orderProductItemRepository, IShoppingCartItemsService shoppingCartItemsService)
        {
            this.ordersRepository = ordersRepository;
            this.orderProductItemRepository = orderProductItemRepository;
            this.shoppingCartItemsService = shoppingCartItemsService;
        }

        public async Task ChngeOrderStatus(string orderId, OrderStatus orderStatus)
        {
            var order = this.ordersRepository.All()
                .Where(x => x.Id == orderId)
                .FirstOrDefault();

            if (order == null)
            {
                return;
            }

            order.Status = orderStatus;
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task ConfirmOrder(string orderId)
        {
            var order = this.ordersRepository.All()
            .Where(x => x.Id == orderId)
            .FirstOrDefault();
            if (order == null)
            {
                return;
            }

            order.IsConfirmed = true;
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task<string> CreateAsync(ShippingType shippingType, string recipientName, string recipientPhoneNumber, string userId, int addressId)
        {
            var totalPrice = this.shoppingCartItemsService.GetShoppingCartItemsTotalPrice(userId);

            if (shippingType == ShippingType.Fast)
            {
                totalPrice = totalPrice + GlobalConstants.ShippingPriceForFastShippingType;
            }

            var order = new Order()
            {
                AddressId = addressId,
                UserId = userId,
                RecipientName = recipientName,
                RecipientPhoneNumber = recipientPhoneNumber,
                ShippingType = shippingType,
                ExpectedDeliveryDate = DateTime.UtcNow.AddDays(7),
                Status = OrderStatus.NotVisited,
                TotalPrice = totalPrice,
                IsConfirmed = false,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            await this.CreateOrderProductItems(order.Id, userId);

            return order.Id;
        }

        public async Task DeleteOrder(string orderId)
        {
            var order = this.ordersRepository.All()
            .Where(x => x.Id == orderId)
            .FirstOrDefault();

            if (order == null)
            {
                return;
            }

            this.ordersRepository.Delete(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllMyOrders<T>(string userId)
        {
            IQueryable<Order> query =
            this.ordersRepository.All()
            .Where(x => x.UserId == userId);

            if (!query.Any())
            {
                return null;
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllOrders<T>()
        {

            IQueryable<Order> query =
            this.ordersRepository.All();
            if (!query.Any())
            {
                return null;
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string orderId, string userId)
        {
            var order = this.ordersRepository
                            .All()
                            .Where(x => x.Id == orderId && x.UserId == userId)
                            .To<T>()
                            .FirstOrDefault();

            return order;
        }

        public string GetOrderUserId(string orderId)
        {
            var userId = this.ordersRepository
                .All()
                .Where(x => x.Id == orderId)
                .Select(x => x.UserId)
                .FirstOrDefault();

            return userId;
        }

        public bool IsMyOrder(string userId, string orderId)
        {
            return this.ordersRepository.All().Any(x => x.Id == orderId && x.UserId == userId);
        }

        private async Task CreateOrderProductItems(string orderId, string userId)
        {
            var productItems = this.shoppingCartItemsService.GetAllShoppingCartItems<OrderProductItemViewModel>(userId);

            if (productItems == null)
            {
                return;
            }

            foreach (var productItem in productItems)
            {
                var orderProductItem = new OrderProductItem()
                {
                    ProductItemId = productItem.ProductItemId,
                    OrderId = orderId,
                    Quantity = productItem.Quantity,
                };

                await this.orderProductItemRepository.AddAsync(orderProductItem);
                await this.orderProductItemRepository.SaveChangesAsync();
            }
        }
    }
}
