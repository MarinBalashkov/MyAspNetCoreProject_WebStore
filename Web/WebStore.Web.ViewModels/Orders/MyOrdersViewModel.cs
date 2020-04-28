using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Orders
{
    public class MyOrdersViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public string ExpectedDeliveryDateString => this.ExpectedDeliveryDate?.ToString("D", CultureInfo.InvariantCulture);

        public decimal TotalPrice { get; set; }

    }
}
