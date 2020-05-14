using AutoMapper;
using System.Linq;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Images;

namespace WebStore.Web.ViewModels.Orders
{
    public class OrderedProductViewModel : IMapFrom<OrderProductItem>, IHaveCustomMappings
    {
        public string ProductItemProductName { get; set; }

        public int Quantity { get; set; }

        public decimal ProductItemProductPrice { get; set; }

        public decimal OrderedProductTotalPrice => (decimal)this.Quantity * this.ProductItemProductPrice;

        public ProductImageViewModel MainImages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderProductItem, OrderedProductViewModel>()
                .ForMember(
                m => m.MainImages,
                opt => opt.MapFrom(x => x.ProductItem.Product.Images.Where(i => (int)i.ImageType == (int)ImageType.Main).FirstOrDefault()));
        }
    }
}