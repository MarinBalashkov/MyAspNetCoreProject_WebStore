namespace WebStore.Web.ViewModels.Products
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WebStore.Data.Models;
    using WebStore.Data.Models.Enums;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Categoties;
    using WebStore.Web.ViewModels.Images;
    using WebStore.Web.ViewModels.ProductItems;
    using WebStore.Web.ViewModels.Reviews;
    using WebStore.Web.ViewModels.ShopingCardItems;

    public class ProductDetailsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ManufacturerLogoUrl { get; set; }

        public IEnumerable<ProductDetailsCategoryViewModel> CategoriesProducts { get; set; }

        public IEnumerable<ProductsDetailsProductItemViewModel> ProductItems { get; set; }

        public ProductImageViewModel MainImage { get; set; }

        public IEnumerable<ProductImageViewModel> SecondaryImages { get; set; }

        public IEnumerable<HomeIndexProductViewModel> RelatetProducts { get; set; }

        public int ReviewsCount { get; set; }

        public IEnumerable<ProductDetailsReviewViewModel> Reviews { get; set; }

        public ShopingCardItemInputViewModel ShopingCardItemInputViewModel { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(
                m => m.SecondaryImages,
                opt => opt.MapFrom(x => x.Images.Where(i => (int)i.ImageType == (int)ImageType.Secondary)))
                .ForMember(
                m => m.MainImage,
                opt => opt.MapFrom(x => x.Images.Where(i => (int)i.ImageType == (int)ImageType.Main).FirstOrDefault()));
        }
    }
}
