namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Common.Models;
    using WebStore.Data.Models.Enums;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrderProductItems = new HashSet<OrderProductItem>();
        }

        public OrderStatus Status { get; set; }

        public DateTime? PreferredDeliveryDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public decimal TotalProductsPrice { get; set; }

        public decimal ShippingPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<OrderProductItem> OrderProductItems { get; set; }
    }
}
