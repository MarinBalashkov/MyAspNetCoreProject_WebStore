using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models.Enums;

namespace WebStore.Services.Data
{
    public interface IOrdersService
    {
        Task<string> CreateAsync(ShippingType shippingType, string recipientName, string recipientPhoneNumber, string userId, int addressId);

        T GetById<T>(string orderId, string userId);

        IEnumerable<T> GetAllMyOrders<T>(string userId);

        Task ChngeOrderStatus(string orderId, OrderStatus orderStatus);

        string GetOrderUserId(string orderId);

        IEnumerable<T> GetAllOrders<T>();

        Task DeleteOrder(string orderId);

        Task ConfirmOrder(string orderId);

        bool IsMyOrder(string userId, string orderId);

    }
}
