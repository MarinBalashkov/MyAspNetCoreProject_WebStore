using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models.Enums;

namespace WebStore.Services.Data
{
    public interface IOrderService
    {
        Task<int> CreateAsync(ShippingType shippingType, string recipientName, string recipientPhoneNumber, string userId, int addressId);

        T GetById<T>(int orderId);

        IEnumerable<T> GetAllMyOrders<T>(string userId);

        Task ChngeOrderStatus(int orderId);

        string GetOrderUserId(int orderId);

        IEnumerable<T> GetAllOrders<T>();

        Task DeleteOrder(int orderId);

        Task ConfirmOrder(int orderId);

        bool IsMyOrder(string userId, int orderId);
    }
}
