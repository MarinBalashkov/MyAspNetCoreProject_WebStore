﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Data.Models;
using WebStore.Data.Models.Enums;
using WebStore.Services.Mapping;
using WebStore.Web.ViewModels.Images;

namespace WebStore.Web.ViewModels.Home
{
    public class MostLikedProducsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductImageViewModel MainImages { get; set; }

        public IEnumerable<MostLikedProductsCategoriesViewModel> CategoriesProducts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, MostLikedProducsViewModel>().ForMember(
                m => m.MainImages,
                opt => opt.MapFrom(x => x.Images.Where(i => (int)i.ImageType == (int)ImageType.Main).FirstOrDefault()));
        }

    }
}
