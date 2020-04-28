using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Images;

namespace WebStore.Web.ViewModels.Favorites
{
    public class FavoriteProductViewModel : IMapFrom<FavoriteProduct>, IHaveCustomMappings
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public ProductImageViewModel MainImages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FavoriteProduct, FavoriteProductViewModel>().ForMember(
                m => m.MainImages,
                opt => opt.MapFrom(x => x.Product.Images.Where(i => (int)i.ImageType == (int)ImageType.Main).FirstOrDefault()));
        }
    }
}
