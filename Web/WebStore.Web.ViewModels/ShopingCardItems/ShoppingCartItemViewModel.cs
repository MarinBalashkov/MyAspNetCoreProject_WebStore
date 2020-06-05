using AutoMapper;
using System.Linq;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Images;

namespace WebStore.Web.ViewModels.ShopingCardItems
{
    public class ShoppingCartItemViewModel : IMapFrom<ShoppingCartItem>, IHaveCustomMappings
    {
        public int ProductItemProductId { get; set; }

        public int ProductItemId { get; set; }

        public string ProductItemSize { get; set; }

        public int ProductItemQuantity { get; set; }

        public string ProductItemProductName { get; set; }

        public decimal ProductItemProductPrice { get; set; }

        public ProductImageViewModel MainImages { get; set; }

        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>().ForMember(
                m => m.MainImages,
                opt => opt.MapFrom(x => x.ProductItem.Product.Images.Where(i => (int)i.ImageType == (int)ImageType.Main).FirstOrDefault()));
        }
    }
}