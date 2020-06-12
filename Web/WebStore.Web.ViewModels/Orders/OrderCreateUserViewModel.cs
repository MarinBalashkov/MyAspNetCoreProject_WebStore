namespace WebStore.Web.ViewModels.Orders
{
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class OrderCreateUserViewModel : IMapFrom<ApplicationUser>
    {
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;
    }
}
