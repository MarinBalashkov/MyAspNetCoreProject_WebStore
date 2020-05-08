using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Orders
{
    public class ConfirmationOrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        //public string ExpectedDeliveryDateString => this.ExpectedDeliveryDate?.ToString("D", CultureInfo.InvariantCulture);

        public string ShippingType { get; set; }

        public decimal TotalPrice { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string AddressDistrict { get; set; }

        public string AddressStreet { get; set; }

        public string AddressZipCode { get; set; }

        public string AddressCity { get; set; }

    }
}
