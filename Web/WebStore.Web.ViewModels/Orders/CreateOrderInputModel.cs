namespace WebStore.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Models.Enums;

    public class CreateOrderInputModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string District { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Recipient Full Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string RecipientName { get; set; }

        [Required]
        [Display(Name = "Recipient Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string RecipientPhoneNumber { get; set; }

        public int? AddressId { get; set; }

        [Required]
        [Display(Name = "Shipping Type")]
        public ShippingType ShippingType { get; set; }
    }
}
