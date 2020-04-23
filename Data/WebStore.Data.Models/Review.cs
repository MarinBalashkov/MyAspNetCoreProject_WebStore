namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductiD { get; set; }

        public Product Product { get; set; }

    }
}
