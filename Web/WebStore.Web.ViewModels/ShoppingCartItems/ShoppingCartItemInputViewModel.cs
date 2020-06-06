namespace WebStore.Web.ViewModels.ShoppingCartItems
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartItemInputViewModel 
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
