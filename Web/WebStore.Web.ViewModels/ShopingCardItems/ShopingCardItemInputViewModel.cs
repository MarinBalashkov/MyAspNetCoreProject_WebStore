namespace WebStore.Web.ViewModels.ShopingCardItems
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ShopingCardItemInputViewModel 
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Size is required")]
        public int ProductItemId { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }
    }
}
