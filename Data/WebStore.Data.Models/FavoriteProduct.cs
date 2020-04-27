using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Common.Models;

namespace WebStore.Data.Models
{
    public class FavoriteProduct : BaseDeletableModel<int?>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
