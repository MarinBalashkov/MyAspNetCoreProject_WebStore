namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Addresses = new HashSet<Address>();
        }

        [StringLength(maximumLength: 30)]
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
