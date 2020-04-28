namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.Orders = new HashSet<Order>();
        }

        [StringLength(maximumLength: 100)]
        public string District { get; set; }

        [StringLength(maximumLength: 100)]
        public string City { get; set; }

        [StringLength(maximumLength: 100)]
        public string Street { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
