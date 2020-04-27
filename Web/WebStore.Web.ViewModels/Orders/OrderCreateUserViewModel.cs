using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.Orders
{
    public class OrderCreateUserViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;
    }
}
