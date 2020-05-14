using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Orders
{
    public class MyOrderDetailsViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public string ShippingType { get; set; }

        public decimal TotalPrice { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string AddressDistrict { get; set; }

        public string AddressStreet { get; set; }

        public string AddressCity { get; set; }

        public IEnumerable<OrderedProductViewModel> OrderProductItems { get; set; }
    }
}
