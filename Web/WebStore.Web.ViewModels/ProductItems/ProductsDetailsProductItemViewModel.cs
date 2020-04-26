using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Data.Models;
using WebStore.Services.Mapping;

namespace WebStore.Web.ViewModels.ProductItems
{
    public class ProductsDetailsProductItemViewModel : IMapFrom<ProductItem>
    {
        public int Id { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }
    }
}
