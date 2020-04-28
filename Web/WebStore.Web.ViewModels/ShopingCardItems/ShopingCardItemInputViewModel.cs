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

        [Required]
        public int ProductItemId { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int Quantity { get; set; }
    }
}
