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
    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderProductItem> orderProductItemRepository;
        private readonly IShoppingCartItemsService shoppingCartItemsService;

        public OrderService(IDeletableEntityRepository<Order> ordersRepository, IDeletableEntityRepository<OrderProductItem> orderProductItemRepository, IShoppingCartItemsService shoppingCartItemsService)
        {
            this.ordersRepository = ordersRepository;
            this.orderProductItemRepository = orderProductItemRepository;
            this.shoppingCartItemsService = shoppingCartItemsService;
        }

        public Task ChngeOrderStatus(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task ConfirmOrder(int orderId)
        {
            var order = this.GetById<Order>(orderId);
            if (order == null)
            {
                return;
            }

            order.IsConfirmed = true;
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(ShippingType shippingType, string recipientName, string recipientPhoneNumber, string userId, int addressId)
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

        public async Task DeleteOrder(int orderId)
        {
            var order = this.GetById<Order>(orderId);

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

        public T GetById<T>(int orderId)
        {
            var order = this.ordersRepository
                            .All()
                            .Where(x => x.Id == orderId)
                            .To<T>()
                            .FirstOrDefault();

            return order;
        }

        public string GetOrderUserId(int orderId)
        {
            var userId = this.ordersRepository
                .All()
                .Where(x => x.Id == orderId)
                .Select(x => x.UserId)
                .FirstOrDefault();

            return userId;
        }

        public bool IsMyOrder(string userId, int orderId)
        {
            return this.ordersRepository.All().Any(x => x.Id == orderId && x.UserId == userId);
        }

        private async Task CreateOrderProductItems(int orderId, string userId)
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
