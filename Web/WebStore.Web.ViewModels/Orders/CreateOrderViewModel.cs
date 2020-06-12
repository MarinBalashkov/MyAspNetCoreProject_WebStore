namespace WebStore.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public CreateOrderInputModel InputModel { get; set; }

        public MiniShoppingCartViewModel MiniShoppingCart { get; set; }

        public IEnumerable<AddressViewModel> MyAddresses { get; set; }
    }
}
