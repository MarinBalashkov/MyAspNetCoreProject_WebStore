namespace WebStore.Web.ViewModels.Orders
{
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class AddressViewModel : IMapFrom<Address>
    {
        public int Id { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string AddressString => $"{this.District} - {this.City} - {this.Street}";
    }
}