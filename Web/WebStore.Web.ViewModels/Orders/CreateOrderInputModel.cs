namespace WebStore.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Models.Enums;

    public class CreateOrderInputModel
    {
        public DateTime? PreferredDeliveryDate { get; set; }

        [Required]
        [Display(Name = "Recipient Full Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string RecipientName { get; set; }

        [Required]
        [Display(Name = "Recipient Phone Number")]
        [Phone]
        public string RecipientPhoneNumber { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "Shipping Type")]
        public ShippingType ShippingType { get; set; }
    }
}
