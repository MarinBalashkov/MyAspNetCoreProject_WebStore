using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Images;

namespace WebStore.Web.ViewModels.Orders
{
    public class MiniShoppingCartItemViewModel : IMapFrom<ShoppingCartItem>, IHaveCustomMappings
    {
        public int ProductItemQuantity { get; set; }

        public string ProductItemProductName { get; set; }

        public decimal ProductItemProductPrice { get; set; }

        public decimal TotalProductOrderPrice => (decimal)this.ProductItemQuantity * this.ProductItemProductPrice;

        public ProductImageViewModel MainImages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShoppingCartItem, MiniShoppingCartItemViewModel>().ForMember(
                m => m.MainImages,
                opt => opt.MapFrom(x => x.ProductItem.Product.Images.Where(i => (int)i.ImageType == (int)ImageType.Main).FirstOrDefault()));
        }
    }
}
